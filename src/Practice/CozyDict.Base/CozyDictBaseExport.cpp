#include "CozyDictBaseExport.h"
#include "jsonparser/ipcjsonconvert.h"
#include "ipcjsonprocessor.h"
#include "jsonparser/ipcfuncmgr.h"

COZYDICTAPI CozyDictBase CozyDictBaseInstance;

COZYDICTAPI bool SetMouseHook(CozyDictBase::MouseHookCallback lpCallback)
{
    return CozyDictBaseInstance.SetMouseHook(lpCallback);
}

COZYDICTAPI bool UnSetMouseHook()
{
    return CozyDictBaseInstance.UnSetMouseHook();
}

COZYDICTAPI bool InvalidateMouseWindow(int nXpos, int nYPos)
{
    return CozyDictBaseInstance.InvalidateMouseWindow(nXpos, nYPos);
}

COZYDICTAPI bool StartPipe()
{
    return CozyDictBaseInstance.StartPipe();
}

COZYDICTAPI bool StopPipe()
{
    return CozyDictBaseInstance.StopPipe();
}

COZYDICTAPI void SetIPCCallback(CozyDictBase::IPCProcCallback lpCallback)
{
    return CozyDictBaseInstance.SetIPCCallback(lpCallback);
}

COZYDICTAPI DWORD GetMouseWindowPid(int xPos, int yPos)
{
    return CozyDictBaseInstance.GetMouseWindowPid(xPos, yPos);
}

int IPCProc(LPCTSTR lpString, DWORD dwPid)
{
    return CozyDictBase::IPCProc(lpString, dwPid);
}

EXPORT_GLOBAL_FUNC_2(IPCProc, int, LPCTSTR, DWORD);
