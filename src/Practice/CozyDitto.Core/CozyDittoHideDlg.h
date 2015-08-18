#ifndef __COZY_DITTO_HIDE_DLG__
#define __COZY_DITTO_HIDE_DLG__

#include <Windows.h>
#include <atlbase.h>
#include <atlwin.h>

class CozyDittoHideDlg;
typedef CWindowImpl<CozyDittoHideDlg, CWindow, CWinTraits<WS_OVERLAPPED, 0>> CozyDittoHideWindowBase;

class CozyDittoHideDlg : public CozyDittoHideWindowBase
{
public:
    typedef void(CALLBACK*HotKeyCallback)(WPARAM wParam, LPARAM lParam);

    CozyDittoHideDlg();

    ~CozyDittoHideDlg();

    void SetHotKeyCallback(HotKeyCallback callback);

    BEGIN_MSG_MAP(CozyDittoHideDlg)
        MESSAGE_HANDLER(WM_CREATE, OnCreate)
        MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
        MESSAGE_HANDLER(WM_HOTKEY, OnHotKey)
    END_MSG_MAP()

    LRESULT OnCreate(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
    LRESULT OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
    LRESULT OnHotKey(UINT /*uMsg*/, WPARAM wParam, LPARAM lParam, BOOL& /*bHandled*/);

private:
    HotKeyCallback m_pCallback;
};

#endif // __COZY_DITTO_HIDE_DLG__