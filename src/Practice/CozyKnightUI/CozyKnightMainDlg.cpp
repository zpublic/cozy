#include "StdAfx.h"
#include "CozyKnightMainDlg.h"

CozyKnightMainDlg::CozyKnightMainDlg(void)
	:CBkDialogViewImplEx<CozyKnightMainDlg>(IDR_MAIN)
{

}

CozyKnightMainDlg::~CozyKnightMainDlg()
{

}

BOOL CozyKnightMainDlg::OnInitDialog(CWindow /*wndFocus*/, LPARAM /*lInitParam*/)
{
    return True;
}

void CozyKnightMainDlg::OnBtnClose()
{
	EndDialog(IDCLOSE);
}

void CozyKnightMainDlg::OnSysCommand(UINT nID, CPoint point)
{
	if(nID == SC_CLOSE)
	{
		if( ::IsWindowEnabled(m_hWnd))
		{
			OnBtnClose();
		}
	}
	else
	{
		SetMsgHandled(FALSE);
	}
}