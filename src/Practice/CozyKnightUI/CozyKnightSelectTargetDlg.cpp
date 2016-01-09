#include "CozyKnightSelectTargetDlg.h"
#include "tlhelp32.h" 

CozyKnightSelectTargetDlg::CozyKnightSelectTargetDlg(HANDLE& hTarget)
    :CBkDialogViewImplEx<CozyKnightSelectTargetDlg>(IDR_SELECT_TARGET), m_hTarget(hTarget)
{
}

CozyKnightSelectTargetDlg::~CozyKnightSelectTargetDlg(void)
{
}

void CozyKnightSelectTargetDlg::OnBtnClose()
{
    EndDialog(IDCLOSE);
}

void CozyKnightSelectTargetDlg::OnOk()
{
    int nItem = GetCurSelItem(IDC_PROCESS_LIST_CTRL);
    if(nItem >= 0 && nItem < m_vecPid.size())
    {
        HANDLE hProcess = ::OpenProcess(PROCESS_ALL_ACCESS, FALSE, m_vecPid[nItem]);
        if(hProcess != NULL)
        {
            m_hTarget = hProcess;
        }
    }

    EndDialog(IDOK);
}

void CozyKnightSelectTargetDlg::OnCalcle()
{
    EndDialog(IDCANCEL);
}

void CozyKnightSelectTargetDlg::OnLDbClick(int nItem)
{
    if(nItem >= 0 && nItem < m_vecPid.size())
    {
        HANDLE hProcess = ::OpenProcess(PROCESS_ALL_ACCESS, FALSE, m_vecPid[nItem]);
        if(hProcess != NULL)
        {
            m_hTarget = hProcess;
        }
        EndDialog(IDOK);
    }
}

void CozyKnightSelectTargetDlg::RefreshProcess()
{
    m_vecPid.clear();
    DeleteAllListItem(IDC_PROCESS_LIST_CTRL);

    PROCESSENTRY32 pe32;
    pe32.dwSize = sizeof(pe32);

    CString cs;

    HANDLE hProcessSnap = ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if(hProcessSnap != INVALID_HANDLE_VALUE)
    {
        bool bMore = ::Process32First(hProcessSnap, &pe32);
        while(bMore)
        {
            m_vecPid.push_back(pe32.th32ProcessID);
            cs.Format(_T("<listitem height=\"20\"><text pos=\"0,0,-0,-0\">%d-%s</text></listitem>"), pe32.th32ProcessID, pe32.szExeFile);
            AppendListItem(IDC_PROCESS_LIST_CTRL, CW2A(cs), -1, FALSE);

            bMore =::Process32Next(hProcessSnap,&pe32);
        }
        UpdateLayoutList(IDC_PROCESS_LIST_CTRL);
    }
    ::CloseHandle(hProcessSnap);
}

LRESULT CozyKnightSelectTargetDlg::OnInitDialog(HWND hDlg, LPARAM lParam)
{
    RefreshProcess();
    return S_OK;
}
