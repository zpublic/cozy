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
    return m_lpszCfgPath.c_str();
}

const wchar_t* CozyThunderTaskImpl::GetRemotePath()
{
    return m_lpszRemtotePath.c_str();
}

const wchar_t* CozyThunderTaskImpl::GetLocalPath()
{
    return m_lpszLocalPath.c_str();
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
    return m_vecBlock[blockId].GetBlcokStatus();
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
    auto size = InitFile();
    
    auto rangeList = splitSize(size);

    InitBlock(rangeList.size());
    m_threadPool.Start();

    OnTaskBegin();

    for (std::size_t i = 0; i < rangeList.size(); ++i)
    {
        m_threadPool.PostTask([=]()
        {
            int pos     = i;
            auto range  = rangeList[pos];

            HttpClient client;
            HttpBuffer buffer;
            client.SetEnableSSL(true);

            if (GetBlockState(pos) == BlockStatusFinish) return;

            buffer.Clear();
            OnBlockStatus(pos, 0);

            auto strRange = __makeRange(range.first, range.second);
            client.AppendHttpHeader(std::make_pair("Range", strRange));

            if (client.DownloadFile(ws2s(m_lpszRemtotePath).c_str(), &buffer) / 100 == 2)
            {
                __safeWrite(pos * DefaultBlockSize, buffer.GetData(), buffer.GetSize());

                ++m_finishblcokCount;
                OnBlockStatus(pos, 1);

                if (m_finishblcokCount == m_vecBlock.size())
                {
                    OnTaskEnd(0);
                }
            }
            else
            {
                OnBlockStatus(pos, 2);
            }
        });
    }

    return true;
}

// state 1 -> 2
bool CozyThunderTaskImpl::Stop()
{
    m_threadPool.Stop();
    m_state = 2;
    OnTaskEnd(1);
    return true;
}

std::string CozyThunderTaskImpl::ws2s(const std::wstring& str)
{
    std::mbstate_t state = std::mbstate_t();
    auto ptr = str.c_str();
    int len = 1 + std::wcsrtombs(NULL, &ptr, 0, &state);
    std::vector<char> mbstr(len);
    std::wcsrtombs(&mbstr[0], &ptr, mbstr.size(), &state);
    return std::string(mbstr.begin(), mbstr.end());
}

std::wstring CozyThunderTaskImpl::s2ws(const std::string& str)
{
    std::mbstate_t state = std::mbstate_t();
    auto ptr = str.c_str();
    int len = 1 + std::mbsrtowcs(NULL, &ptr, 0, &state);
    std::vector<wchar_t> wstr(len);
    std::mbsrtowcs(&wstr[0], &ptr, wstr.size(), &state);
    return std::wstring(wstr.begin(), wstr.end());
}

void CozyThunderTaskImpl::__safeWrite(std::size_t offset, const void* data, std::size_t size)
{
    std::lock_guard<std::mutex> lock(m_fileMutex);

    m_plocalFile.seekp(offset, std::ios::beg);
    m_plocalFile.write(static_cast<const char*>(data), size);
}

std::string CozyThunderTaskImpl::__makeRange(int begin, int end)
{
    std::string value = "bytes=";
    value += std::to_string(begin);
    value += "-";
    value += std::to_string(end);
    return value;
}

std::size_t CozyThunderTaskImpl::InitFile()
{
    auto size = this->GetFileSize();
    if (size <= 0) return 0;

    auto path = ws2s(m_lpszLocalPath);
    m_plocalFile.open(path, std::ios::out | std::ios::binary | std::ios::_Nocreate);
    if (!m_plocalFile.is_open())
    {
        m_plocalFile.open(path, std::ios::out | std::ios::binary | std::ios::_Noreplace);
    }

    m_plocalFile.seekp(size - 1);
    char buffer = 0;
    m_plocalFile.write(&buffer, sizeof(char));

    return size;
}

void CozyThunderTaskImpl::OnTaskBegin()
{
    m_state = 1;

    if (m_pCallback != nullptr)
    {
        m_pCallback->OnStart();
    }
}

void CozyThunderTaskImpl::OnBlockStatus(int id, int status)
{
    m_vecBlock[id].SetBlockStatus(status);
    if (m_pCallback != nullptr)
    {
        m_pCallback->OnBlockState(id, status);
    }
}

void CozyThunderTaskImpl::OnTaskEnd(int code)
{
    m_plocalFile.close();
    //std::fclose(m_plocalFile);
    //m_plocalFile = nullptr;

    m_state = 3;

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
            m_state = 2;
            auto status = block.GetBlcokStatus();
            if (status == BlockStatusFailed)
            {
                block.SetBlockStatus(BlockStatusStart);
            }
            else if (status == BlockStatusFinish)
            {
                ++m_finishblcokCount;
            }
        }
    }
    else
    {
        m_state = 0;
        Block block{};
        block.SetBlockStatus(BlockStatusStart);
        m_vecBlock.assign(n, block);
    }
}

void CozyThunderTaskImpl::SetTaskState(int value)
{
    m_state = value;
}

void CozyThunderTaskImpl::SetFileSize(unsigned long value)
{
    m_remoteSize = value;
}

void CozyThunderTaskImpl::SetBlockCount(unsigned int value)
{
    m_vecBlock.clear();
    InitBlock(value);
}

void CozyThunderTaskImpl::SetBlockInfo(const std::wstring& value)
{
    if (value.size() == m_vecBlock.size())
    {
        for (std::size_t i = 0; i < value.size(); ++i)
        {
            Block b;
            b.SetBlockStatus(value[i] - L'0');
            m_vecBlock[i] = b;
        }
    }
}

std::vector<std::pair<std::size_t, std::size_t>> CozyThunderTaskImpl::splitSize(std::size_t size)
{
    std::vector<std::pair<std::size_t, std::size_t>> res;
    auto blockNum = (size + DefaultBlockSize - 1) / DefaultBlockSize;

    for (unsigned int i = 0; i < blockNum; ++i)
    {
        std::size_t needToRead = (i == blockNum - 1) ? (size % DefaultBlockSize) : DefaultBlockSize;
        res.emplace_back(std::make_pair(i * DefaultBlockSize, i * DefaultBlockSize + needToRead - 1));
    }

    return res;
}