#ifndef __COZY_CAPTURE_WINDOW__
#define __COZY_CAPTURE_WINDOW__

#include "windows.h"
#include "atlbase.h"
#include "atlwin.h"
#include "atlimage.h"

class CozyCaptureWindow : public CWindowImpl<CozyCaptureWindow, CWindow, CWinTraits<WS_DLGFRAME, 0>>
{
public:
    CozyCaptureWindow();
    ~CozyCaptureWindow();

private:
    BEGIN_MSG_MAP(CozyCaptureWindow)
        MESSAGE_HANDLER(WM_PAINT, OnPaint)
        MESSAGE_HANDLER(WM_NCPAINT, OnPaint)
        MESSAGE_HANDLER(WM_CREATE, OnCreate)
        MESSAGE_HANDLER(WM_CLOSE, OnClose)
        MESSAGE_HANDLER(WM_MOUSEMOVE, OnMouseMove)
        MESSAGE_HANDLER(WM_LBUTTONUP, OnLeftButtonUp)
        MESSAGE_HANDLER(WM_LBUTTONDOWN, OnLeftButtonDown)
        MESSAGE_HANDLER(WM_RBUTTONDBLCLK, OnRightButtonClicked)
        MESSAGE_HANDLER(WM_NCMOUSEMOVE, OnMouseMove)
        MESSAGE_HANDLER(WM_NCLBUTTONUP, OnLeftButtonUp)
        MESSAGE_HANDLER(WM_NCLBUTTONDOWN, OnLeftButtonDown)
        MESSAGE_HANDLER(WM_NCRBUTTONDBLCLK, OnRightButtonClicked)
    END_MSG_MAP();

    LRESULT OnPaint(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnClose(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnCreate(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnMouseMove(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnLeftButtonUp(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnLeftButtonDown(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnRightButtonClicked(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
private:
    CImage m_CaptureImg;

    LONG m_lWidth;
    LONG m_lHeight;
};

#endif // __COZY_CAPTURE_WINDOW__