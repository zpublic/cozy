// CozyDitto.Core.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDittoCore.h"

#pragma comment(lib, "CozyDitto.Base.lib")

#define COZYDITTO_BASE_IMPORT
#include "../CozyDitto.Base/CozyDittoBase.h"

LPCTSTR lpHindWindowClassName = TEXT("CozyDittoHidden");

HWND HideMessageWindowHwnd = nullptr;

HINSTANCE hInstance = nullptr;

MSG HindWindowMessage;

HotKeyCallBack pHotKeyCallBack = nullptr;

COZYDITTO_CORE_API bool RegisterHotKeyWithName(LPCTSTR lpId ,UINT fsModifiers, UINT vk)
{
    return CozyRegisterHotKey(HideMessageWindowHwnd, GetHotKeyIdWithName(lpId), fsModifiers, vk);
}

COZYDITTO_CORE_API bool UnregisterHotKeyWithName(LPCTSTR lpId)
{
    return CozyUnregisterHotKey(HideMessageWindowHwnd, GetHotKeyIdWithName(lpId));
}

COZYDITTO_CORE_API int GetHotKeyIdWithName(LPCTSTR lpName)
{
    return ::GlobalAddAtom(lpName);
}

COZYDITTO_CORE_API bool SetClipboardText(LPCTSTR lpText, DWORD dwLength)
{
    return CozySetClipboardText(HideMessageWindowHwnd, lpText, dwLength);
}

COZYDITTO_CORE_API DWORD GetClipboardText(LPTSTR lpResult)
{
    return CozyGetClipboardText(HideMessageWindowHwnd, lpResult);
}


LRESULT CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_CLOSE:
        ::DestroyWindow(hwnd);
        ::PostQuitMessage(0);
        break;
    case WM_HOTKEY:
        if (pHotKeyCallBack != nullptr)
        {
            pHotKeyCallBack(wParam);
        }
        break;
    default:
        return ::DefWindowProc(hwnd, message, wParam, lParam);
        break;
    }
    return 0;
}

COZYDITTO_CORE_API bool CreateHideMessageWindow()
{
    hInstance = ::GetModuleHandle(nullptr);

    WNDCLASS wndclass;
    wndclass.style          = CS_HREDRAW | CS_VREDRAW;
    wndclass.lpfnWndProc    = WndProc;
    wndclass.cbClsExtra     = 0;
    wndclass.cbWndExtra     = 0;
    wndclass.hbrBackground  = (HBRUSH)GetStockObject(WHITE_BRUSH);
    wndclass.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wndclass.hIcon          = LoadIcon(nullptr, IDI_APPLICATION);
    wndclass.hInstance      = hInstance;
    wndclass.lpszClassName  = lpHindWindowClassName;
    wndclass.lpszMenuName   = nullptr;

    if (::RegisterClass(&wndclass) == 0)
    {
        return false;
    }

    HideMessageWindowHwnd = ::CreateWindowEx(0, lpHindWindowClassName, nullptr, WS_OVERLAPPEDWINDOW, -1, -1, 0, 0, nullptr, nullptr, hInstance, nullptr);
    if (HideMessageWindowHwnd == nullptr)
    {
        return false;
    }

    ::UpdateWindow(HideMessageWindowHwnd);
    return true;
}

COZYDITTO_CORE_API void EnterMessageLoop()
{
    ::ZeroMemory(&HindWindowMessage, sizeof(HindWindowMessage));

    while (::GetMessage(&HindWindowMessage, HideMessageWindowHwnd, 0, 0)>0)
    {
        ::TranslateMessage(&HindWindowMessage);
        ::DispatchMessage(&HindWindowMessage);
    }
    ::UnregisterClass(lpHindWindowClassName, hInstance);
}

COZYDITTO_CORE_API void SetHotKeyCallback(HotKeyCallBack pCallback)
{
    pHotKeyCallBack = pCallback;
}