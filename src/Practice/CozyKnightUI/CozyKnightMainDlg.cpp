#include "StdAfx.h"
#include "CozyKnightMainDlg.h"
#include "CozyKnightSelectTargetDlg.h"

CozyKnightMainDlg::CozyKnightMainDlg(void)
	:CBkDialogViewImplEx<CozyKnightMainDlg>(IDR_MAIN), m_core(NULL), m_nTaskCount(0)
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
        }
    }
}

void CozyKnightMainDlg::OnDeleteAllTask()
{
    if(m_core != NULL)
    {
        DeleteAllListItem(IDC_TASK_LIST_CTRL);
        m_core->ClearTask();
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
    DWORD dwStyle = WS_CHILD|LVS_REPORT|LVS_SHOWSELALWAYS;

    if(m_searchList.Create(m_hWnd, NULL, NULL, dwStyle, 0, 101))
    {
        m_searchList.AddColumn(_T("地址"), 0);
        m_searchList.AddColumn(_T("类型"), 1);
        m_searchList.AddColumn(_T("数值"), 2);
        m_searchList.SetColumnWidth(0, 235);
        m_searchList.SetColumnWidth(1, 117);
        m_searchList.SetColumnWidth(2, 235);

        //m_searchList.AddItem(0, 0, _T("0x1000"));
        //m_searchList.AddItem(0, 1, _T("4 bytes"));
        //m_searchList.AddItem(0, 2, _T("2147483647"));

        m_searchList.SetDlgCtrlID(9);
    }
}

void CozyKnightMainDlg::InitSelectList()
{
    DWORD dwStyle = WS_CHILD|LVS_REPORT|LVS_SHOWSELALWAYS;

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

        //m_selectList.AddItem(0, 0, _T("地址1"));
        //m_selectList.AddItem(0, 1, _T("0x1000"));
        //m_selectList.AddItem(0, 2, _T("4 bytes"));
        //m_selectList.AddItem(0, 3, _T("2147483647"));
        //m_selectList.AddItem(0, 4, _T("2147483647"));

        m_selectList.SetDlgCtrlID(10);
    }
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
                CString strBuff;
                for(ADDRESS_LIST::iterator iter = addrlist.begin(); iter != addrlist.end(); ++iter)
                {
                    strBuff.Format(_T("%p"), iter->addr);
                    m_searchList.AddItem(0, 0, strBuff);
                    strBuff.Format(_T("%d"), iter->size);
                    m_searchList.AddItem(0, 1, strBuff);
                    int nValue = 0;
                    m_core->ReadValue(*iter, nValue);
                    strBuff.Format(_T("%d"), nValue);
                    m_searchList.AddItem(0, 2, strBuff);
                }
            }
        }
    }
}