#pragma once
#include "stdafx.h"
#include "iknight.h"

class CozyKnightMainDlg
    :public CBkDialogViewImplEx<CozyKnightMainDlg>
{
public:
    CozyKnightMainDlg(void);
    ~CozyKnightMainDlg();

    enum
    {
        IDC_BTN_CLOSE           = 1000,
        IDC_BTN_SELECT_TARGET   = 1001,
        IDC_BTN_NEW_TASK        = 3,
        IDC_BTN_DELETE_TASK     = 4,
        IDC_BTN_DELETE_ALL_TASK = 5,
        IDC_BTN_IMPORT          = 6,
        IDC_BTN_EXPORT          = 7,

        IDC_TASK_LIST_CTRL      = 8,
    };

protected:
    BK_NOTIFY_MAP(IDC_RICHVIEW_WIN_EX)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_CLOSE, OnBtnClose)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_SELECT_TARGET, OnSelectTarget)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_NEW_TASK, OnNewTask);
    BK_NOTIFY_MAP_END()

    BEGIN_MSG_MAP_EX(CBkDialogViewImplEx<CozyKnightMainDlg>)
        MSG_BK_NOTIFY(IDC_RICHVIEW_WIN_EX)
        MSG_WM_INITDIALOG(OnInitDialog)
        CHAIN_MSG_MAP(CBkDialogViewImplEx<CozyKnightMainDlg>)
        REFLECT_NOTIFICATIONS_EX()
    END_MSG_MAP()

    BOOL OnInitDialog(CWindow /*wndFocus*/, LPARAM /*lInitParam*/);
    void OnBtnClose();
    void OnSelectTarget();
    void OnNewTask();

private:
    CEdit           m_edtValue;
    CComboBox       m_comboValueType;
    CListViewCtrl   m_searchList;
    CListViewCtrl   m_selectList;
    IKnight*        m_core;
    int             m_nTaskCount;
};
