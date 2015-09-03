#ifndef __COZY_DICK_HOOK__
#define __COZY_DICK_HOOK__

#define COZYDICTEXPORT
#ifndef COZYDICTEXPORT
#define COZYDICTAPI _declspec(dllexport)
#else
#define COZYDICTAPI _declspec(dllexport) 
#endif

#include "windows.h"

namespace zl
{
    namespace Ipc
    {
        class ipcCallClient;
    }
}

class CozyDictHook
{
public:
    typedef BOOL(CALLBACK * _TextOutA)(HDC /*hdc*/, int /*x*/, int /*y*/, LPCSTR /*lpString*/, int /*c*/);

    typedef BOOL(CALLBACK * _TextOutW)(HDC /*hdc*/, int /*x*/, int /*y*/, LPCWSTR /*lpString*/, int /*c*/);

    typedef BOOL(CALLBACK * _ExtTextOutA)(HDC /*hdc*/, int /*x*/, int /*y*/, UINT /*options*/, const RECT * /*lprect*/, LPCSTR /*lpString*/, UINT /*c*/, const INT * /*lpDx*/);

    typedef BOOL(CALLBACK * _ExtTextOutW)(HDC /*hdc*/, int /*x*/, int /*y*/, UINT /*options*/, const RECT * /*lprect*/, LPCWSTR /*lpString*/, UINT /*c*/, const INT * /*lpDx*/);
public: 
    CozyDictHook();
    ~CozyDictHook();

    void InitHookEnv();

    bool SetTextOutAHook();
    bool SetTextOutWHook();
    bool SetExtTextOutAHook();
    bool SetExtTextOutWHook();

    bool UnsetAllApiHook();

    bool SetCBTHook();

    bool UnsetCBTHook();

    static void SetHInstance(HINSTANCE hInstance);

public:
    static bool StartPipe();
    static bool StopPipe();
    static bool SendPipeData(LPCTSTR lpBytes, DWORD dwSize);

public:
    static BOOL CALLBACK TextOutAProc(HDC hdc, int x, int y, LPCSTR lpString, int c);
    static BOOL CALLBACK TextOutWProc(HDC hdc, int x, int y, LPCWSTR lpString, int c);
    static BOOL CALLBACK ExtTextOutAProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCSTR lpString, UINT c, const INT * lpDx);
    static BOOL CALLBACK ExtTextOutWProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCWSTR lpString, UINT c, const INT * lpDx);

    static LRESULT CALLBACK CBTHookProc(int nCode, WPARAM wParam, LPARAM lParam);
private:
    static _TextOutA m_lpTrueTextOutA;
    static _TextOutW m_lpTrueTextOutW;
    static _ExtTextOutA m_lpTrueExtTextOutA;
    static _ExtTextOutW m_lpTrueExtTextOutW;

    static HINSTANCE m_hInstance;

private:
    static zl::Ipc::ipcCallClient* m_lpPipeClt;
};


EXTERN_C COZYDICTAPI void InitHookEnv();
EXTERN_C COZYDICTAPI bool SetAllHook();
EXTERN_C COZYDICTAPI bool UnsetAllHook();
EXTERN_C COZYDICTAPI bool SetCBTHook();
EXTERN_C COZYDICTAPI bool UnSetCBTHook();

#endif // __COZY_DICK_HOOK__
