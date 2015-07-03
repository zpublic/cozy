// ProcessUtilCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "ProcessUtilCpp.h"
#include "tlhelp32.h"

PROCESSUTILCPP_API CProcessUtilCpp CProcessUtilCppInstance;

CProcessUtilCpp::CProcessUtilCpp(void)
{
    return;
}

DWORD CProcessUtilCpp::EnumProcess(PROCESSENUMPROC lpEnumFunc)
{
    PROCESSENTRY32 pe32;
    pe32.dwSize = sizeof(pe32);
    HANDLE hProcessSnap = ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (hProcessSnap == INVALID_HANDLE_VALUE)
    {
        ::CloseHandle(hProcessSnap);
        return 0;
    }

    DWORD dwCount = 0;
    bool bMore = (::Process32First(hProcessSnap, &pe32) == TRUE);
    while (bMore)
    {
        ++dwCount;
        lpEnumFunc(pe32.th32ProcessID);
        bMore = (::Process32Next(hProcessSnap, &pe32) == TRUE);
    }
    ::CloseHandle(hProcessSnap);
    return dwCount;
}

PROCESSUTILCPP_API DWORD EnumProcess(PROCESSENUMPROC lpEnumFunc)
{
    return CProcessUtilCppInstance.EnumProcess(lpEnumFunc);
}