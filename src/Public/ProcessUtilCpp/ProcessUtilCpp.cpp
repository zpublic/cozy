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

DWORD CProcessUtilCpp::ProcessEnum(PROCESSENUMPROC lpEnumFunc)
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

bool CProcessUtilCpp::ProcessTerminate(DWORD dwProcessId)
{
    HANDLE hProcess = ::OpenProcess(PROCESS_TERMINATE, false, dwProcessId);
    if (hProcess == nullptr)
    {
        ::CloseHandle(hProcess);
        return false;
    }
    if (!TerminateProcess(hProcess, 0))
    {
        ::CloseHandle(hProcess);
        return false;
    }
    ::CloseHandle(hProcess);
    return true;
}

bool CProcessUtilCpp::ProcessTerminateWithTimeOut(DWORD dwProcessId, DWORD dwTimeOut)
{
    HANDLE hProcess = ::OpenProcess(PROCESS_TERMINATE, false, dwProcessId);
    if (hProcess == nullptr)
    {
        ::CloseHandle(hProcess);
        return false;
    }
    if (!TerminateProcess(hProcess, 0))
    {
        ::CloseHandle(hProcess);
        return false;
    }
    DWORD dwResult = ::WaitForSingleObject(hProcess, dwTimeOut);
    ::CloseHandle(hProcess);
    return (dwResult == WAIT_OBJECT_0);
}

bool CProcessUtilCpp::ProcessCreate(LPTSTR lpPath)
{
    STARTUPINFO si = { sizeof(STARTUPINFO) };
    PROCESS_INFORMATION piProcess;
    bool bResult = (::CreateProcess(nullptr, lpPath, nullptr, nullptr, false, 0, nullptr, nullptr, &si, &piProcess) == TRUE);
    ::CloseHandle(piProcess.hProcess);
    ::CloseHandle(piProcess.hThread);
    return bResult;
}

bool CProcessUtilCpp::GetProcessName(DWORD dwProcessId)
{
    // TODO get process name by processid
    /*HANDLE hProcess = ::OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, dwProcessId);
    if (hProcess == nullptr)
    {
        ::CloseHandle(hProcess);
        return false;
    }
*/
}

PROCESSUTILCPP_API DWORD ProcessEnum(PROCESSENUMPROC lpEnumFunc)
{
    return CProcessUtilCppInstance.ProcessEnum(lpEnumFunc);
}

PROCESSUTILCPP_API bool ProcessTerminate(DWORD dwProcessId)
{
    return CProcessUtilCppInstance.ProcessTerminate(dwProcessId);
}

PROCESSUTILCPP_API bool ProcessTerminateWithTimeOut(DWORD dwProcessId, DWORD dwTimeOut)
{
    return CProcessUtilCppInstance.ProcessTerminateWithTimeOut(dwProcessId, dwTimeOut);
}

PROCESSUTILCPP_API bool ProcessCreate(LPTSTR lpPath)
{
    return CProcessUtilCppInstance.ProcessCreate(lpPath);
}

PROCESSUTILCPP_API bool GetProcessName(DWORD dwProcessId)
{
    return CProcessUtilCppInstance.GetProcessName(dwProcessId);
}