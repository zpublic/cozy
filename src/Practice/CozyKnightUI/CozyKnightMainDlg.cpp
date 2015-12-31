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
    if(m_edtValue.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER))
    {
        m_edtValue.SetDlgCtrlID(11);
    }
    if(m_comboValueType.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER | CBS_DROPDOWNLIST | WS_VSCROLL))
    {
        m_comboValueType.AddString(_T("byte"));
        m_comboValueType.AddString(_T("word"));
        m_comboValueType.AddString(_T("double word"));

        m_comboValueType.SetDlgCtrlID(12);
    }
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