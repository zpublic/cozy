#ifndef __COZY_CAPTURE_WINDOW__
#define __COZY_CAPTURE_WINDOW__

#include "windows.h"
#include "atlbase.h"
#include "atlwin.h"
#include "atlimage.h"
enum class CaptureStatus
{
    S_None,
    S_Selecting,
    S_Selected,
};

class CozyCaptureWindow : public CWindowImpl<CozyCaptureWindow, CWindow, CWinTraits<WS_DLGFRAME, 0>>
{
public:
    static const DWORD FLG_TOCLIP = 1;
    static const DWORD FLG_TOFILE = 2;

    static const DWORD RET_FAILED   = 0;
    static const DWORD RET_CLIP     = 1;
    static const DWORD RET_FILE     = 2;

public:
    CozyCaptureWindow(DWORD dwFlags, LPCTSTR lpFilePath, LPDWORD lpResultState);
    ~CozyCaptureWindow();

private:
    BEGIN_MSG_MAP(CozyCaptureWindow)
        MESSAGE_HANDLER(WM_PAINT, OnPaint)
        MESSAGE_HANDLER(WM_NCPAINT, OnPaint)
        MESSAGE_HANDLER(WM_CREATE, OnCreate)
        MESSAGE_HANDLER(WM_CLOSE, OnClose)
        MESSAGE_HANDLER(WM_MOUSEMOVE, OnMouseMoveConvert)
        MESSAGE_HANDLER(WM_LBUTTONUP, OnLeftButtonUpConvert)
        MESSAGE_HANDLER(WM_LBUTTONDOWN, OnLeftButtonDownConvert)
        MESSAGE_HANDLER(WM_RBUTTONDBLCLK, OnRightButtonClickedConvert)
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
    LRESULT OnMouseMoveConvert(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnLeftButtonUpConvert(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnLeftButtonDownConvert(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);
    LRESULT OnRightButtonClickedConvert(UINT   uMsg, WPARAM   wParam, LPARAM   lParam, BOOL&   bHandled);

    void Exit();
    void BlendImage();
    bool SendImageToFile(const CImage &Img);
    bool SendImageToClipboard(CImage &Img);

    static void Point2Rect(const POINT &pa, const POINT &pb, RECT * rect);
    static LPARAM Point2LPARAM(const POINT &p);
private:
    CImage m_CaptureImg;
    CImage m_ResultImg;
    CImage m_MaskImg;

    bool m_IsMoved;
    CaptureStatus m_ThisStatus;

    POINT m_BeginPoint;
    POINT m_CurrPoint;

    LONG m_lWidth;
    LONG m_lHeight;

private:
    bool m_IsSaveToFile;
    bool m_IsSaveToClipboard;
    LPCTSTR m_lpFileName;
    LPDWORD m_lpResultState;
};

#endif // __COZY_CAPTURE_WINDOW__