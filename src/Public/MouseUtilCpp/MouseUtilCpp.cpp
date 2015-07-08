// MouseUtilCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "MouseUtilCpp.h"

MOUSEUTILCPP_API CMouseUtilCpp CMouseUtilCppInstance;

CMouseUtilCpp::CMouseUtilCpp(void)
{

}

bool CMouseUtilCpp::GetCursorPosition(long* lpXPosition, long* lpYPosition)
{
    POINT p;
    bool bResult = (::GetCursorPos(&p) == TRUE);

    if (!bResult) return false;
    *lpXPosition = p.x;
    *lpYPosition = p.y;
    return true;
}

bool CMouseUtilCpp::SetCursorPosition(int iXPosition, int iYPosition)
{
    return (::SetCursorPos(iXPosition, iYPosition) == TRUE);
}

bool CMouseUtilCpp::CursorClip(const RECT* rect)
{
    return (::ClipCursor(rect) == TRUE);
}

void CMouseUtilCpp::MouseEvent(
    DWORD dwFlags, DWORD dx, DWORD dy, 
    DWORD dwData, ULONG_PTR dwExtraInfo)
{
    INPUT input;
    input.type              = INPUT_MOUSE;
    input.mi.dwFlags        = dwFlags;
    input.mi.dx             = dx;
    input.mi.dy             = dy;
    input.mi.mouseData      = dwData;
    input.mi.dwExtraInfo    = dwExtraInfo;
    input.mi.time           = ::GetTickCount();
    ::SendInput(1, &input, sizeof(INPUT));
}

void CMouseUtilCpp::MouseClick(BUTTON key, DWORD dx, DWORD dy)
{
    DWORD dwKeyDownTag;
    DWORD dwKeyUpTag;

    switch (key)
    {
    case LEFT:
        dwKeyDownTag    = MOUSEEVENTF_LEFTDOWN;
        dwKeyUpTag      = MOUSEEVENTF_LEFTUP;
        break;
    case RIGHT:
        dwKeyDownTag    = MOUSEEVENTF_RIGHTDOWN;
        dwKeyUpTag      = MOUSEEVENTF_RIGHTUP;
        break;
    case MIDDLE:
        dwKeyDownTag    = MOUSEEVENTF_MIDDLEDOWN;
        dwKeyUpTag      = MOUSEEVENTF_MIDDLEUP;
        break;
    default:
        break;
    }

    MouseEvent(dwKeyDownTag, dx, dy, 0, 0);
    MouseEvent(dwKeyUpTag, dx, dy, 0, 0);
}

MOUSEUTILCPP_API bool GetCursorPosition(long* lpXPosition, long* lpYPosition)
{
    return CMouseUtilCppInstance.GetCursorPosition(lpXPosition, lpYPosition);
}

MOUSEUTILCPP_API bool CursorClip(long lLeft, long lTop, long lRight, long lBottom)
{
    RECT rect;
    rect.left   = lLeft;
    rect.right  = lRight;
    rect.top    = lTop;
    rect.bottom = lBottom;
    return CMouseUtilCppInstance.CursorClip(&rect);
}

MOUSEUTILCPP_API bool CursorUnClip()
{
    return CMouseUtilCppInstance.CursorClip(nullptr);
}

MOUSEUTILCPP_API void MouseEvent(DWORD dwFlags, DWORD dx, DWORD dy, DWORD dwData, ULONG_PTR dwExtraInfo)
{
    CMouseUtilCppInstance.MouseEvent(dwFlags, dx, dy, dwData, dwExtraInfo);
}

MOUSEUTILCPP_API bool SetCursorPosition(int iXPosition, int iYPosition)
{
    return CMouseUtilCppInstance.SetCursorPosition(iXPosition, iYPosition);
}

MOUSEUTILCPP_API void MouseClick(BUTTON key, DWORD dx, DWORD dy)
{
    CMouseUtilCppInstance.MouseClick(key, dx, dy);
}

MOUSEUTILCPP_API void LeftClick(DWORD dx, DWORD dy)
{
    CMouseUtilCppInstance.MouseClick(BUTTON::LEFT, dx, dy);
}

MOUSEUTILCPP_API void RightClick(DWORD dx, DWORD dy)
{
    CMouseUtilCppInstance.MouseClick(BUTTON::RIGHT, dx, dy);
}

MOUSEUTILCPP_API void MiddleClick(DWORD dx, DWORD dy)
{
    CMouseUtilCppInstance.MouseClick(BUTTON::MIDDLE, dx, dy);
}