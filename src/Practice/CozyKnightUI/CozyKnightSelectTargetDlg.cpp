#include "CozyKnightSelectTargetDlg.h"

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
    AppendListItem(1001, "<text pos=\"0,0,-0,-0\">123</text>");
}

void CozyKnightSelectTargetDlg::OnCalcle()
{
    OnBtnClose();
}