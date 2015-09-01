// CozyDictBase.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDictBase.h"

#include "3rd/mhook-lib/mhook.h"

HHOOK CozyDictBase::m_hHook = nullptr;

CozyDictBase::MouseHookCallback CozyDictBase::m_lpMouseCallback     = nullptr;
CozyDictBase::_TextOutA CozyDictBase::m_lpTrueTextOutA              = nullptr;
CozyDictBase::_TextOutW CozyDictBase::m_lpTrueTextOutW              = nullptr;
CozyDictBase::_ExtTextOutA CozyDictBase::m_lpTrueExtTextOutA        = nullptr;
CozyDictBase::_ExtTextOutW CozyDictBase::m_lpTrueExtTextOutW        = nullptr;

CozyDictBase::_TextOutA CozyDictBase::m_lpTextOutACallback          = nullptr;
CozyDictBase::_TextOutW CozyDictBase::m_lpTextOutWCallback          = nullptr;
CozyDictBase::_ExtTextOutA CozyDictBase::m_lpExtTextOutACallback    = nullptr;
CozyDictBase::_ExtTextOutW CozyDictBase::m_lpExtTextOutWCallback    = nullptr;

COZYDICTAPI CozyDictBase CozyDictBaseInstance;

CozyDictBase::CozyDictBase()
{

}

CozyDictBase::~CozyDictBase()
{

}

LRESULT WINAPI CozyDictBase::MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
    if (nCode >= 0)
    {
        if (m_lpMouseCallback != nullptr)
        {
            m_lpMouseCallback(nCode, wParam, lParam);
        }
    }
    return ::CallNextHookEx(m_hHook, nCode, wParam, lParam);
}

bool CozyDictBase::SetMouseHook(MouseHookCallback lpMouseCallback)
{
    m_lpMouseCallback = lpMouseCallback;
    m_hHook = ::SetWindowsHookEx(WH_MOUSE_LL, MouseHookProc, nullptr, 0);
    if (m_hHook == nullptr)
    {
        return false;
    }
    return true;
}

bool CozyDictBase::UnSetMouseHook()
{
    if (m_hHook != nullptr)
    {
        if (::UnhookWindowsHookEx(m_hHook))
        {
            return true;
        }
    }
    return false;
}

bool CozyDictBase::InvalidateMouseWindow(int nXpos, int nYPos)
{
    POINT point{ nXpos, nYPos };
    HWND hWnd = ::WindowFromPoint(point);
    if (hWnd == nullptr)
    {
        return false;
    }
    if (!::ScreenToClient(hWnd, &point))
    {
        return false;
    }

    RECT rect;
    rect.left = point.x;
    rect.top = point.y;
    rect.right = rect.left + 16;
    rect.bottom = rect.top + 16;

    if (!::InvalidateRect(hWnd, &rect, false))
    {
        return false;
    }
    return true;
}

void CozyDictBase::ResetAPIHookCallback()
{
    m_lpTextOutACallback = nullptr;
    m_lpTextOutWCallback = nullptr;
    m_lpExtTextOutACallback = nullptr;
    m_lpExtTextOutWCallback = nullptr;
}

void CozyDictBase::InitHookEnv()
{
    m_lpTrueTextOutA    = reinterpret_cast<_TextOutA>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "TextOutA"));
    m_lpTrueTextOutW    = reinterpret_cast<_TextOutW>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "TextOutW"));
    m_lpTrueExtTextOutA = reinterpret_cast<_ExtTextOutA>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "ExtTextOutA"));
    m_lpTrueExtTextOutW = reinterpret_cast<_ExtTextOutW>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "ExtTextOutW"));
}

bool CozyDictBase::SetTextOutAHook(_TextOutA lpCallback)
{
    m_lpTextOutACallback = lpCallback;

    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueTextOutA), TextOutAProc))
    {
        return true;
    }
    return false;
}

bool CozyDictBase::SetTextOutWHook(_TextOutW lpCallback)
{
    m_lpTextOutWCallback = lpCallback;

    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueTextOutW), TextOutWProc))
    {
        return true;
    }
    return false;
}

bool CozyDictBase::SetExtTextOutAHook(_ExtTextOutA lpCallback)
{
    m_lpExtTextOutACallback = lpCallback;

    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueExtTextOutA), ExtTextOutAProc))
    {
        return true;
    }
    return false;
}
bool CozyDictBase::SetExtTextOutWHook(_ExtTextOutW lpCallback)
{
    m_lpExtTextOutWCallback = lpCallback;

    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueExtTextOutW), ExtTextOutWProc))
    {
        return true;
    }
    return false;
}

bool CozyDictBase::UnsetAllApiHook()
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

BOOL WINAPI CozyDictBase::TextOutAProc(HDC hdc, int x, int y, LPCSTR lpString, int c)
{
    if (m_lpTextOutACallback != nullptr)
    {
        m_lpTextOutACallback(hdc, x, y, lpString, c);
    }
    return m_lpTrueTextOutA(hdc, x, y, lpString, c);
}

BOOL WINAPI CozyDictBase::TextOutWProc(HDC hdc, int x, int y, LPCWSTR lpString, int c)
{
    if (m_lpTextOutWCallback != nullptr)
    {
        m_lpTextOutWCallback(hdc, x, y, lpString, c);
    }
    return m_lpTrueTextOutW(hdc, x, y, lpString, c);
}

BOOL WINAPI CozyDictBase::ExtTextOutAProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCSTR lpString, UINT c, const INT * lpDx)
{
    if (m_lpExtTextOutACallback != nullptr)
    {
        m_lpExtTextOutACallback(hdc, x, y, options, lprect, lpString, c, lpDx);
    }
    return m_lpTrueExtTextOutA(hdc, x, y, options, lprect, lpString, c, lpDx);
}

BOOL WINAPI CozyDictBase::ExtTextOutWProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCWSTR lpString, UINT c, const INT * lpDx)
{
    if (m_lpExtTextOutWCallback != nullptr)
    {
        m_lpExtTextOutWCallback(hdc, x, y, options, lprect, lpString, c, lpDx);
    }
    return m_lpTrueExtTextOutW(hdc, x, y, options, lprect, lpString, c, lpDx);
}

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

COZYDICTAPI void ResetAPIHookCallback()
{
    CozyDictBaseInstance.ResetAPIHookCallback();
}

COZYDICTAPI void InitHookEnv()
{
    CozyDictBaseInstance.InitHookEnv();
}

COZYDICTAPI bool SetTextOutAHook(CozyDictBase::_TextOutA lpCallback)
{
    return CozyDictBaseInstance.SetTextOutAHook(lpCallback);
}

COZYDICTAPI bool SetTextOutWHook(CozyDictBase::_TextOutW lpCallback)
{
    return CozyDictBaseInstance.SetTextOutWHook(lpCallback);
}

COZYDICTAPI bool SetExtTextOutAHook(CozyDictBase::_ExtTextOutA lpCallback)
{
    return CozyDictBaseInstance.SetExtTextOutAHook(lpCallback);
}

COZYDICTAPI bool SetExtTextOutWHook(CozyDictBase::_ExtTextOutW lpCallback)
{
    return CozyDictBaseInstance.SetExtTextOutWHook(lpCallback);
}

COZYDICTAPI bool UnsetAllHook()
{
    return CozyDictBaseInstance.UnsetAllApiHook();
}