#pragma once
#include "stdafx.h"

class CozyKnightSelectTargetDlg
    :public CBkDialogViewImplEx<CozyKnightSelectTargetDlg>
{
public:
    CozyKnightSelectTargetDlg(HANDLE hTarget);
    ~CozyKnightSelectTargetDlg(void);

    enum
    {
        IDC_BTN_CLOSE           = 1000,
        IDC_BTN_OK              = 1002,
        IDC_BTN_CANCLE          = 1003,
    };

protected:
    BK_NOTIFY_MAP(IDC_RICHVIEW_WIN_EX)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_CLOSE, OnBtnClose)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_OK, OnOk)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_CANCLE, OnCalcle)

    BK_NOTIFY_MAP_END()

    BEGIN_MSG_MAP_EX(CBkDialogViewImplEx<CozyKnightSelectTargetDlg>)
        MSG_BK_NOTIFY(IDC_RICHVIEW_WIN_EX)
        CHAIN_MSG_MAP(CBkDialogViewImplEx<CozyKnightSelectTargetDlg>)
        MSG_WM_INITDIALOG(OnInitDialog)
        REFLECT_NOTIFICATIONS_EX()
    END_MSG_MAP()

    void OnBtnClose();
    void OnOk();
    void OnCalcle();
    void OnInitDialog();

    void RefreshProcess();

private:
    HANDLE &m_hTarget;
};
