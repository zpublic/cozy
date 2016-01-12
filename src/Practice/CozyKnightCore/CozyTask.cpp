#include "StdAfx.h"
#include "CozyTask.h"
#include <algorithm>
#include "PredicateObject.h"

CozyTask::CozyTask(HANDLE hTarget)
    :m_hTarget(hTarget)
{
    
}

CozyTask::~CozyTask(void)
{

}

void CozyTask::SearchRange(int min, int max)
{
    // not implemented
}

ADDRESS_LIST CozyTask::GetResultAddress()
{
    return m_AddrList;
}

void CozyTask::SearcFirst(LPCVOID lpData, DWORD dwSize)
{
    PredicateObject pred(lpData, dwSize, m_hTarget);

    MEMORY_BASIC_INFORMATION mbi;
    ::ZeroMemory(&mbi, sizeof(mbi));

    std::vector<BYTE> vecBuffer;
    LPBYTE lpMemAddress     = 0;
    BOOL bReadRet           = FALSE;

    while(::VirtualQueryEx(m_hTarget, lpMemAddress, &mbi, sizeof(mbi)))
    {
        if(mbi.Type == MEM_PRIVATE && (mbi.Protect & PAGE_READWRITE) && mbi.State == MEM_COMMIT)
        {
            vecBuffer.resize(mbi.RegionSize);
            if(::ReadProcessMemory(m_hTarget, lpMemAddress, &vecBuffer[0], mbi.RegionSize, NULL))
            {
                for(DWORD i = 0; i < mbi.RegionSize; i+= dwSize)
                {
                    if(pred(&vecBuffer[i], dwSize))
                    {
                        ADDRESS_INFO result;
                        result.addr = lpMemAddress + i;
                        result.size = dwSize;

                        m_AddrList.push_back(result);
                    }
                }
            }
        }
        lpMemAddress += mbi.RegionSize;
    }
}

void CozyTask::SearchNext(LPCVOID lpData, DWORD dwSize)
{
    PredicateObject pred(lpData, dwSize, m_hTarget);

    m_AddrList.erase(std::remove_if(m_AddrList.begin(), m_AddrList.end(), pred), m_AddrList.end());
}

void CozyTask::SearchDoubleWord(DWORD value)
{
    if(m_AddrList.size() == 0)
    {
        SearcFirst(&value, sizeof(value));
    }
    else
    {
        SearchNext(&value, sizeof(value));
    }
}

void CozyTask::SearchWord(WORD value)
{
    if(m_AddrList.size() == 0)
    {
        SearcFirst(&value, sizeof(value));
    }
    else
    {
        SearchNext(&value, sizeof(value));
    }
}

void CozyTask::SearchByte(BYTE value)
{
    if(m_AddrList.size() == 0)
    {
        SearcFirst(&value, sizeof(value));
    }
    else
    {
        SearchNext(&value, sizeof(value));
    }
}

void CozyTask::SearchBytes(LPCVOID lpData, DWORD dwSize)
{
    if(m_AddrList.size() == 0)
    {
        SearcFirst(lpData, dwSize);
    }
    else
    {
        SearchNext(lpData, dwSize);
    }
}