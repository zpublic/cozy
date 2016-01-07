#include "StdAfx.h"
#include "CozyKnightMainDlg.h"
#include "CozyKnightSelectTargetDlg.h"
#include "CozyKnightModifyDlg.h"

CozyKnightMainDlg::CozyKnightMainDlg(void)
	:CBkDialogViewImplEx<CozyKnightMainDlg>(IDR_MAIN), m_core(NULL), m_nTaskCount(0), m_nSelectCount(0)
{
    HMODULE hDllLib = ::LoadLibrary(_T("CozyKnightCore.dll"));
    if(hDllLib)
    {
        typedef IKnight* (*GetInstanceFunc)();
        GetInstanceFunc lpfnGetInstance = (GetInstanceFunc)::GetProcAddress(hDllLib, "GetInstance");
        m_core = lpfnGetInstance();
    }
}

CozyKnightMainDlg::~CozyKnightMainDlg()
{
    if(m_core != NULL)
    {
        m_core->Release();
        m_core = NULL;
    }
}

BOOL CozyKnightMainDlg::OnInitDialog(CWindow /*wndFocus*/, LPARAM /*lInitParam*/)
{
    InitEditBox();
    InitComboBox();
    InitSearchList();
    InitSelectList();

    return True;
}

void CozyKnightMainDlg::OnBtnClose()
{
	EndDialog(IDCLOSE);
}

void CozyKnightMainDlg::OnSelectTarget()
{
    if(m_core != NULL)
    {
        HANDLE hTarget = NULL;
        CozyKnightSelectTargetDlg dlg(hTarget);
        if(dlg.DoModal(m_hWnd) == IDOK && hTarget != NULL)
        {
            m_core->Attach(hTarget);
            OnDeleteAllTask();
            m_searchList.DeleteAllItems();
            m_searchList.DeleteAllItems();
        }
    }
}

void CozyKnightMainDlg::OnNewTask()
{
    if(m_core != NULL)
    {
        if(m_core->CreateTask() != NULL)
        {
            CString cs;
            cs.Format(_T("<listitem height=\"32\"><text pos=\"0,0,-0,-0\">任务%d</text></listitem>"), m_nTaskCount++);
            AppendListItem(IDC_TASK_LIST_CTRL, CW2A(cs, CP_UTF8), -1, TRUE);
        }
    }
}

void CozyKnightMainDlg::OnDeleteTask()
{
    if(m_core != NULL)
    {
        int nItem = GetCurSelItem(IDC_TASK_LIST_CTRL);
        if(nItem >= 0 && nItem < m_core->GetTaskCount())
        {
            DeleteListItem(IDC_TASK_LIST_CTRL, nItem);

            IKnightTask* pSelectedTask =  m_core->GetTask(nItem);
            m_core->DeleteTask(pSelectedTask);
            m_searchList.DeleteAllItems();
        }
    }
}

void CozyKnightMainDlg::OnDeleteAllTask()
{
    if(m_core != NULL)
    {
        DeleteAllListItem(IDC_TASK_LIST_CTRL);
        m_core->ClearTask();
        m_searchList.DeleteAllItems();
    }
}

void CozyKnightMainDlg::InitComboBox()
{
    if(m_comboValueType.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER | CBS_DROPDOWNLIST | WS_VSCROLL))
    {
        m_comboValueType.AddString(_T("byte"));
        m_comboValueType.AddString(_T("2 bytes"));
        m_comboValueType.AddString(_T("4 bytes"));

        m_comboValueType.SetDlgCtrlID(12);
    }
}

void CozyKnightMainDlg::InitEditBox()
{
    if(m_edtValue.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER))
    {
        m_edtValue.SetDlgCtrlID(11);
    }
}

void CozyKnightMainDlg::InitSearchList()
{
    DWORD dwStyle = WS_CHILD | WS_TABSTOP | WS_VISIBLE | LVS_REPORT | LVS_SHOWSELALWAYS | LVS_SINGLESEL;

    if(m_searchList.Create(m_hWnd, NULL, NULL, dwStyle, 0, 101))
    {
        m_searchList.AddColumn(_T("地址"), 0);
        m_searchList.AddColumn(_T("类型"), 1);
        m_searchList.AddColumn(_T("数值"), 2);
        m_searchList.SetColumnWidth(0, 235);
        m_searchList.SetColumnWidth(1, 117);
        m_searchList.SetColumnWidth(2, 235);

        m_searchList.SetDlgCtrlID(IDC_SEARCH_LIST_CTRL);
    }
}

void CozyKnightMainDlg::InitSelectList()
{
    DWORD dwStyle = WS_CHILD | WS_TABSTOP | WS_VISIBLE | LVS_REPORT | LVS_SHOWSELALWAYS | LVS_SINGLESEL;

    if(m_selectList.Create(m_hWnd, NULL, NULL,  dwStyle, 0, 101))
    {
        m_selectList.AddColumn(_T("名称"), 0);
        m_selectList.AddColumn(_T("地址"), 1);
        m_selectList.AddColumn(_T("类型"), 2);
        m_selectList.AddColumn(_T("数值"), 3);
        m_selectList.AddColumn(_T("锁定"), 4);
        m_selectList.SetColumnWidth(0, 117);
        m_selectList.SetColumnWidth(1, 117);
        m_selectList.SetColumnWidth(2, 117);
        m_selectList.SetColumnWidth(3, 117);
        m_selectList.SetColumnWidth(4, 117);

        m_selectList.SetDlgCtrlID(10);
    }
}

void CozyKnightMainDlg::AppendSearchItem(LPVOID lpAddr, INT nSize, int nValue, int nItemId)
{
    CString strBuff;

    strBuff.Format(_T("%p"), lpAddr);
    m_searchList.AddItem(nItemId, 0, strBuff);

    strBuff.Format(_T("%d"), nSize);
    m_searchList.AddItem(nItemId, 1, strBuff);

    strBuff.Format(_T("%d"), nValue);
    m_searchList.AddItem(nItemId, 2, strBuff);
}

void CozyKnightMainDlg::AppendSelectedItem(LPVOID lpAddr, INT nSize, int nValue, int nItemId)
{
    CString strBuff;

    strBuff.Format(_T("地址%d"), m_nSelectCount++);
    m_selectList.AddItem(nItemId, 0, strBuff);

    strBuff.Format(_T("%p"), lpAddr);
    m_selectList.AddItem(nItemId, 1, strBuff);

    strBuff.Format(_T("%d"), nSize);
    m_selectList.AddItem(nItemId, 2, strBuff);

    strBuff.Format(_T("%d"), nValue);
    m_selectList.AddItem(nItemId, 3, strBuff);

    m_selectList.AddItem(nItemId, 4, _T("FALSE"));
}

void CozyKnightMainDlg::OnSearch()
{
    if(m_core != NULL)
    {
        int nItem = GetCurSelItem(IDC_TASK_LIST_CTRL);
        if(nItem >= 0 && nItem < m_core->GetTaskCount())
        {
            IKnightTask* pTask = m_core->GetTask(nItem);
            if(pTask)
            {
                int nLenght = m_edtValue.LineLength(1);
                CString strValue;

                m_edtValue.GetLine(1, strValue.GetBuffer(nLenght), nLenght);
                int nData = ::_ttoi(strValue);
                strValue.ReleaseBuffer(nLenght);

                pTask->Search(nData);

                ADDRESS_LIST addrlist = pTask->GetResultAddress();

                int i = 0;
                for(ADDRESS_LIST::iterator iter = addrlist.begin(); iter != addrlist.end(); ++iter)
                {
                    int nValue = 0;
                    m_core->ReadValue(*iter, nValue);
                    AppendSearchItem(iter->addr, iter->size, nValue, i++);
                }
            }
        }
    }
}

void CozyKnightMainDlg::OnTaskLBtnUp(int nListItem)
{
    if(nListItem < m_core->GetTaskCount())
    {
        IKnightTask* pTask = m_core->GetTask(nListItem);
        if(pTask)
        {
            m_searchList.DeleteAllItems();

            ADDRESS_LIST addrlist = pTask->GetResultAddress();

            int i = 0;
            for(ADDRESS_LIST::iterator iter = addrlist.begin(); iter != addrlist.end(); ++iter)
            {
                int nValue = 0;
                m_core->ReadValue(*iter, nValue);
                AppendSearchItem(iter->addr, iter->size, nValue, i++);
            }
        }
    }
}

LRESULT CozyKnightMainDlg::OnSearchDBListClick(LPNMHDR pnmh)
{
    if(m_core != NULL)
    {
        int nItem = GetCurSelItem(IDC_TASK_LIST_CTRL);
        if(nItem >= 0 && nItem < m_core->GetTaskCount())
        {
            IKnightTask* pTask = m_core->GetTask(nItem);
            if(pTask)
            {
                ADDRESS_LIST addrList = pTask->GetResultAddress();
                if(addrList.size() > 0)
                {
                    int nIndex = m_searchList.GetSelectedIndex();
                    if(nIndex >= 0)
                    {
                        int nValue = 0;
                        m_core->ReadValue(addrList[nIndex], nValue);

                        size_t nSavedCount = m_core->GetSavedAddressCount();
                        m_core->SaveAddress(addrList[nIndex]);
                        AppendSelectedItem(addrList[nIndex].addr, addrList[nIndex].size, nValue, nSavedCount);
                    }
                }
            }
        }
    }

    return S_OK;
}

LRESULT CozyKnightMainDlg::OnSelectedDBListClick(LPNMHDR pnmh)
{
    CozyKnightModifyDlg dlg;
    dlg.DoModal(m_hWnd);

    return S_OK;
}