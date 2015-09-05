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
    bool CreateCaptureWindow();

    void EnterMainLoop();
    
    CozyCaptureWindow* m_lpCaptureWindow;
};

extern "C" COZYAPI bool CreateCaptureWindow();
extern "C" COZYAPI void EnterMainLoop();

#endif // __COZY_CAPTURE_BASE__