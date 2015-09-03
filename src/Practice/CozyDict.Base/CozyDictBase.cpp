// CozyDictBase.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDictBase.h"

#include "jsonparser/ipcjsonconvert.h"
#include "ipcpipesvr.h"
#include "ipcjsonprocessor.h"

HHOOK CozyDictBase::m_hHook = nullptr;

CozyDictBase::MouseHookCallback CozyDictBase::m_lpMouseCallback = nullptr;
CozyDictBase::IPCProcCallback CozyDictBase::m_lpIpcCallback = nullptr;

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
    rect.right = rect.left + 1;
    rect.bottom = rect.top + 1;

    if (!::InvalidateRect(hWnd, &rect, false))
    {
        return false;
    }
    return true;
}

bool CozyDictBase::StartPipe()
{
    m_lpPipeSvr = new zl::Ipc::ipcPipeSvr();
    m_lpPipeSvr->SetMsgProcessor(new zl::Ipc::ipcJsonProcessor());
    return m_lpPipeSvr->Start(TEXT("\\\\.\\pipe\\CozyDictPipe"));
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

void CozyDictBase::SetIPCCallback(IPCProcCallback lpCallback)
{
    m_lpIpcCallback = lpCallback;
}

int CozyDictBase::IPCProc(LPCTSTR lpString, DWORD dwPid)
{
    if (m_lpIpcCallback != nullptr)
    {
        return m_lpIpcCallback(lpString, dwPid);
    }
    return 0;
}

DWORD CozyDictBase::GetMouseWindowPid(int xPos, int yPos)
{
    POINT point{ xPos, yPos };
    HWND hWnd = ::WindowFromPoint(point);
    if (hWnd == nullptr)
    {
        return 0;
    }
    DWORD dwPid = 0;
    ::GetWindowThreadProcessId(hWnd, &dwPid);
    return dwPid;
}
