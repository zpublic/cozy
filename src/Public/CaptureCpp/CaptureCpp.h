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
    
    WORD GetClrBits(WORD wInput);

    DWORD AppendBitmapHeader(LPBYTE lpData, LPBITMAP lpBitmap);

    void GetWindowSize(HWND hwnd, POINT *pResult);

    bool GetWindowHDC(HWND *lpHwnd, HDC *lpHdc);

    DWORD GetCaptureDataSize(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBITMAP lpBitmap);

    DWORD GetCaptureData(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBYTE lpResult);

};

CAPTURECPP_API CCaptureCpp CCaptureCppCppInstance;

CAPTURECPP_API int GetDesktopNum(void);

CAPTURECPP_API bool GetDesktopSize(int nIndex, int* w, int* h);

extern "C" CAPTURECPP_API DWORD AppendBitmapHeader(LPBYTE lpData, LPBITMAP lpBitmap);

extern "C" CAPTURECPP_API bool GetWindowHDC(HWND *lpHwnd, HDC *lpHdc);

extern "C" CAPTURECPP_API DWORD GetCaptureDataSize(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBITMAP lpBitmap);

extern "C" CAPTURECPP_API DWORD GetCaptureData(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBYTE lpResult);

