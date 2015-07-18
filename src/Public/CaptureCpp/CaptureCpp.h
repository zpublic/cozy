// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the CAPTURECPP_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// CAPTURECPP_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef CAPTURECPP_EXPORTS
#define CAPTURECPP_API __declspec(dllexport)
#else
#define CAPTURECPP_API __declspec(dllimport)
#endif

class CCaptureCpp
{
public:
    CCaptureCpp(void);
    
    bool GetCaptureData(LPBYTE lpResult);

    DWORD GetWindowBitmapSize(LPBITMAP lpBitmap);

    WORD GetClrBits(WORD wInput);

    DWORD AppendBitmapHeader(LPBYTE lpData, LPBITMAP lpBitmap);
};

CAPTURECPP_API CCaptureCpp CCaptureCppCppInstance;

CAPTURECPP_API int GetDesktopNum(void);

CAPTURECPP_API bool GetDesktopSize(int nIndex, int* w, int* h);

extern "C" CAPTURECPP_API bool GetCaptureData(LPBYTE lpResult);

extern "C" CAPTURECPP_API DWORD GetWindowBitmapSize(LPBITMAP bitmap);

extern "C" CAPTURECPP_API DWORD AppendBitmapHeader(LPBYTE lpData, LPBITMAP lpBitmap);
