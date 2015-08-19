// CozyDitto.Core.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDittoCore.h"
#include "CozyDittoHideDlg.h"

#pragma comment(lib, "CozyDitto.Base.lib")
#define COZYDITTO_BASE_IMPORT
#include "../CozyDitto.Base/CozyDittoBase.h"

CCozyDitto CozyDittoCoreInstance;

CCozyDitto::CCozyDitto()
    :m_pHideDlg(nullptr)
{
    CreateHideDlg();
}

CCozyDitto::~CCozyDitto()
{
    m_pHideDlg->Detach();
    delete m_pHideDlg;
}

int CCozyDitto::GetHotKeyIdWithName(LPCTSTR lpName)
{
    return ::GlobalAddAtom(lpName);
}

bool CCozyDitto::RegisterHotKeyWithName(LPCTSTR lpName ,UINT fsModifiers, UINT vk)
{
    return CozyRegisterHotKey(m_pHideDlg->m_hWnd, GetHotKeyIdWithName(lpName), fsModifiers, vk);
}

bool CCozyDitto::UnregisterHotKeyWithName(LPCTSTR lpName)
{
    return CozyUnregisterHotKey(m_pHideDlg->m_hWnd, GetHotKeyIdWithName(lpName));
}

bool CCozyDitto::SetClipboardText(LPCTSTR lpText, DWORD dwLength)
{
    return CozySetClipboardText(m_pHideDlg->m_hWnd, lpText, dwLength);
}

DWORD CCozyDitto::GetClipboardText(LPTSTR lpResult)
{
    return CozyGetClipboardText(m_pHideDlg->m_hWnd, lpResult);
}

void CCozyDitto::CreateHideDlg()
{
    m_pHideDlg = new CozyDittoHideDlg();
    m_pHideDlg->Create(nullptr);
}

void CCozyDitto::SetHotKeyCallback(HotKeyCallback callback)
{
    m_pHideDlg->SetHotKeyCallback(callback);
}

COZYDITTO_CORE_API int GetHotKeyIdWithName(LPCTSTR lpName)
{
    return CCozyDitto::GetHotKeyIdWithName(lpName);
}

COZYDITTO_CORE_API bool RegisterHotKeyWithName(LPCTSTR lpName, UINT fsModifiers, UINT vk)
{
    return CozyDittoCoreInstance.RegisterHotKeyWithName(lpName, fsModifiers, vk);
}

COZYDITTO_CORE_API bool UnregisterHotKeyWithName(LPCTSTR lpName)
{
    return CozyDittoCoreInstance.UnregisterHotKeyWithName(lpName);
}

COZYDITTO_CORE_API bool SetClipboardText(LPCTSTR lpText, DWORD dwLength)
{
    return CozyDittoCoreInstance.SetClipboardText(lpText, dwLength);
}

COZYDITTO_CORE_API DWORD GetClipboardText(LPTSTR lpResult)
{
    return CozyDittoCoreInstance.GetClipboardText(lpResult);
}

COZYDITTO_CORE_API void SetHotKeyCallback(HotKeyCallback callback)
{
    CozyDittoCoreInstance.SetHotKeyCallback(callback);
}

COZYDITTO_CORE_API void EnterMessageLoop()
{
    MSG msg;
    ::ZeroMemory(&msg, sizeof(msg));

    while (::GetMessage(&msg, nullptr, 0, 0))
    {
        ::TranslateMessage(&msg);
        ::DispatchMessage(&msg);
    }
}