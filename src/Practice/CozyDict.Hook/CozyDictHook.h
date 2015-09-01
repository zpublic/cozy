#ifndef __COZY_DICK_HOOK__
#define __COZY_DICK_HOOK__

#define COZYDICTEXPORT
#ifndef COZYDICTEXPORT
#define COZYDICTAPI _declspec(dllexport)
#else
#define COZYDICTAPI _declspec(dllexport) 
#endif

#include "windows.h"

class CozyDictHook
{
public:
    typedef BOOL(WINAPI * _TextOutA)(HDC /*hdc*/, int /*x*/, int /*y*/, LPCSTR /*lpString*/, int /*c*/);

    typedef BOOL(WINAPI * _TextOutW)(HDC /*hdc*/, int /*x*/, int /*y*/, LPCWSTR /*lpString*/, int /*c*/);

    typedef BOOL(WINAPI * _ExtTextOutA)(HDC /*hdc*/, int /*x*/, int /*y*/, UINT /*options*/, const RECT * /*lprect*/, LPCSTR /*lpString*/, UINT /*c*/, const INT * /*lpDx*/);

    typedef BOOL(WINAPI * _ExtTextOutW)(HDC /*hdc*/, int /*x*/, int /*y*/, UINT /*options*/, const RECT * /*lprect*/, LPCWSTR /*lpString*/, UINT /*c*/, const INT * /*lpDx*/);
public: 
    CozyDictHook();
    ~CozyDictHook();

    void ResetAPIHookCallback();
    void InitHookEnv();

    bool SetTextOutAHook(_TextOutA lpCallback);
    bool SetTextOutWHook(_TextOutW lpCallback);
    bool SetExtTextOutAHook(_ExtTextOutA lpCallback);
    bool SetExtTextOutWHook(_ExtTextOutW lpCallback);

    bool UnsetAllApiHook();

public:
    static BOOL WINAPI TextOutAProc(HDC hdc, int x, int y, LPCSTR lpString, int c);
    static BOOL WINAPI TextOutWProc(HDC hdc, int x, int y, LPCWSTR lpString, int c);
    static BOOL WINAPI ExtTextOutAProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCSTR lpString, UINT c, const INT * lpDx);
    static BOOL WINAPI ExtTextOutWProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCWSTR lpString, UINT c, const INT * lpDx);

private:
    static _TextOutA m_lpTrueTextOutA;
    static _TextOutW m_lpTrueTextOutW;
    static _ExtTextOutA m_lpTrueExtTextOutA;
    static _ExtTextOutW m_lpTrueExtTextOutW;

private:

    static _TextOutA m_lpTextOutACallback;
    static _TextOutW m_lpTextOutWCallback;
    static _ExtTextOutA m_lpExtTextOutACallback;
    static _ExtTextOutW m_lpExtTextOutWCallback;
};


EXTERN_C COZYDICTAPI void ResetAPIHookCallback();
EXTERN_C COZYDICTAPI void InitHookEnv();
EXTERN_C COZYDICTAPI bool SetTextOutAHook(CozyDictHook::_TextOutA lpCallback);
EXTERN_C COZYDICTAPI bool SetTextOutWHook(CozyDictHook::_TextOutW lpCallback);
EXTERN_C COZYDICTAPI bool SetExtTextOutAHook(CozyDictHook::_ExtTextOutA lpCallback);
EXTERN_C COZYDICTAPI bool SetExtTextOutWHook(CozyDictHook::_ExtTextOutW lpCallback);
EXTERN_C COZYDICTAPI bool UnsetAllHook();

#endif // __COZY_DICK_HOOK__
