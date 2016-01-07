#pragma once
#include "stdafx.h"

class CozyKnightModifyDlg
    :public CBkDialogViewImplEx<CozyKnightModifyDlg>
{
public:
    CozyKnightModifyDlg(void);
    ~CozyKnightModifyDlg(void);

    enum
    {
        IDC_BTN_CLOSE = 1000,
    };

    BK_NOTIFY_MAP(IDC_RICHVIEW_WIN_EX)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_CLOSE, OnCloseBtn)
    BK_NOTIFY_MAP_END()

    BEGIN_MSG_MAP_EX(CozyKnightModifyDlg)
        MSG_BK_NOTIFY(IDC_RICHVIEW_WIN_EX)
        CHAIN_MSG_MAP(CBkDialogViewImplEx<CozyKnightModifyDlg>)
        REFLECT_NOTIFICATIONS_EX()
    END_MSG_MAP()

    void OnCloseBtn();
};
