// CozyDictHook.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDictHook.h"

#include "3rd/mhook-lib/mhook.h"
#include "ipccallclient.h"
#include "cstring"

#pragma data_seg("CozyDictHook")

HHOOK hCBTHook = nullptr;

#pragma data_seg()

CozyDictHook::_TextOutA CozyDictHook::m_lpTrueTextOutA = nullptr;
CozyDictHook::_TextOutW CozyDictHook::m_lpTrueTextOutW = nullptr;
CozyDictHook::_ExtTextOutA CozyDictHook::m_lpTrueExtTextOutA = nullptr;
CozyDictHook::_ExtTextOutW CozyDictHook::m_lpTrueExtTextOutW = nullptr;

zl::Ipc::ipcCallClient* CozyDictHook::m_lpPipeClt = nullptr;

HINSTANCE CozyDictHook::m_hInstance = nullptr;

COZYDICTAPI CozyDictHook CozyDictHookInstance;

CozyDictHook::CozyDictHook()
{

}

CozyDictHook::~CozyDictHook()
{

}

void CozyDictHook::InitHookEnv()
{
    m_lpTrueTextOutA = reinterpret_cast<_TextOutA>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "TextOutA"));
    m_lpTrueTextOutW = reinterpret_cast<_TextOutW>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "TextOutW"));
    m_lpTrueExtTextOutA = reinterpret_cast<_ExtTextOutA>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "ExtTextOutA"));
    m_lpTrueExtTextOutW = reinterpret_cast<_ExtTextOutW>(::GetProcAddress(::GetModuleHandle(TEXT("gdi32")), "ExtTextOutW"));
}

bool CozyDictHook::SetTextOutAHook()
{
    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueTextOutA), TextOutAProc))
    {
        return true;
    }
    return false;
}

bool CozyDictHook::SetTextOutWHook()
{
    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueTextOutW), TextOutWProc))
    {
        return true;
    }
    return false;
}

bool CozyDictHook::SetExtTextOutAHook()
{
    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueExtTextOutA), ExtTextOutAProc))
    {
        return true;
    }
    return false;
}
bool CozyDictHook::SetExtTextOutWHook()
{
    if (Mhook_SetHook(reinterpret_cast<LPVOID*>(&m_lpTrueExtTextOutW), ExtTextOutWProc))
    {
        return true;
    }
    return false;
}

bool CozyDictHook::UnsetAllApiHook()
{
    bool bFlag = true;
    if (!Mhook_Unhook(reinterpret_cast<LPVOID*>(&m_lpTrueTextOutA)))
    {
        bFlag = false;
    }
    if (!Mhook_Unhook(reinterpret_cast<LPVOID*>(&m_lpTrueTextOutW)))
    {
        bFlag = false;
    }
    //if (!Mhook_Unhook(reinterpret_cast<LPVOID*>(&m_lpTrueExtTextOutA)))
    {
        bFlag = false;
    }
    //if (!Mhook_Unhook(reinterpret_cast<LPVOID*>(&m_lpTrueExtTextOutW)))
    {
        bFlag = false;
    }
    return bFlag;
}

LRESULT CALLBACK CozyDictHook::CBTHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
    return ::CallNextHookEx(hCBTHook, nCode, wParam, lParam);
}

BOOL CALLBACK CozyDictHook::TextOutAProc(HDC hdc, int x, int y, LPCSTR lpString, int c)
{
    int len = MultiByteToWideChar(CP_ACP, 0, lpString, std::strlen(lpString), nullptr, 0);
    LPWSTR lpWStr = new wchar_t[len + 1];
    MultiByteToWideChar(CP_ACP, 0, lpString, std::strlen(lpString), lpWStr, len);
    lpWStr[len] = '\0';
    SendPipeData(lpWStr);
    return m_lpTrueTextOutA(hdc, x, y, lpString, c);
}

BOOL CALLBACK CozyDictHook::TextOutWProc(HDC hdc, int x, int y, LPCWSTR lpString, int c)
{
    SendPipeData(lpString);
    return m_lpTrueTextOutW(hdc, x, y, lpString, c);
}

BOOL CALLBACK CozyDictHook::ExtTextOutAProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCSTR lpString, UINT c, const INT * lpDx)
{
    int l = std::strlen(lpString);
    int len = MultiByteToWideChar(CP_ACP, 0, lpString,l , nullptr, 0);
    LPWSTR lpWStr = new wchar_t[len + 1];
    MultiByteToWideChar(CP_ACP, 0, lpString, l, lpWStr, len);
    lpWStr[len] = '\0';
    SendPipeData(lpWStr);
    return m_lpTrueExtTextOutA(hdc, x, y, options, lprect, lpString, c, lpDx);
}

BOOL CALLBACK CozyDictHook::ExtTextOutWProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCWSTR lpString, UINT c, const INT * lpDx)
{
    SendPipeData(lpString);
    return m_lpTrueExtTextOutW(hdc, x, y, options, lprect, lpString, c, lpDx);
}

bool CozyDictHook::StartPipe()
{
    if (m_lpPipeClt == nullptr)
    {
        m_lpPipeClt = new zl::Ipc::ipcCallClient();
    }

    if(m_lpPipeClt->ConnectServer(TEXT("\\\\.\\pipe\\CozyDictPipe")))
    {
        return true;
    }
    return false;
}

bool CozyDictHook::StopPipe()
{
    if (m_lpPipeClt != nullptr)
    {
        m_lpPipeClt->DisConnect();
        delete m_lpPipeClt;
        m_lpPipeClt = nullptr;
        return true;
    }
    return false;
}

bool CozyDictHook::SendPipeData(LPCTSTR lpBytes)
{
    if (m_lpPipeClt != nullptr)
    {
        int nRetValue = 0;
        return !!m_lpPipeClt->CallFunc("IPCProc", nRetValue, lpBytes, ::GetCurrentProcessId());
    }
    return false;
}

bool CozyDictHook::SetCBTHook()
{
    hCBTHook = ::SetWindowsHookEx(WH_CBT, CBTHookProc, m_hInstance, 0);
    if (hCBTHook == nullptr)
    {
        return false;
    }
    return true;
}

bool CozyDictHook::UnsetCBTHook()
{
    if (hCBTHook != nullptr)
    {
        if (::UnhookWindowsHookEx(hCBTHook))
        {
            return true;
        }
    }
    return false;
}

void CozyDictHook::SetHInstance(HINSTANCE hInstance)
{
    m_hInstance = hInstance;
}

COZYDICTAPI void InitHookEnv()
{
    CozyDictHookInstance.InitHookEnv();
}

COZYDICTAPI bool UnsetAllHook()
{
    return CozyDictHookInstance.UnsetAllApiHook();
}

COZYDICTAPI bool SetAllHook()
{
    if (!CozyDictHookInstance.SetTextOutAHook()) return false;
    if (!CozyDictHookInstance.SetTextOutWHook()) return false;
    // if (!CozyDictHookInstance.SetExtTextOutAHook()) return false;
    // if (!CozyDictHookInstance.SetExtTextOutWHook()) return false;
    return true;
}

COZYDICTAPI bool SetCBTHook()
{
    return CozyDictHookInstance.SetCBTHook();
}

COZYDICTAPI bool UnSetCBTHook()
{
    return CozyDictHookInstance.UnsetCBTHook();
}
