// CozyDictBase.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDictBase.h"

#include "ipcpipesvrchannel.h"

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

bool CozyDictBase::StartPipe()
{
    HANDLE hPipe = CreateNamedPipe(TEXT("\\\\.\\pipe\\CozyDictPipe"), PIPE_ACCESS_DUPLEX, PIPE_TYPE_BYTE | PIPE_READMODE_BYTE, 1, 0, 0, 1000, NULL);
    if (hPipe == INVALID_HANDLE_VALUE)
    {
        return false;
    }

    m_lpPipeSvr = new zl::Ipc::ipcPipeSvrChannel();
    return !!m_lpPipeSvr->Start(hPipe);
}

bool CozyDictBase::StopPipe()
{
    if (m_lpPipeSvr)
    {
        m_lpPipeSvr->Stop();
        delete m_lpPipeSvr;
        return true;
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

COZYDICTAPI bool InvalidateMouseWindow(int nXpos, int nYPos)
{
    return CozyDictBaseInstance.InvalidateMouseWindow(nXpos, nYPos);
}

EXTERN_C COZYDICTAPI bool StartPipe()
{
    return CozyDictBaseInstance.StartPipe();
}

EXTERN_C COZYDICTAPI bool StopPipe()
{
    return CozyDictBaseInstance.StopPipe();
}