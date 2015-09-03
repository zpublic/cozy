#include "CozyDictHookExport.h"
#include "CozyDictHook.h"

COZYDICTAPI CozyDictHook CozyDictHookInstance;

void CozyDictHook::ProcessAttach()
{
    CozyDictHookInstance.InitHookEnv();
    CozyDictHookInstance.SetAllHook();
    CozyDictHookInstance.StartPipe();
}

void CozyDictHook::ProcessDetach()
{
    CozyDictHookInstance.StopPipe();
    CozyDictHookInstance.UnsetAllApiHook();
}

COZYDICTAPI bool SetCBTHook()
{
    return CozyDictHookInstance.SetCBTHook();
}

COZYDICTAPI bool UnSetCBTHook()
{
    return CozyDictHookInstance.UnsetCBTHook();
}