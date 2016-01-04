#include "CozyKnightSelectTargetDlg.h"
#include "tlhelp32.h" 

CozyKnightSelectTargetDlg::CozyKnightSelectTargetDlg(HANDLE hTarget)
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
   
}

void CozyKnightSelectTargetDlg::OnCalcle()
{
    OnBtnClose();
}

void CozyKnightSelectTargetDlg::RefreshProcess()
{
    PROCESSENTRY32 pe32;
    pe32.dwSize = sizeof(pe32);

    HANDLE hProcessSnap = ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if(hProcessSnap == INVALID_HANDLE_VALUE)
    {
        bool bMore = ::Process32First(hProcessSnap, &pe32);
    }
     AppendListItem(1001, "<text pos=\"0,0,-0,-0\">123</text>");
}

void CozyKnightSelectTargetDlg::OnInitDialog()
{
    RefreshProcess();
}