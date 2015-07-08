// KeyboardUtilCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "KeyboardUtilCpp.h"

KEYBOARDUTILCPP_API CKeyboardUtilCpp CKeyboardUtilCppInstance;

CKeyboardUtilCpp::CKeyboardUtilCpp()
{
}

void CKeyboardUtilCpp::KeyboardEvent(WORD wKey, WORD wScanKey, DWORD dwFlag, DWORD dwExtraInfo)
{
    INPUT input;
    input.type              = INPUT_KEYBOARD;
    input.ki.wVk            = wKey;
    input.ki.wScan          = wScanKey;
    input.ki.dwExtraInfo    = dwExtraInfo;
    input.ki.dwFlags        = dwFlag;
    input.ki.time           = ::GetTickCount();
    ::SendInput(1, &input, sizeof(INPUT));
}

bool CKeyboardUtilCpp::QueryKeyState(WORD wKey)
{
    return ::GetAsyncKeyState(wKey) != 0;
}

void CKeyboardUtilCpp::SendKeyEvent(WORD wKey)
{
    KeyboardEvent(wKey, 0, 0, 0);
    KeyboardEvent(wKey, 0, KEYEVENTF_KEYUP, 0);
}

KEYBOARDUTILCPP_API void KeyboardEvent(WORD wKey, WORD wScanKey, DWORD dwFlag, DWORD dwExtraInfo)
{
    return CKeyboardUtilCppInstance.KeyboardEvent(wKey, wScanKey, dwFlag, dwExtraInfo);
}

KEYBOARDUTILCPP_API bool QueryKeyState(WORD wKey)
{
    return CKeyboardUtilCppInstance.QueryKeyState(wKey);
}

KEYBOARDUTILCPP_API void SendKeyEvent(WORD wKey)
{
    return CKeyboardUtilCppInstance.SendKeyEvent(wKey);
}