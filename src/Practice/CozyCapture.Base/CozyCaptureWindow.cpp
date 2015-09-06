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
    if (m_ThisStatus == CaptureStatus::S_Selecting)
    {
        if (!m_IsMoved) m_IsMoved = true;
        m_CurrPoint = POINT{ GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam) };
    }
    
    return 0;
}

LRESULT CozyCaptureWindow::OnLeftButtonUp(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
    m_ThisStatus = CaptureStatus::S_Selected;
    return 0;
}

LRESULT CozyCaptureWindow::OnLeftButtonDown(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
    m_IsMoved = false;
    m_ThisStatus = CaptureStatus::S_Selecting;
    m_BeginPoint = POINT { GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam) };

    return 0;
}

LRESULT CozyCaptureWindow::OnRightButtonClicked(UINT uMsg, WPARAM  wParam, LPARAM lParam, BOOL& bHandled)
{
    if (m_ThisStatus == CaptureStatus::S_None)
    {
        Exit();
    }
    else if (m_ThisStatus == CaptureStatus::S_Selected)
    {
        m_IsMoved = false;
    }
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
    Exit();
    return 0;
}

void CozyCaptureWindow::BlendImage()
{
    CImageDC imageDc(m_CaptureImg);
    CImageDC resultDc(m_ResultImg);
    CImageDC maskDc(m_MaskImg);

    ::BitBlt(resultDc, 0, 0, m_lWidth, m_lHeight, imageDc, 0, 0, SRCCOPY);
    
    if (m_IsMoved)
    {
        RECT ActRect;

        if (m_BeginPoint.x > m_CurrPoint.x)
        {
            ActRect.left = m_CurrPoint.x;
            ActRect.right = m_BeginPoint.x;
        }
        else
        {
            ActRect.left = m_BeginPoint.x;
            ActRect.right = m_CurrPoint.x;
        }

        if (m_BeginPoint.y > m_CurrPoint.y)
        {
            ActRect.top = m_CurrPoint.y;
            ActRect.bottom = m_BeginPoint.y;
        }
        else
        {
            ActRect.top = m_BeginPoint.y;
            ActRect.bottom = m_CurrPoint.y;
        }

            BLENDFUNCTION bn;
        bn.AlphaFormat = 0;
        bn.BlendFlags = 0;
        bn.BlendOp = AC_SRC_OVER;
        bn.SourceConstantAlpha = 150;

        ::AlphaBlend(resultDc, 0, 0, m_lWidth, m_lHeight, maskDc, 0, 0, m_lWidth, m_lHeight, bn);
        ::BitBlt(resultDc, ActRect.left, ActRect.top, ActRect.right - ActRect.left, ActRect.bottom - ActRect.top, imageDc, ActRect.left, ActRect.top, SRCCOPY);
    }
}

LRESULT CozyCaptureWindow::OnMouseMoveConvert(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled)
{
    POINT cp { GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam) };
    ::ClientToScreen(m_hWnd, &cp);
    return OnMouseMove(uMsg, wParam, Point2LPARAM(cp), bHandled);
}

LRESULT CozyCaptureWindow::OnLeftButtonUpConvert(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled)
{
    POINT cp{ GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam) };
    ::ClientToScreen(m_hWnd, &cp);
    return OnLeftButtonUp(uMsg, wParam, Point2LPARAM(cp), bHandled);
}

LRESULT CozyCaptureWindow::OnLeftButtonDownConvert(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled)
{
    POINT cp{ GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam) };
    ::ClientToScreen(m_hWnd, &cp);
    return OnLeftButtonDown(uMsg, wParam, Point2LPARAM(cp), bHandled);
}

LRESULT CozyCaptureWindow::OnRightButtonClickedConvert(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled)
{
    POINT cp{ GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam) };
    ::ClientToScreen(m_hWnd, &cp);
    return OnRightButtonClicked(uMsg, wParam, Point2LPARAM(cp), bHandled);
}

void CozyCaptureWindow::Exit()
{
    ::PostQuitMessage(0);
}

LPARAM CozyCaptureWindow::Point2LPARAM(const POINT &p)
{
    return p.y << 16 | p.x;
}