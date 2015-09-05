#include "CozyCaptureWindow.h"

CozyCaptureWindow::CozyCaptureWindow()
{

}


CozyCaptureWindow::~CozyCaptureWindow()
{
}


LRESULT CozyCaptureWindow::OnCreate(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
    HWND hWnd   = ::GetDesktopWindow();
    HDC hdc     = ::GetWindowDC(hWnd);
    RECT rect;
    ::GetWindowRect(hWnd, &rect);

    m_lWidth    = rect.right - rect.left;
    m_lHeight   = rect.bottom - rect.top;
    MoveWindow(0, 0, m_lWidth, m_lHeight);

    HBITMAP hBmp = ::CreateCompatibleBitmap(hdc, m_lWidth, m_lHeight);

    BITMAP bmp;
    if (!::GetObject(hBmp, sizeof(BITMAP), (LPVOID)&bmp))
    {
        return 0;
    }

    int nBPP    = bmp.bmPlanes * bmp.bmBitsPixel;
    nBPP        = nBPP < 24 ? 24 : 32;

    if (!m_CaptureImg.Create(m_lWidth, m_lHeight, nBPP))
    {
        return 0;
    }

    CImageDC imageDc(m_CaptureImg);
    ::BitBlt(imageDc, 0, 0, m_lWidth, m_lHeight, hdc, 0, 0, SRCCOPY);
    
    return 0;
}

LRESULT CozyCaptureWindow::OnMouseMove(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
    return 0;
}

LRESULT CozyCaptureWindow::OnLeftButtonUp(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
    return 0;
}

LRESULT CozyCaptureWindow::OnLeftButtonDown(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
    return 0;
}

LRESULT CozyCaptureWindow::OnRightButtonClicked(UINT uMsg, WPARAM  wParam, LPARAM lParam, BOOL& bHandled)
{
    return 0;
}

LRESULT CozyCaptureWindow::OnPaint(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
    HDC hdc = GetWindowDC();
    m_CaptureImg.Draw(hdc, 0, 0, m_lWidth, m_lHeight);
    ReleaseDC(hdc);
    return 0;
}

LRESULT CozyCaptureWindow::OnClose(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled)
{
    ::PostQuitMessage(0);

    return 0;
}
