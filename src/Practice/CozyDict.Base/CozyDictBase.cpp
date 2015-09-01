// CozyDictBase.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDictBase.h"

HHOOK CozyDictBase::m_hHook = nullptr;

CozyDictBase::MouseHookCallback CozyDictBase::m_lpMouseCallback = nullptr;

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
