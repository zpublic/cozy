#include "CozyKnightModifyDlg.h"

CozyKnightModifyDlg::CozyKnightModifyDlg(CString& strName, ADDRESS_INFO& addrRef, int& nValue, BOOL& bCheck)
    :CBkDialogViewImplEx<CozyKnightModifyDlg>(IDR_MODIFY),
    m_StrName(strName), m_AddrRef(addrRef), m_Value(nValue), m_Lock(bCheck)
{

}

CozyKnightModifyDlg::~CozyKnightModifyDlg(void)
{

}

void CozyKnightModifyDlg::OnCloseBtn()
{
    EndDialog(IDCLOSE);
}

BOOL CozyKnightModifyDlg::OnInitDialog(CWindow /*wndFocus*/, LPARAM /*lInitParam*/)
{
    InitNameEdt();
    InitAddrEdt();
    InitValueEdt();
    InitTypeCombo();

    return TRUE;
}

void CozyKnightModifyDlg::OnOkBtn()
{
    UpdateModify();
    EndDialog(IDOK);
}

void CozyKnightModifyDlg::InitNameEdt()
{
    if(m_NameEdit.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER))
    {
        m_NameEdit.SetWindowText(m_StrName);
        m_NameEdit.SetDlgCtrlID(IDC_EDT_NAME);
    }
}

void CozyKnightModifyDlg::InitAddrEdt()
{
    if(m_AddrEdit.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER))
    {
        CString strAddr;
        strAddr.Format(_T("%p"), m_AddrRef.addr);

        m_AddrEdit.SetWindowText(strAddr);
        m_AddrEdit.SetDlgCtrlID(IDC_EDT_ADDR);
    }
}

void CozyKnightModifyDlg::InitValueEdt()
{
    if(m_ValueEdit.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER))
    {
        CString strValue;
        strValue.Format(_T("%d"), m_Value);

        m_ValueEdit.SetWindowText(strValue);
        m_ValueEdit.SetDlgCtrlID(IDC_EDT_VALUE);
    }
}

void CozyKnightModifyDlg::InitTypeCombo()
{
    if(m_TypeCombo.Create(m_hWnd, NULL, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER | CBS_DROPDOWNLIST | WS_VSCROLL))
    {
        m_TypeCombo.AddString(_T("byte"));
        m_TypeCombo.AddString(_T("2 bytes"));
        m_TypeCombo.AddString(_T("4 bytes"));

        m_TypeCombo.SetDlgCtrlID(IDC_COMBO_TYPE);
    }
}

void CozyKnightModifyDlg::UpdateModify()
{
    m_StrName   = GetEditText(m_NameEdit);
    m_Value     = ::_ttoi(GetEditText(m_ValueEdit));
    m_Lock      = GetItemCheck(IDC_CHK_LOCK);
}


CString CozyKnightModifyDlg::GetEditText(const CEdit& editCtrl)
{
    int nLenght = editCtrl.LineLength(1);
    CString strValue;
    editCtrl.GetLine(1, strValue.GetBuffer(nLenght), nLenght);
    strValue.ReleaseBuffer(nLenght);
    return strValue;
}