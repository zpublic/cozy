#include "stdafx.h"
#include "CozyKnightCore.h"
#include <vector>

BOOL CozyKnightCore::EnumMem(DWORD dwPid, EnumCallback lpfnCallback, DWORD dwSize)
{
    HANDLE hProcess = ::OpenProcess(PROCESS_ALL_ACCESS, FALSE, dwPid);
    if(hProcess == NULL)
    {
        return false;
    }

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
                    lpfnCallback(&vecData[i]);
                }
            }
        }
        lpMemAddress += mbi.RegionSize;
    }
    return true;
}