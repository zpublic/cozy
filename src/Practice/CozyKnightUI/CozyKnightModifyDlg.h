#pragma once
#include "stdafx.h"
#include "knightdef.h"

class CozyKnightModifyDlg
    :public CBkDialogViewImplEx<CozyKnightModifyDlg>
{
public:
    CozyKnightModifyDlg(CString& strName, ADDRESS_INFO& addrInfo, int& nValue, BOOL& bCheck);
    ~CozyKnightModifyDlg(void);

    enum
    {
        IDC_BTN_CLOSE   = 1000,
        IDC_BTN_OK      = 6,

        IDC_EDT_NAME    = 1,
        IDC_EDT_ADDR    = 2,
        IDC_EDT_VALUE   = 4,

        IDC_COMBO_TYPE  = 3,

        IDC_CHK_LOCK    = 5,
    };

    BK_NOTIFY_MAP(IDC_RICHVIEW_WIN_EX)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_CLOSE, OnCloseBtn)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_OK, OnOkBtn);
    BK_NOTIFY_MAP_END()

    BEGIN_MSG_MAP_EX(CozyKnightModifyDlg)
        MSG_BK_NOTIFY(IDC_RICHVIEW_WIN_EX)
        MSG_WM_INITDIALOG(OnInitDialog)
        CHAIN_MSG_MAP(CBkDialogViewImplEx<CozyKnightModifyDlg>)
        REFLECT_NOTIFICATIONS_EX()
    END_MSG_MAP()

    BOOL OnInitDialog(CWindow /*wndFocus*/, LPARAM /*lInitParam*/);
    void OnCloseBtn();
    void OnOkBtn();

private:
    void InitNameEdt();
    void InitAddrEdt();
    void InitValueEdt();
    void InitTypeCombo();

    void UpdateModify();

    static CString GetEditText(const CEdit& editCtrl);

private:
    CEdit                   m_NameEdit;
    CEdit                   m_AddrEdit;
    CEdit                   m_ValueEdit;
    CComboBox               m_TypeCombo;
    CString&                m_StrName;
    const ADDRESS_INFO&     m_AddrRef;
    int&                    m_Value;
    BOOL&                   m_Lock;
};
