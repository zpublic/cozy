// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the FILEUTILCPP_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// PROCESSUTILCPP_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef PROCESSUTILCPP_EXPORTS
#define PROCESSUTILCPP_API __declspec(dllexport)
#else
#define PROCESSUTILCPP_API __declspec(dllimport)
#endif

#include "windows.h"

using PROCESSENUMPROC = bool(CALLBACK*)(DWORD dwProcessId);
using PROCESSNAMEPROC = void(CALLBACK*)(LPTSTR lpProcName);

class CProcessUtilCpp
{
public:
    CProcessUtilCpp(void);

    bool ProcessEnum(PROCESSENUMPROC lpEnumFunc);

    bool ProcessTerminate(DWORD dwProcessId);

    bool ProcessTerminateWithTimeOut(DWORD dwProcessId, DWORD dwTimeOut);

    bool ProcessCreate(LPTSTR lpPath);

    bool GetProcessName(DWORD dwProcessId, PROCESSNAMEPROC lpNameFunc);
};

extern PROCESSUTILCPP_API CProcessUtilCpp CProcessUtilCppInstance;

extern "C" PROCESSUTILCPP_API bool ProcessEnum(PROCESSENUMPROC lpEnumFunc);

extern "C" PROCESSUTILCPP_API bool ProcessTerminate(DWORD dwProcessId);

extern "C" PROCESSUTILCPP_API bool ProcessTerminateWithTimeOut(DWORD dwProcessId, DWORD dwTimeOut);

extern "C" PROCESSUTILCPP_API bool ProcessCreate(LPTSTR lpPath);

extern "C" PROCESSUTILCPP_API bool GetProcessName(DWORD dwProcessId, PROCESSNAMEPROC lpNameFunc);