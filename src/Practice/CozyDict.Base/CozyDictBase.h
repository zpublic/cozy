#ifndef __COZY_DICT_BASE__
#define __COZY_DICT_BASE__

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

#endif