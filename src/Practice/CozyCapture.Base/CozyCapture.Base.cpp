// CozyCapture.Base.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "CozyCapture.Base.h"
#include "CozyCaptureWindow.h"

COZYAPI CozyCaptureBase g_CozyCaptureBaseInstance;

bool CozyCaptureBase::CreateCaptureWindow()
{
    m_lpCaptureWindow = new CozyCaptureWindow();
    m_lpCaptureWindow->Create(nullptr, CWindow::rcDefault);
    m_lpCaptureWindow->ShowWindow(SW_NORMAL);

    return true;
}

void CozyCaptureBase::EnterMainLoop()
{
    MSG   msg;
    while (::GetMessage(&msg, NULL, 0, 0))
    {
        ::TranslateMessage(&msg);
        ::DispatchMessage(&msg);
    }
    m_lpCaptureWindow->Detach();
    delete m_lpCaptureWindow;
    m_lpCaptureWindow = nullptr;
}

COZYAPI bool CreateCaptureWindow()
{
    return g_CozyCaptureBaseInstance.CreateCaptureWindow();
}


COZYAPI void EnterMainLoop()
{
    return g_CozyCaptureBaseInstance.EnterMainLoop();
}