#include "stdafx.h"
#include "CozyKnightCore.h"
#include <vector>
#include <algorithm>

CozyKnightCore::CozyKnightCore()
    :m_hTarget(NULL)
{

}

CozyKnightCore::~CozyKnightCore()
{
    Detch();
}

BOOL CozyKnightCore::SearchFirst(HANDLE hProcess, const MemoryTester lpTester, DWORD dwSize, std::vector<AddressInfo>& vecResult)
{
    MEMORY_BASIC_INFORMATION mbi;
    ::ZeroMemory(&mbi, sizeof(mbi));

    std::vector<BYTE> vecData;
    LPBYTE lpMemAddress     = 0;
    BOOL bReadRet           = FALSE;

    while(::VirtualQueryEx(hProcess, lpMemAddress, &mbi, sizeof(mbi)))
    {
        if(mbi.Type == MEM_PRIVATE && (mbi.Protect & PAGE_EXECUTE) && (mbi.Protect & PAGE_READWRITE))
        {
            vecData.resize(mbi.RegionSize);
            if(::ReadProcessMemory(hProcess, lpMemAddress, &vecData[0], mbi.RegionSize, NULL))
            {
                for(DWORD i = 0; i < mbi.RegionSize; i+=dwSize)
                {
                    AddressInfo tAddress(hProcess, lpMemAddress);
                    if(lpTester(tAddress))
                    {
                        vecResult.push_back(tAddress);
                    }
                }
            }
        }
        lpMemAddress += mbi.RegionSize;
    }
    return true;
}

BOOL CozyKnightCore::Search(std::vector<AddressInfo>& vecSource, const MemoryTester lpTester)
{
    if(vecSource.size() == 0)
    {
        return FALSE;
    }

    vecSource.erase(std::remove_if(vecSource.begin(), vecSource.end(), lpTester), vecSource.end());
    return TRUE;
}

void CozyKnightCore::Attch(HANDLE hProcess)
{
    Detch();

    m_hTarget = hProcess;
}

void CozyKnightCore::Detch()
{
    if(m_hTarget != NULL)
    {
        ::CloseHandle(m_hTarget);
        m_hTarget = NULL;
    }
}