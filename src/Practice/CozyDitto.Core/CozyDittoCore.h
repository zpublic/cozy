#ifndef __COZY_DITTO_CORE__
#define __COZY_DITTO_CORE__

#ifndef COZYDITTO_CORE_IMPORT
#define COZYDITTO_CORE_API _declspec(dllexport)
#else
#define COZYDITTO_CORE_API _declspec(dllimport)
#endif

class CozyDittoHideDlg;

typedef void(CALLBACK*HotKeyCallback)(WPARAM wParam, LPARAM lParam);

class CCozyDitto
{
public:
    CCozyDitto();

    ~CCozyDitto();

    static int GetHotKeyIdWithName(LPCTSTR lpName);

    bool RegisterHotKeyWithName(LPCTSTR lpName, UINT fsModifiers, UINT vk);

    bool UnregisterHotKeyWithName(LPCTSTR lpName);

    bool SetClipboardText(LPCTSTR lpText, DWORD dwLength);

    DWORD GetClipboardText(LPTSTR lpResult);

    void CreateHideDlg();

    void SetHotKeyCallback(HotKeyCallback callback);

private:
    CozyDittoHideDlg*   m_pHideDlg;
};

EXTERN_C COZYDITTO_CORE_API int GetHotKeyIdWithName(LPCTSTR lpName);

EXTERN_C COZYDITTO_CORE_API bool RegisterHotKeyWithName(LPCTSTR lpName, UINT fsModifiers, UINT vk);

EXTERN_C COZYDITTO_CORE_API bool UnregisterHotKeyWithName(LPCTSTR lpName);

EXTERN_C COZYDITTO_CORE_API bool SetClipboardText(LPCTSTR lpText, DWORD dwLength);

EXTERN_C COZYDITTO_CORE_API DWORD GetClipboardText(LPTSTR lpResult);

EXTERN_C COZYDITTO_CORE_API void SetHotKeyCallback(HotKeyCallback callback);

EXTERN_C COZYDITTO_CORE_API void EnterMessageLoop();

#endif // __COZY_DITTO_CORE__