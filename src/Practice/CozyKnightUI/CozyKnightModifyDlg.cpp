#include "CozyKnightModifyDlg.h"

CozyKnightModifyDlg::CozyKnightModifyDlg(void)
    :CBkDialogViewImplEx<CozyKnightModifyDlg>(IDR_MODIFY)
{
}

CozyKnightModifyDlg::~CozyKnightModifyDlg(void)
{
}

void CozyKnightModifyDlg::OnCloseBtn()
{
    EndDialog(IDCLOSE);
}