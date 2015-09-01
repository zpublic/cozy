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

    typedef BOOL(WINAPI * _TextOutA)(HDC /*hdc*/, int /*x*/, int /*y*/, LPCSTR /*lpString*/, int /*c*/);

    typedef BOOL(WINAPI * _TextOutW)(HDC /*hdc*/, int /*x*/, int /*y*/, LPCWSTR /*lpString*/, int /*c*/);

    typedef BOOL(WINAPI * _ExtTextOutA)(HDC /*hdc*/, int /*x*/, int /*y*/, UINT /*options*/, const RECT * /*lprect*/, LPCSTR /*lpString*/, UINT /*c*/, const INT * /*lpDx*/);

    typedef BOOL(WINAPI * _ExtTextOutW)(HDC /*hdc*/, int /*x*/, int /*y*/, UINT /*options*/, const RECT * /*lprect*/, LPCWSTR /*lpString*/, UINT /*c*/, const INT * /*lpDx*/);

public:
    CozyDictBase();
    ~CozyDictBase();

    // MouseHook

    bool SetMouseHook(MouseHookCallback MouseCallback);

    bool UnSetMouseHook();

    bool InvalidateMouseWindow(int nXpos, int nYPos);

    // API Hook

    void ResetAPIHookCallback();
    void InitHookEnv();

    bool SetTextOutAHook(_TextOutA lpCallback);
    bool SetTextOutWHook(_TextOutW lpCallback);
    bool SetExtTextOutAHook(_ExtTextOutA lpCallback);
    bool SetExtTextOutWHook(_ExtTextOutW lpCallback);

    bool UnsetAllApiHook();

private:
    static LRESULT WINAPI MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam);

    static BOOL WINAPI TextOutAProc(HDC hdc, int x, int y, LPCSTR lpString, int c);
    static BOOL WINAPI TextOutWProc(HDC hdc, int x, int y, LPCWSTR lpString, int c);
    static BOOL WINAPI ExtTextOutAProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCSTR lpString, UINT c, const INT * lpDx);
    static BOOL WINAPI ExtTextOutWProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCWSTR lpString, UINT c, const INT * lpDx);

private:
    static HHOOK m_hHook;

    static _TextOutA m_lpTrueTextOutA;
    static _TextOutW m_lpTrueTextOutW;
    static _ExtTextOutA m_lpTrueExtTextOutA;
    static _ExtTextOutW m_lpTrueExtTextOutW;

private:
    static MouseHookCallback m_lpMouseCallback;
    static _TextOutA m_lpTextOutACallback;
    static _TextOutW m_lpTextOutWCallback;
    static _ExtTextOutA m_lpExtTextOutACallback;
    static _ExtTextOutW m_lpExtTextOutWCallback;
};

EXTERN_C COZYDICTAPI bool SetMouseHook(CozyDictBase::MouseHookCallback lpCallback);
EXTERN_C COZYDICTAPI bool UnSetMouseHook();
EXTERN_C COZYDICTAPI bool InvalidateMouseWindow(int nXpos, int nYPos);

EXTERN_C COZYDICTAPI void ResetAPIHookCallback();
EXTERN_C COZYDICTAPI void InitHookEnv();
EXTERN_C COZYDICTAPI bool SetTextOutAHook(CozyDictBase::_TextOutA lpCallback);
EXTERN_C COZYDICTAPI bool SetTextOutWHook(CozyDictBase::_TextOutW lpCallback);
EXTERN_C COZYDICTAPI bool SetExtTextOutAHook(CozyDictBase::_ExtTextOutA lpCallback);
EXTERN_C COZYDICTAPI bool SetExtTextOutWHook(CozyDictBase::_ExtTextOutW lpCallback);
EXTERN_C COZYDICTAPI bool UnsetAllHook();

#endif