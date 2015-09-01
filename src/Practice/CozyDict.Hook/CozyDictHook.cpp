// CozyDictHook.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDictHook.h"

#include "3rd/mhook-lib/mhook.h"

CozyDictHook::_TextOutA CozyDictHook::m_lpTrueTextOutA = nullptr;
CozyDictHook::_TextOutW CozyDictHook::m_lpTrueTextOutW = nullptr;
CozyDictHook::_ExtTextOutA CozyDictHook::m_lpTrueExtTextOutA = nullptr;
CozyDictHook::_ExtTextOutW CozyDictHook::m_lpTrueExtTextOutW = nullptr;

CozyDictHook::_TextOutA CozyDictHook::m_lpTextOutACallback = nullptr;
CozyDictHook::_TextOutW CozyDictHook::m_lpTextOutWCallback = nullptr;
CozyDictHook::_ExtTextOutA CozyDictHook::m_lpExtTextOutACallback = nullptr;
CozyDictHook::_ExtTextOutW CozyDictHook::m_lpExtTextOutWCallback = nullptr;

COZYDICTAPI CozyDictHook CozyDictHookInstance;

CozyDictHook::CozyDictHook()
{

}

CozyDictHook::~CozyDictHook()
{

}

void CozyDictHook::ResetAPIHookCallback()
{
    m_lpTextOutACallback = nullptr;
    m_lpTextOutWCallback = nullptr;
    m_lpExtTextOutACallback = nullptr;
    m_lpExtTextOutWCallback = nullptr;
}

void CozyDictHook::InitHookEnv()
{
    m_lpTrueTextOutA = reinterpret_cast<_TextOutA>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "TextOutA"));
    m_lpTrueTextOutW = reinterpret_cast<_TextOutW>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "TextOutW"));
    m_lpTrueExtTextOutA = reinterpret_cast<_ExtTextOutA>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "ExtTextOutA"));
    m_lpTrueExtTextOutW = reinterpret_cast<_ExtTextOutW>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "ExtTextOutW"));
}

bool CozyDictHook::SetTextOutAHook(_TextOutA lpCallback)
{
    m_lpTextOutACallback = lpCallback;

    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueTextOutA), TextOutAProc))
    {
        return true;
    }
    return false;
}

bool CozyDictHook::SetTextOutWHook(_TextOutW lpCallback)
{
    m_lpTextOutWCallback = lpCallback;

    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueTextOutW), TextOutWProc))
    {
        return true;
    }
    return false;
}

bool CozyDictHook::SetExtTextOutAHook(_ExtTextOutA lpCallback)
{
    m_lpExtTextOutACallback = lpCallback;

    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueExtTextOutA), ExtTextOutAProc))
    {
        return true;
    }
    return false;
}
bool CozyDictHook::SetExtTextOutWHook(_ExtTextOutW lpCallback)
{
    m_lpExtTextOutWCallback = lpCallback;

    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueExtTextOutW), ExtTextOutWProc))
    {
        return true;
    }
    return false;
}

bool CozyDictHook::UnsetAllApiHook()
{
    bool bFlag = true;

    if (!Mhook_Unhook(reinterpret_cast<LPVOID*>(m_lpTrueTextOutA)))
    {
        bFlag = false;
    }
    if (Mhook_Unhook(reinterpret_cast<LPVOID*>(m_lpTrueTextOutW)))
    {
        bFlag = false;
    }
    if (Mhook_Unhook(reinterpret_cast<LPVOID*>(m_lpTrueExtTextOutA)))
    {
        bFlag = false;
    }
    if (Mhook_Unhook(reinterpret_cast<LPVOID*>(m_lpTrueExtTextOutW)))
    {
        bFlag = false;
    }
    if (bFlag)
    {
        ResetAPIHookCallback();
    }
    return bFlag;
}

BOOL WINAPI CozyDictHook::TextOutAProc(HDC hdc, int x, int y, LPCSTR lpString, int c)
{
    if (m_lpTextOutACallback != nullptr)
    {
        m_lpTextOutACallback(hdc, x, y, lpString, c);
    }
    return m_lpTrueTextOutA(hdc, x, y, lpString, c);
}

BOOL WINAPI CozyDictHook::TextOutWProc(HDC hdc, int x, int y, LPCWSTR lpString, int c)
{
    if (m_lpTextOutWCallback != nullptr)
    {
        m_lpTextOutWCallback(hdc, x, y, lpString, c);
    }
    MessageBox(0, 0, 0, 0);
    return m_lpTrueTextOutW(hdc, x, y, lpString, c);
}

BOOL WINAPI CozyDictHook::ExtTextOutAProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCSTR lpString, UINT c, const INT * lpDx)
{
    if (m_lpExtTextOutACallback != nullptr)
    {
        m_lpExtTextOutACallback(hdc, x, y, options, lprect, lpString, c, lpDx);
    }
    return m_lpTrueExtTextOutA(hdc, x, y, options, lprect, lpString, c, lpDx);
}

BOOL WINAPI CozyDictHook::ExtTextOutWProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCWSTR lpString, UINT c, const INT * lpDx)
{
    if (m_lpExtTextOutWCallback != nullptr)
    {
        m_lpExtTextOutWCallback(hdc, x, y, options, lprect, lpString, c, lpDx);
    }
    return m_lpTrueExtTextOutW(hdc, x, y, options, lprect, lpString, c, lpDx);
}

COZYDICTAPI void ResetAPIHookCallback()
{
    CozyDictHookInstance.ResetAPIHookCallback();
}

COZYDICTAPI void InitHookEnv()
{
    CozyDictHookInstance.InitHookEnv();
}

COZYDICTAPI bool SetTextOutAHook(CozyDictHook::_TextOutA lpCallback)
{
    return CozyDictHookInstance.SetTextOutAHook(lpCallback);
}

COZYDICTAPI bool SetTextOutWHook(CozyDictHook::_TextOutW lpCallback)
{
    return CozyDictHookInstance.SetTextOutWHook(lpCallback);
}

COZYDICTAPI bool SetExtTextOutAHook(CozyDictHook::_ExtTextOutA lpCallback)
{
    return CozyDictHookInstance.SetExtTextOutAHook(lpCallback);
}

COZYDICTAPI bool SetExtTextOutWHook(CozyDictHook::_ExtTextOutW lpCallback)
{
    return CozyDictHookInstance.SetExtTextOutWHook(lpCallback);
}

COZYDICTAPI bool UnsetAllHook()
{
    return CozyDictHookInstance.UnsetAllApiHook();
}