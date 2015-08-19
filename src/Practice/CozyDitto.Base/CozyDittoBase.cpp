// CozyDitto.Base.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDittoBase.h"

COZYDITTO_BASE_API bool CozyRegisterHotKey(HWND hWnd, int id, UINT fsModifiers, UINT vk)
{
    return ::RegisterHotKey(hWnd, id, fsModifiers, vk) == TRUE;
}

COZYDITTO_BASE_API bool CozyUnregisterHotKey(HWND hWnd, int id)
{
    return ::UnregisterHotKey(hWnd, id) == TRUE;
}

COZYDITTO_BASE_API bool CozySetClipboardText(HWND hWnd, LPCTSTR lpText, DWORD dwLength)
{
    if (dwLength == 0)
    {
        return false;
    }

    HANDLE hGlobalMemory = ::GlobalAlloc(GHND, dwLength + 2);
    if (hGlobalMemory == nullptr)
    {
        return false;
    }

    LPBYTE lpGlobalMemory = static_cast<LPBYTE>(::GlobalLock(hGlobalMemory));
    if (lpGlobalMemory == nullptr)
    {
        return false;
    }

    ::CopyMemory(lpGlobalMemory, lpText, dwLength);

    ::GlobalUnlock(hGlobalMemory);
    ::OpenClipboard(hWnd);
    ::EmptyClipboard();
    ::SetClipboardData(CF_UNICODETEXT, hGlobalMemory);
    ::CloseClipboard();
    return true;
}

COZYDITTO_BASE_API DWORD CozyGetClipboardText(HWND hWnd, LPTSTR lpResult)
{
    ::OpenClipboard(hWnd);
    HANDLE hClipMemory  = ::GetClipboardData(CF_UNICODETEXT);
    DWORD dwLength      = ::GlobalSize(hClipMemory);
    if (dwLength > 0 && lpResult != nullptr)
    {
        LPBYTE lpClipMemory = static_cast<LPBYTE>(::GlobalLock(hClipMemory));
        ::CopyMemory(lpResult, lpClipMemory, dwLength);
        ::GlobalUnlock(hClipMemory);
    }
    ::CloseClipboard();
    return dwLength - 2;
}