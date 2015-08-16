#ifndef __COZY_DITTO_BASE__
#define __COZY_DITTO_BASE__

#ifndef COZYDITTO_BASE_IMPORT
#define COZYDITTO_BASE_API _declspec(dllexport)
#else
#define COZYDITTO_BASE_API _declspec(dllimport)
#endif

#include "windows.h"

EXTERN_C COZYDITTO_BASE_API bool CozyRegisterHotKey(HWND hWnd, int id, UINT fsModifiers, UINT vk);

EXTERN_C COZYDITTO_BASE_API bool CozyUnregisterHotKey(HWND hWnd, int id);

EXTERN_C COZYDITTO_BASE_API bool CozySetClipboardText(HWND hWnd, LPCTSTR lpText, DWORD dwLength);

EXTERN_C COZYDITTO_BASE_API DWORD CozyGetClipboardText(HWND hWnd, LPTSTR lpResult);

#endif // __COZY_DITTO_BASE__