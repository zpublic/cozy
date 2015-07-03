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

class CProcessUtilCpp
{
public:
    CProcessUtilCpp(void);
    DWORD EnumProcess(PROCESSENUMPROC lpEnumFunc);
};

extern PROCESSUTILCPP_API CProcessUtilCpp CProcessUtilCppInstance;

extern "C" PROCESSUTILCPP_API DWORD EnumProcess(PROCESSENUMPROC lpEnumFunc);
