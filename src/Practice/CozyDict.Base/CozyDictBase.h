#ifndef __COZY_DICT_BASE__
#define __COZY_DICT_BASE__

#define COZYDICTEXPORT
#ifndef COZYDICTEXPORT
#define COZYDICTAPI _declspec(dllexport)
#else
#define COZYDICTAPI _declspec(dllexport) 
#endif

namespace zl
{
    namespace Ipc
    {
        class ipcPipeSvr;
    }
}

#include "windows.h"

class CozyDictBase
{
public:
    typedef LRESULT(CALLBACK * MouseHookCallback)(int /*nCode*/, WPARAM /*wParam*/, LPARAM /*lParam*/);
    typedef int (CALLBACK * IPCProcCallback) (LPCTSTR lpString, DWORD dwPid);

public:
    CozyDictBase();
    ~CozyDictBase();

    bool SetMouseHook(MouseHookCallback MouseCallback);

    bool UnSetMouseHook();

    bool InvalidateMouseWindow(int nXpos, int nYPos);

public:
    bool StartPipe();
    bool StopPipe();

    void SetIPCCallback(IPCProcCallback lpCallback);

    static int IPCProc(LPCTSTR lpString, DWORD dwPid);

    static DWORD GetMouseWindowPid(int xPox, int yPos);

private:
    static LRESULT WINAPI MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam);

    static HHOOK m_hHook;
    static MouseHookCallback m_lpMouseCallback;
    static IPCProcCallback m_lpIpcCallback;

    zl::Ipc::ipcPipeSvr* m_lpPipeSvr;
};

EXTERN_C COZYDICTAPI bool SetMouseHook(CozyDictBase::MouseHookCallback lpCallback);
EXTERN_C COZYDICTAPI bool UnSetMouseHook();
EXTERN_C COZYDICTAPI bool InvalidateMouseWindow(int nXpos, int nYPos);
EXTERN_C COZYDICTAPI bool StartPipe();
EXTERN_C COZYDICTAPI bool StopPipe();
EXTERN_C COZYDICTAPI void SetIPCCallback(CozyDictBase::IPCProcCallback lpCallback);
EXTERN_C COZYDICTAPI DWORD GetMouseWindowPid(int xPox, int yPos);

#endif