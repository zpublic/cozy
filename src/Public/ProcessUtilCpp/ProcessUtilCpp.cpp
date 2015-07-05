// ProcessUtilCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "ProcessUtilCpp.h"
#include "tlhelp32.h"
#include "Psapi.h"

PROCESSUTILCPP_API CProcessUtilCpp CProcessUtilCppInstance;

CProcessUtilCpp::CProcessUtilCpp(void)
{
    return;
}

bool CProcessUtilCpp::ProcessEnum(PROCESSENUMPROC lpEnumFunc)
{
    PROCESSENTRY32 pe32;
    pe32.dwSize = sizeof(pe32);
    HANDLE hProcessSnap = ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (hProcessSnap == INVALID_HANDLE_VALUE)
    {
        ::CloseHandle(hProcessSnap);
        return false;
    }

    DWORD dwCount = 0;
    bool bMore = (::Process32First(hProcessSnap, &pe32) == TRUE);
    while (bMore)
    {
        ++dwCount;
        if (lpEnumFunc(pe32.th32ProcessID))
        {
            break;
        }
        bMore = (::Process32Next(hProcessSnap, &pe32) == TRUE);
    }
    ::CloseHandle(hProcessSnap);
    return true;
}

bool CProcessUtilCpp::ProcessTerminate(DWORD dwProcessId)
{
    HANDLE hProcess = ::OpenProcess(PROCESS_TERMINATE, false, dwProcessId);
    if (hProcess == nullptr)
    {
        ::CloseHandle(hProcess);
        return false;
    }
    bool bResult = (::TerminateProcess(hProcess, 0) != FALSE);
    ::CloseHandle(hProcess);
    return bResult;
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

bool CProcessUtilCpp::GetProcessName(DWORD dwProcessId, PROCESSNAMEPROC lpNameFunc)
{
    if (lpNameFunc == nullptr) return false;
    HANDLE hProcess = ::OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, dwProcessId);
    if (hProcess == nullptr)
    {
        ::CloseHandle(hProcess);
        return false;
    }

    LPTSTR lpResult = new TCHAR[260];
    DWORD dwCount = ::GetModuleFileNameEx(hProcess, nullptr, lpResult, 260);
    lpNameFunc(lpResult);
    delete[] lpResult;
    ::CloseHandle(hProcess);
    return (dwCount != 0);
}

PROCESSUTILCPP_API bool ProcessEnum(PROCESSENUMPROC lpEnumFunc)
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

PROCESSUTILCPP_API bool GetProcessName(DWORD dwProcessId, PROCESSNAMEPROC lpNameFunc)
{
    return CProcessUtilCppInstance.GetProcessName(dwProcessId, lpNameFunc);
}