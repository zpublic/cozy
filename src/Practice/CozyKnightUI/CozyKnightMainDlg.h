#pragma once
#include "stdafx.h"
#include "iknight.h"
#include "rapidjson/prettywriter.h"
#include <vector>
#include <map>

class CozyKnightMainDlg
    :public CBkDialogViewImplEx<CozyKnightMainDlg>
{
public:
    typedef std::pair<CString, BOOL> MetaInfo;
    typedef std::pair<ADDRESS_INFO, int> SelectedInfo;

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
        IDC_BTN_SEARCH          = 13,

        IDC_TASK_LIST_CTRL      = 8,
        IDC_SEARCH_LIST_CTRL    = 9,
        IDC_SELECTED_LIST_CTRL  = 10,
    };

    enum
    {
        IDC_TIMER_LOCK          = 23,
        IDC_TIMER_UPDATE        = 24,
    };

protected:
    BK_NOTIFY_MAP(IDC_RICHVIEW_WIN_EX)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_CLOSE, OnBtnClose)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_SELECT_TARGET, OnSelectTarget)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_NEW_TASK, OnNewTask)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_DELETE_TASK, OnDeleteTask)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_DELETE_ALL_TASK, OnDeleteAllTask)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_SEARCH, OnSearch)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_IMPORT, OnImport)
        BK_NOTIFY_ID_COMMAND(IDC_BTN_EXPORT, OnExport)

        BK_LISTWND_NOTIFY_BEGIN(IDC_TASK_LIST_CTRL)
            BK_LISTWND_LISTITEM_LBUTTONDOWN(OnTaskLBtnUp)
        BK_LISTWND_NOTIFY_END()
    BK_NOTIFY_MAP_END()

    BEGIN_MSG_MAP_EX(CozyKnightMainDlg)
        MSG_BK_NOTIFY(IDC_RICHVIEW_WIN_EX)
        MSG_WM_INITDIALOG(OnInitDialog)
        MSG_WM_TIMER(OnTimer)
        MSG_WM_CLOSE(OnClose)

        NOTIFY_HANDLER_EX(IDC_SEARCH_LIST_CTRL, NM_DBLCLK, OnSearchDBListClick)
        NOTIFY_HANDLER_EX(IDC_SELECTED_LIST_CTRL, NM_DBLCLK, OnSelectedDBListClick)

        CHAIN_MSG_MAP(CBkDialogViewImplEx<CozyKnightMainDlg>)
        REFLECT_NOTIFICATIONS_EX()
    END_MSG_MAP()

    BOOL OnInitDialog(CWindow /*wndFocus*/, LPARAM /*lInitParam*/);
    void OnClose();
    void OnBtnClose();
    void OnSelectTarget();
    void OnNewTask();
    void OnDeleteTask();
    void OnDeleteAllTask();
    void OnSearch();
    void OnExport();
    void OnImport();
    void OnTimer(UINT_PTR nIDEvent);

    void OnTaskLBtnUp(int nListItem);

    LRESULT OnSearchDBListClick(LPNMHDR pnmh);
    LRESULT OnSelectedDBListClick(LPNMHDR pnmh);

private:
    void InitComboBox();
    void InitEditBox();
    void InitSearchList();
    void InitSelectList();

private:
    void AppendSearchItem(LPVOID lpAddr, INT nSize, int nValue, int nItemId);
    void AppendSelectedItem(LPVOID lpAddr, INT nSize, int nValue, int nItemId, LPCTSTR lpName = NULL);
    void UpdateSearchItem(LPVOID lpAddr, INT nSize, int nValue, int nItemId, BOOL bUsable = TRUE);
    void UpdateSelectedItem(const CString& strName, LPVOID lpAddr, INT nSize, int nValue, int nItemId, BOOL bChekced = FALSE, BOOL bUsable = TRUE);

    void UpdateDataProc();
    void AutoLockProc();

private:
    std::vector<MetaInfo >          m_SelectedMetaInfo;
    std::map<LPVOID, SelectedInfo>  m_AutoLockList;
    CEdit               m_edtValue;
    CComboBox           m_comboValueType;
    CListViewCtrl       m_searchList;
    CListViewCtrl       m_selectList;
    IKnight*            m_core;
    int                 m_nTaskCount;
    int                 m_nSelectCount;
};
