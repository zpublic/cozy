// CozyDictHook.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyDictHook.h"

#include "3rd/mhook-lib/mhook.h"
#include "ipcpipecltchannel.h"
#include "cstring"

CozyDictHook::_TextOutA CozyDictHook::m_lpTrueTextOutA = nullptr;
CozyDictHook::_TextOutW CozyDictHook::m_lpTrueTextOutW = nullptr;
CozyDictHook::_ExtTextOutA CozyDictHook::m_lpTrueExtTextOutA = nullptr;
CozyDictHook::_ExtTextOutW CozyDictHook::m_lpTrueExtTextOutW = nullptr;

zl::Ipc::ipcPipeCltChannel* CozyDictHook::m_lpPipeClt = nullptr;

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

    if (!Mhook_Unhook(reinterpret_cast<LPVOID*>(m_lpTrueTextOutA)))
    {
        bFlag = false;
    }
    if (Mhook_Unhook(reinterpret_cast<LPVOID*>(m_lpTrueTextOutW)))
    {
        bFlag = false;
    }
    if (Mhook_Unhook(reinterpret_cast<LPVOID*>(m_lpTrueExtTextOutA)))
    {
        bFlag = false;
    }
    if (Mhook_Unhook(reinterpret_cast<LPVOID*>(m_lpTrueExtTextOutW)))
    {
        bFlag = false;
    }
    return bFlag;
}

BOOL WINAPI CozyDictHook::TextOutAProc(HDC hdc, int x, int y, LPCSTR lpString, int c)
{
    SendPipeData((LPVOID)lpString, sizeof(*lpString) * std::strlen(lpString));
    return m_lpTrueTextOutA(hdc, x, y, lpString, c);
}

BOOL WINAPI CozyDictHook::TextOutWProc(HDC hdc, int x, int y, LPCWSTR lpString, int c)
{
    SendPipeData((LPVOID)lpString, sizeof(*lpString) * std::wcslen(lpString));
    MessageBox(0, 0, 0, 0);
    return m_lpTrueTextOutW(hdc, x, y, lpString, c);
}

BOOL WINAPI CozyDictHook::ExtTextOutAProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCSTR lpString, UINT c, const INT * lpDx)
{
    SendPipeData((LPVOID)lpString, sizeof(*lpString) * std::strlen(lpString));
    return m_lpTrueExtTextOutA(hdc, x, y, options, lprect, lpString, c, lpDx);
}

BOOL WINAPI CozyDictHook::ExtTextOutWProc(HDC hdc, int x, int y, UINT options, const RECT * lprect, LPCWSTR lpString, UINT c, const INT * lpDx)
{
    SendPipeData((LPVOID)lpString, sizeof(*lpString) * std::wcslen(lpString));
    return m_lpTrueExtTextOutW(hdc, x, y, options, lprect, lpString, c, lpDx);
}

bool CozyDictHook::StartPipe()
{
    m_lpPipeClt = new zl::Ipc::ipcPipeCltChannel();
    m_lpPipeClt->SetPipeName(TEXT("\\\\.\\pipe\\CozyDictPipe"));
    if (m_lpPipeClt->Connect())
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
        return true;
    }
    return false;
}

bool CozyDictHook::SendPipeData(LPVOID lpBytes, DWORD dwSize)
{
    if (m_lpPipeClt != nullptr)
    {
        return m_lpPipeClt->Send(lpBytes, dwSize);
    }
    return false;
}

COZYDICTAPI void InitHookEnv()
{
    CozyDictHookInstance.InitHookEnv();
}

COZYDICTAPI bool SetTextOutAHook()
{
    return CozyDictHookInstance.SetTextOutAHook();
}

COZYDICTAPI bool SetTextOutWHook()
{
    return CozyDictHookInstance.SetTextOutWHook();
}

COZYDICTAPI bool SetExtTextOutAHook()
{
    return CozyDictHookInstance.SetExtTextOutAHook();
}

COZYDICTAPI bool SetExtTextOutWHook()
{
    return CozyDictHookInstance.SetExtTextOutWHook();
}

COZYDICTAPI bool UnsetAllHook()
{
    return CozyDictHookInstance.UnsetAllApiHook();
}