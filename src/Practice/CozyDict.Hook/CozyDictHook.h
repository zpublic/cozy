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
        class ipcPipeCltChannel;
    }
}

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

    void InitHookEnv();

    bool SetTextOutAHook();
    bool SetTextOutWHook();
    bool SetExtTextOutAHook();
    bool SetExtTextOutWHook();

    bool UnsetAllApiHook();

public:
    static bool StartPipe();
    static bool StopPipe();
    static bool SendPipeData(LPVOID lpBytes, DWORD dwSize);

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
    static zl::Ipc::ipcPipeCltChannel* m_lpPipeClt;
};


EXTERN_C COZYDICTAPI void InitHookEnv();
EXTERN_C COZYDICTAPI bool SetTextOutAHook();
EXTERN_C COZYDICTAPI bool SetTextOutWHook();
EXTERN_C COZYDICTAPI bool SetExtTextOutAHook();
EXTERN_C COZYDICTAPI bool SetExtTextOutWHook();
EXTERN_C COZYDICTAPI bool UnsetAllHook();
EXTERN_C COZYDICTAPI bool SetAllHook();

#endif // __COZY_DICK_HOOK__
