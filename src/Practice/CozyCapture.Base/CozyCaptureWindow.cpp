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

    SetWindowPos(m_hWnd, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

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
    if (!m_ResultImg.Create(m_lWidth, m_lHeight, nBPP))
    {
        return 0;
    }
    if (!m_MaskImg.Create(m_lWidth, m_lHeight, nBPP))
    {
        return 0;
    }

    CImageDC maskDc(m_MaskImg);

    RECT r;
    r.left      = 0;
    r.top       = 0;
    r.bottom    = m_lHeight;
    r.right     = m_lWidth;
    ::FillRect(maskDc, &r, ::CreateSolidBrush(RGB(0, 0, 0)));

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
    BlendImage();
    m_ResultImg.Draw(hdc, 0, 0, m_lWidth, m_lHeight);
    ReleaseDC(hdc);
    return 0;
}

LRESULT CozyCaptureWindow::OnClose(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled)
{
    ::PostQuitMessage(0);

    return 0;
}

void CozyCaptureWindow::BlendImage()
{
    CImageDC imageDc(m_CaptureImg);
    CImageDC resultDc(m_ResultImg);
    CImageDC maskDc(m_MaskImg);

    ::BitBlt(resultDc, 0, 0, m_lWidth, m_lHeight, imageDc, 0, 0, SRCCOPY);
    
    BLENDFUNCTION bn;
    bn.AlphaFormat          = 0;
    bn.BlendFlags           = 0;
    bn.BlendOp              = AC_SRC_OVER;
    bn.SourceConstantAlpha  = 150;

    ::AlphaBlend(resultDc, 0, 0, m_lWidth, m_lHeight, maskDc, 0, 0, m_lWidth, m_lHeight, bn);
    ::BitBlt(resultDc, 100, 100, 300, 300, imageDc, 100, 100, SRCCOPY);
}