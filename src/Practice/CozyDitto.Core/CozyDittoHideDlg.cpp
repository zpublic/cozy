#include "CozyDittoHideDlg.h"

CozyDittoHideDlg::CozyDittoHideDlg()
    :m_pCallback(nullptr)
{
}

CozyDittoHideDlg::~CozyDittoHideDlg()
{
}

void CozyDittoHideDlg::SetHotKeyCallback(HotKeyCallback callback)
{
    m_pCallback = callback;
}

LRESULT CozyDittoHideDlg::OnCreate(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
{
    return 0;
}

LRESULT CozyDittoHideDlg::OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
{
    ::MoveWindow(m_hWnd, 0, 0, 0, 0, FALSE);
    ::ShowWindow(m_hWnd, SW_HIDE);
    return 0;
}

LRESULT CozyDittoHideDlg::OnHotKey(UINT /*uMsg*/, WPARAM wParam, LPARAM lParam, BOOL& /*bHandled*/)
{
    if (m_pCallback != nullptr)
    {
        m_pCallback(wParam, lParam);
    }
    return 0;
}
