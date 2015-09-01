// CozyDictBase.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDictBase.h"

CozyDictBase::MouseHookCallback CozyDictBase::m_lpMouseCallback = nullptr;
HHOOK CozyDictBase::m_hHook = nullptr;

COZYDICTAPI CozyDictBase CozyDictBaseInstance;

CozyDictBase::CozyDictBase()
{

}

CozyDictBase::~CozyDictBase()
{

}

LRESULT WINAPI CozyDictBase::MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
    if (nCode > 0)
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
        if (::UnhookWindowsHookEx(m_hHook) == TRUE)
        {
            return true;
        }
    }
    return false;
}


COZYDICTAPI bool SetMouseHook(CozyDictBase::MouseHookCallback lpCallback)
{
    return CozyDictBaseInstance.SetMouseHook(lpCallback);
}

COZYDICTAPI bool UnSetMouseHook()
{
    return CozyDictBaseInstance.UnSetMouseHook();
}