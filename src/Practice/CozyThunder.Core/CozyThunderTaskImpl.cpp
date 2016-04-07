#include "stdafx.h"
#include "CozyThunderTaskImpl.h"
#include "HttpClient.h"
#include "HttpBuffer.h"
#include <cwchar>
#include <string>
#include <algorithm>

using namespace Cozy;

CozyThunderTaskImpl::CozyThunderTaskImpl()
    :m_pCallback(nullptr),
    m_threadPool(5, 10),
    m_lpszLocalPath(nullptr),
    m_lpszRemtotePath(nullptr),
    m_lpszCfgPath(nullptr),
	m_finishblcokCount(0),
    m_state(0),
    m_remoteSize(-1)
{

}

CozyThunderTaskImpl::~CozyThunderTaskImpl()
{
    Stop();
}

int CozyThunderTaskImpl::GetTaskState()
{
    return m_state;
}

const wchar_t* CozyThunderTaskImpl::GetCfgPath()
{
    return m_lpszCfgPath;
}

const wchar_t* CozyThunderTaskImpl::GetRemotePath()
{
    return m_lpszRemtotePath;
}

const wchar_t* CozyThunderTaskImpl::GetLocalPath()
{
    return m_lpszLocalPath;
}

unsigned long CozyThunderTaskImpl::GetFileSize()
{
    HttpClient client;
    return client.GetFileSize(ws2s(m_lpszRemtotePath).c_str());
}
unsigned int CozyThunderTaskImpl::GetBlockCount()
{
    return m_vecBlock.size();
}

// state = 0 下载开始 1 下载完成 2下载失败
int CozyThunderTaskImpl::GetBlockState(unsigned int blockId)
{
    if (blockId < m_vecBlock.size())
    {
        return m_vecBlock[blockId].GetBlcokStatus();
    }
    return BlockStatusInvalid;
}

void CozyThunderTaskImpl::SetCfgPath(const wchar_t* path)
{
    m_lpszCfgPath = path;
}

void CozyThunderTaskImpl::SetRemotePath(const wchar_t* path)
{
    m_lpszRemtotePath = path;
}

void CozyThunderTaskImpl::SetLocalPath(const wchar_t* path)
{
    m_lpszLocalPath = path;
}

void CozyThunderTaskImpl::SetTaskCallback(ICozyThunderTaskCallback* pCallback)
{
    m_pCallback = pCallback;
}

// state 0/2 -> 1
bool CozyThunderTaskImpl::Start()
{
    auto size		= InitLocalFile();
    auto blockNum	= (size + DefaultBlockSize - 1) / DefaultBlockSize;

    InitBlock(blockNum);
    m_threadPool.Start();
    OnTaskBegin();

    for (int i = 0; i < blockNum; ++i)
    {
        m_threadPool.PostTask([=]()
        {
            int startPos = i;
            int readSize = blockNum / blockNum;
            std::size_t needToRead = (startPos == blockNum - 1) ? (size % DefaultBlockSize) : DefaultBlockSize;

            HttpClient client;
            HttpBuffer buffer;
            client.SetEnableSSL(true);

            for (int curpos = startPos; curpos != startPos + readSize; ++curpos)
            {
                if (GetBlockState(curpos) == BlockStatusFinish) continue;

                buffer.Clear();
                OnBlockStatus(curpos, 0);

                auto strRange = __makeRange(curpos * DefaultBlockSize, curpos * DefaultBlockSize + needToRead - 1);
                client.AppendHttpHeader(std::make_pair("Range", strRange));

                auto str = ws2s(m_lpszRemtotePath);
                if (client.DownloadFile(str.c_str(), &buffer) / 100 == 2)
                {
                    __safeWrite(m_plocalFile, curpos * DefaultBlockSize, buffer.GetData(), buffer.GetSize());

                    ++m_finishblcokCount;
                    m_vecBlock[curpos].SetBlockStatus(BlockStatusFinish);
                    OnBlockStatus(curpos, 1);

                    if (m_finishblcokCount == m_vecBlock.size())
                    {
                        std::fclose(m_plocalFile);
                        OnTaskEnd(0);
                    }
                }
                else
                {
                    m_vecBlock[curpos].SetBlockStatus(BlockStatusFailed);
                    OnBlockStatus(curpos, 2);
                }
            }
        });
    }

    return true;
}

// state 1 -> 2
bool CozyThunderTaskImpl::Stop()
{
    m_threadPool.Stop();
    OnTaskPause();
    return false;
}

std::string CozyThunderTaskImpl::ws2s(const wchar_t* ptr)
{
    std::mbstate_t state = std::mbstate_t();
    int len = 1 + std::wcsrtombs(NULL, &ptr, 0, &state);
    std::vector<char> mbstr(len);
    std::wcsrtombs(&mbstr[0], &ptr, mbstr.size(), &state);
    return std::string(mbstr.begin(), mbstr.end());
}

void CozyThunderTaskImpl::__safeWrite(std::FILE* pFile, std::size_t offset, const void* data, std::size_t size)
{
    std::lock_guard<std::mutex> lock(m_fileMutex);

    std::fseek(pFile, offset, SEEK_SET);
    std::fwrite(data, size, 1, pFile);
    std::fflush(pFile);
}

std::string CozyThunderTaskImpl::__makeRange(int begin, int end)
{
    std::string value = "bytes=";
    value += std::to_string(begin);
    value += "-";
    value += std::to_string(end);
    return value;
}

std::size_t CozyThunderTaskImpl::InitLocalFile()
{
    auto size = this->GetFileSize();

    if (size <= 0) return 0;

    auto str = ws2s(m_lpszLocalPath);

    m_plocalFile = std::fopen(str.c_str(), "wb+");
    if (!m_plocalFile) return 0;

    std::fseek(m_plocalFile, size - 1, SEEK_SET);
    char buffer = 0;
    std::fwrite(&buffer, sizeof(char), 1, m_plocalFile);

    return size;
}

void CozyThunderTaskImpl::OnTaskBegin()
{
    if (m_pCallback != nullptr)
    {
        m_pCallback->OnStart();
    }
}

void CozyThunderTaskImpl::OnBlockStatus(int id, int status)
{
    if (m_pCallback != nullptr)
    {
        m_pCallback->OnBlockState(id, status);
    }
}

void CozyThunderTaskImpl::OnTaskEnd(int code)
{
    if (m_pCallback != nullptr)
    {
        m_pCallback->OnStop(code);
    }
}

void CozyThunderTaskImpl::InitBlock(int n)
{
    m_finishblcokCount = 0;
    if (m_vecBlock.size() == n)
    {
        for (auto& block : m_vecBlock)
        {
            auto status = block.GetBlcokStatus();
            if (status == BlockStatusStart || status == BlockStatusFailed)
            {
                block.SetBlockStatus(BlockStatusNew);
            }
            else if(status != BlockStatusNew)
            {
                ++m_finishblcokCount;
            }
        }
    }
    else
    {
        Block block{};
        block.SetBlockStatus(BlockStatusNew);
        m_vecBlock.assign(n, block);
    }
}

void CozyThunderTaskImpl::OnTaskPause()
{
    OnTaskEnd(1);
}
