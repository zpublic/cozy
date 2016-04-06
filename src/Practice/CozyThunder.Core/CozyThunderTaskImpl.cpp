#include "stdafx.h"
#include "CozyThunderTaskImpl.h"
#include "HttpClient.h"
#include "HttpBuffer.h"
#include <cwchar>
#include <string>

using namespace Cozy;

CozyThunderTaskImpl::CozyThunderTaskImpl()
    :m_pCallback(nullptr),
    m_threadPool(5, 10),
    m_lpszLocalPath(nullptr),
    m_lpszRemtotePath(nullptr),
    m_lpszCfgPath(nullptr),
    m_state(0),
    m_remoteSize(-1)
{

}

CozyThunderTaskImpl::~CozyThunderTaskImpl()
{

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
    if (m_remoteSize == -1)
    {
        auto client = HttpClient();

        auto str = ws2s(m_lpszRemtotePath);
        m_remoteSize = client.GetFileSize(str.c_str());
    }
    
    return m_remoteSize;
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
    auto size = this->GetFileSize();

    if (size <= 0) return false;

    auto str = ws2s(m_lpszLocalPath);

    m_plocalFile =  std::fopen(str.c_str(), "wb+");
    if (!m_plocalFile) return false;

    std::fseek(m_plocalFile, size - 1, SEEK_SET);
    char buffer = 0;
    std::fwrite(&buffer, sizeof(char), 1, m_plocalFile);

    auto blockNum = (size + DefaultBlockSize - 1) / DefaultBlockSize;

    m_vecBlock.clear();
    Block block{};
    block.SetBlockStatus(1);
    m_vecBlock.assign(blockNum, block);

    m_threadPool.Start();

    auto taskNum = (blockNum > 5 ? 5 : blockNum);
    for (int i = 0; i < taskNum; ++i)
    {
        m_threadPool.PostTask([=]()
        {
            int startPos = i;
            int readSize = blockNum / taskNum;
            std::size_t needToRead = (startPos == blockNum - 1) ? (size % DefaultBlockSize) : DefaultBlockSize;
            HttpClient client;
            client.SetEnableSSL(true);
            for (int curpos = startPos; curpos != i + readSize; ++curpos)
            {
                HttpBuffer buffer;
                std::string value = "bytes=";
                value += std::to_string(curpos * needToRead);
                value += "-";
                value += std::to_string((curpos + 1) * needToRead);

                client.AppendHttpHeader(std::make_pair("Range", value));

                auto str = ws2s(m_lpszRemtotePath);
                client.DownloadFile(str.c_str(), &buffer);

                std::fseek(m_plocalFile, curpos * needToRead, SEEK_SET);
                std::fwrite(buffer.GetData(), buffer.GetSize(), 1, m_plocalFile);
            }
        });
    }

    return true;
}

// state 1 -> 2
bool CozyThunderTaskImpl::Stop()
{
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