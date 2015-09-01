#ifndef __COZY_DICT_BASE__
#define __COZY_DICT_BASE__

#define COZYDICTEXPORT
#ifndef COZYDICTEXPORT
#define COZYDICTAPI _declspec(dllexport)
#else
#define COZYDICTAPI _declspec(dllexport) 
#endif

#include "windows.h"

class CozyDictBase
{
public:
    typedef LRESULT(CALLBACK * MouseHookCallback)(int /*nCode*/, WPARAM /*wParam*/, LPARAM /*lParam*/);

public:
    CozyDictBase();
    ~CozyDictBase();

    // MouseHook
    bool SetMouseHook(MouseHookCallback MouseCallback);

    bool UnSetMouseHook();

    static LRESULT WINAPI MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam);

private:
    static MouseHookCallback m_lpMouseCallback;
    static HHOOK m_hHook;
};

EXTERN_C COZYDICTAPI bool SetMouseHook(CozyDictBase::MouseHookCallback lpCallback);
EXTERN_C COZYDICTAPI bool UnSetMouseHook();

#endif