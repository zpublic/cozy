#include "stdafx.h"
#include "CozyThunderTaskImpl.h"

using namespace Cozy;

CozyThunderTaskImpl::CozyThunderTaskImpl()
    :m_state(0), 
    m_lpszCfgPath(nullptr), 
    m_lpszRemtotePath(nullptr), 
    m_lpszLocalPath(nullptr), 
    m_pCallback(nullptr)
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
    return 0;
}
unsigned int CozyThunderTaskImpl::GetBlockCount()
{
    return 0;
}

// state = 0 下载开始 1 下载完成 2下载失败
int CozyThunderTaskImpl::GetBlockState(unsigned int blockId)
{
    return 0;
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
    return false;
}

// state 1 -> 2
bool CozyThunderTaskImpl::Stop()
{
    return false;
}