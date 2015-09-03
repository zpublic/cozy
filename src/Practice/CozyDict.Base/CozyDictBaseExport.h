#ifndef __COZY_DICT_BASE_EXPORT__
#define __COZY_DICT_BASE_EXPORT__

#define COZYDICTEXPORT
#ifndef COZYDICTEXPORT
#define COZYDICTAPI _declspec(dllexport)
#else
#define COZYDICTAPI _declspec(dllexport) 
#endif

#include "CozyDictBase.h"

extern "C" COZYDICTAPI bool SetMouseHook(CozyDictBase::MouseHookCallback lpCallback);
extern "C" COZYDICTAPI bool UnSetMouseHook();
extern "C" COZYDICTAPI bool InvalidateMouseWindow(int nXpos, int nYPos);
extern "C" COZYDICTAPI bool StartPipe();
extern "C" COZYDICTAPI bool StopPipe();
extern "C" COZYDICTAPI void SetIPCCallback(CozyDictBase::IPCProcCallback lpCallback);
extern "C" COZYDICTAPI DWORD GetMouseWindowPid(int xPox, int yPos);

#endif // __COZY_DICT_BASE_EXPORT__
