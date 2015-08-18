#ifndef __COZY_DITTO_CORE__
#define __COZY_DITTO_CORE__

#ifndef COZYDITTO_CORE_IMPORT
#define COZYDITTO_CORE_API _declspec(dllexport)
#else
#define COZYDITTO_CORE_API _declspec(dllimport)
#endif

typedef bool(CALLBACK * HotKeyCallBack)(int id);

EXTERN_C COZYDITTO_CORE_API bool RegisterHotKeyWithName(LPCTSTR lpId, UINT fsModifiers, UINT vk);

EXTERN_C COZYDITTO_CORE_API int GetHotKeyIdWithName(LPCTSTR lpName);

EXTERN_C COZYDITTO_CORE_API bool UnregisterHotKeyWithName(LPCTSTR lpId);

EXTERN_C COZYDITTO_CORE_API bool SetClipboardText(LPCTSTR lpText, DWORD dwLength);

EXTERN_C COZYDITTO_CORE_API DWORD GetClipboardText(LPTSTR lpResult);

EXTERN_C COZYDITTO_CORE_API bool CreateHideMessageWindow();

EXTERN_C COZYDITTO_CORE_API void EnterMessageLoop();

EXTERN_C COZYDITTO_CORE_API void SetHotKeyCallback(HotKeyCallBack callback);

#endif // __COZY_DITTO_CORE__