// CozyDitto.Core.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDittoCore.h"

#pragma comment(lib, "CozyDitto.Base.lib")

#define COZYDITTO_BASE_IMPORT
#include "../CozyDitto.Base/CozyDittoBase.h"

LPCTSTR lpShowWindowText = TEXT("ShowWindowHotKey");

COZYDITTO_CORE_API bool RegisterShowWindowHotKey(HWND hWnd, UINT fsModifiers, UINT vk)
{
    return CozyRegisterHotKey(hWnd, GetShowWindowHotKeyId(), fsModifiers, vk);
}

COZYDITTO_CORE_API bool UnregisterShowWindowHotKey(HWND hWnd)
{
    return CozyUnregisterHotKey(hWnd, GetShowWindowHotKeyId());
}

COZYDITTO_CORE_API int GetShowWindowHotKeyId()
{
    return ::GlobalAddAtom(lpShowWindowText);
}

COZYDITTO_CORE_API bool SetClipboardText(HWND hWnd, LPCTSTR lpText, DWORD dwLength)
{
    return CozySetClipboardText(hWnd, lpText, dwLength);
}

COZYDITTO_CORE_API DWORD GetClipboardText(HWND hWnd, LPTSTR lpResult)
{
    return CozyGetClipboardText(hWnd, lpResult);
}