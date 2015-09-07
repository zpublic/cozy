#ifndef __COZY_CAPTURE_BASE__
#define __COZY_CAPTURE_BASE__

#define COZYAPI_EXPORT
#ifndef COZYAPI_EXPORT
#define COZYAPI _declspec(dllimport)
#else
#define COZYAPI _declspec(dllexport)
#endif 

class CozyCaptureWindow;
class CozyCaptureBase
{
public:
    bool CreateCaptureWindow(DWORD dwFlags, LPCTSTR lpFileName, LPDWORD lpResultState);

    void EnterMainLoop();
    
    CozyCaptureWindow* m_lpCaptureWindow;
};

extern "C" COZYAPI bool GetCaptureImage(DWORD dwFlags, LPCTSTR lpFileName, LPDWORD lpResultState);

#endif // __COZY_CAPTURE_BASE__