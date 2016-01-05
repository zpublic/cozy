#include "StdAfx.h"
#include "CozyTask.h"
#include <algorithm>

CozyTask::CozyTask(HANDLE hTarget)
    :m_hTarget(hTarget)
{
    
}

CozyTask::~CozyTask(void)
{

}

void CozyTask::Search(int value)
{
    if(m_AddrList.size() == 0)
    {
        SearcFirst(value);
    }
    else
    {
        SearchNext(value);
    }
}

void CozyTask::SearchRange(int min, int max)
{
    // not implemented
}

ADDRESS_LIST CozyTask::GetResultAddress()
{
    return m_AddrList;
}

void CozyTask::SearcFirst(int value)
{
    MEMORY_BASIC_INFORMATION mbi;
    ::ZeroMemory(&mbi, sizeof(mbi));

    int nBuffer             = 0;
    LPBYTE lpMemAddress     = 0;
    BOOL bReadRet           = FALSE;

    while(::VirtualQueryEx(m_hTarget, lpMemAddress, &mbi, sizeof(mbi)))
    {
        if(mbi.Type == MEM_PRIVATE && (mbi.Protect & PAGE_READWRITE) && mbi.State == MEM_COMMIT)
        {
            if(::ReadProcessMemory(m_hTarget, lpMemAddress, &nBuffer, mbi.RegionSize, NULL))
            {
                for(DWORD i = 0; i + sizeof(int) < mbi.RegionSize; i+=sizeof(int))
                {
                    if(value == nBuffer)
                    {
                        ADDRESS_INFO result;
                        result.addr = lpMemAddress + i;
                        result.size = sizeof(int);

                        m_AddrList.push_back(result);
                    }
                }
            }
        }
        lpMemAddress += mbi.RegionSize;
    }
}

void CozyTask::SearchNext(int value)
{
    PredicateObject<int> pred(value, m_hTarget);

    m_AddrList.erase(std::remove_if(m_AddrList.begin(), m_AddrList.end(), pred), m_AddrList.end());
}