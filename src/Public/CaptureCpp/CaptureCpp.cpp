// CaptureCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CaptureCpp.h"

CAPTURECPP_API CCaptureCpp CcaptureCppInstance;

CCaptureCpp::CCaptureCpp(void)
{

}

void GetWindowSize(HWND hwnd, POINT *pResult)
{
    RECT rect;
    POINT point;
    ::GetWindowRect(hwnd, &rect);
    point.x = rect.right - rect.left;;
    point.y = rect.bottom - rect.top;
    *pResult = point;
}

WORD CCaptureCpp::GetClrBits(WORD wInput)
{
    WORD cClrBits = wInput;
    if (cClrBits == 1)
        cClrBits = 1;
    else if (cClrBits <= 4)
        cClrBits = 4;
    else if (cClrBits <= 8)
        cClrBits = 8;
    else if (cClrBits <= 16)
        cClrBits = 16;
    else if (cClrBits <= 24)
        cClrBits = 24;
    else cClrBits = 32;
    return cClrBits;
}

DWORD CCaptureCpp::GetWindowBitmapSize()
{
    HWND hwnd       = ::GetDesktopWindow();
    HDC hdc         = ::GetWindowDC(hwnd);
    POINT size;
    GetWindowSize(hwnd, &size);
    HDC memHdc      = ::CreateCompatibleDC(hdc);
    HBITMAP hBmp    = ::CreateCompatibleBitmap(hdc, size.x, size.y);
    ::SelectObject(memHdc, hBmp);
    ::BitBlt(memHdc, 0, 0, size.x, size.y, hdc, 0, 0, SRCCOPY);

    BITMAP bmp;
    WORD cClrBits;
    if (!::GetObject(hBmp, sizeof(BITMAP), (LPVOID)&bmp))
    {
        return 0;
    }

    cClrBits = GetClrBits((WORD)(bmp.bmPlanes * bmp.bmBitsPixel));    

    ::ReleaseDC(hwnd, hdc);

    return (bmp.bmWidth + 7) / 8 * bmp.bmHeight * cClrBits + sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER) + (1 << cClrBits) *sizeof(RGBQUAD);
}

bool CCaptureCpp::GetCaptureData(LPBYTE lpResult)
{
    if (lpResult == nullptr) return 0;

    HWND hwnd       = ::GetDesktopWindow();
    HDC hdc         = ::GetWindowDC(hwnd);
    POINT size;
    GetWindowSize(hwnd, &size);
    HDC memHdc      = ::CreateCompatibleDC(hdc);
    HBITMAP hBmp    = ::CreateCompatibleBitmap(hdc, size.x, size.y);
    ::SelectObject(memHdc, hBmp);
    ::BitBlt(memHdc, 0, 0, size.x, size.y, hdc, 0, 0, SRCCOPY);

    BITMAP bmp;
    PBITMAPINFO pbmi;
    WORD cClrBits;
    if (!::GetObject(hBmp, sizeof(BITMAP), (LPVOID)&bmp))
    {
        return false;
    }

    cClrBits = GetClrBits((WORD)(bmp.bmPlanes * bmp.bmBitsPixel));

    if (cClrBits != 24)
    {
        pbmi = (PBITMAPINFO)::LocalAlloc(LPTR, sizeof(BITMAPINFOHEADER) + sizeof(RGBQUAD) * (1 << cClrBits));
    }
    else
    {
        pbmi = (PBITMAPINFO)::LocalAlloc(LPTR, sizeof(BITMAPINFOHEADER));
    }

    pbmi->bmiHeader.biSize          = sizeof(BITMAPINFOHEADER);
    pbmi->bmiHeader.biWidth         = bmp.bmWidth;
    pbmi->bmiHeader.biHeight        = bmp.bmHeight;
    pbmi->bmiHeader.biPlanes        = bmp.bmPlanes;
    pbmi->bmiHeader.biBitCount      = bmp.bmBitsPixel;

    if (cClrBits < 24)
        pbmi->bmiHeader.biClrUsed   = (1 << cClrBits);

    pbmi->bmiHeader.biCompression   = BI_RGB;
    pbmi->bmiHeader.biSizeImage     = (pbmi->bmiHeader.biWidth + 7) / 8 * pbmi->bmiHeader.biHeight * cClrBits;
    pbmi->bmiHeader.biClrImportant  = 0;

    BITMAPFILEHEADER hdr;
    PBITMAPINFOHEADER pbih;
    LPBYTE lpBits;

    pbih    = (PBITMAPINFOHEADER)pbmi;
    lpBits  = (LPBYTE)::GlobalAlloc(GMEM_FIXED, pbih->biSizeImage);
    ::GetDIBits(memHdc, hBmp, 0, (WORD)pbih->biHeight, lpBits, pbmi, DIB_RGB_COLORS);

    hdr.bfType = 0x4d42;
    hdr.bfSize = (DWORD)(sizeof(BITMAPFILEHEADER) + pbih->biSize + pbih->biClrUsed * sizeof(RGBQUAD) + pbih->biSizeImage);
    hdr.bfReserved1 = 0;
    hdr.bfReserved2 = 0;
    hdr.bfOffBits   = (DWORD)sizeof(BITMAPFILEHEADER) + pbih->biSize + pbih->biClrUsed * sizeof(RGBQUAD);
    
    DWORD offset = 0;
    ::CopyMemory(lpResult + offset, (LPVOID)&hdr, sizeof(BITMAPFILEHEADER));
    offset += sizeof(BITMAPFILEHEADER);

    ::CopyMemory(lpResult + offset, (LPVOID)pbih, sizeof(BITMAPINFOHEADER) + pbih->biClrUsed * sizeof(RGBQUAD));
    offset += sizeof(BITMAPINFOHEADER) + pbih->biClrUsed * sizeof(RGBQUAD);

    ::CopyMemory(lpResult + offset, (LPSTR)lpBits, pbih->biSizeImage);
    
    ::LocalFree(pbmi);
    ::GlobalFree((HGLOBAL)lpBits);
    ::ReleaseDC(hwnd, hdc);
    return true;
}


CAPTURECPP_API int GetDesktopNum(void)
{
    int dspNum = ::GetSystemMetrics(SM_CMONITORS);
    return dspNum;
}

int gMonitorIndex = 0;
int gMonitorCount = 0;
RECT gMonitorSize[10];

BOOL CALLBACK MonitorEnumProc(HMONITOR hMonitor, HDC hdcMonitor, LPRECT lprcMonitor, LPARAM dwData)
{
    if (gMonitorIndex < 10)
    {
        gMonitorSize[gMonitorIndex] = *lprcMonitor;
        gMonitorIndex++;
        gMonitorCount++;
    }
    return TRUE;
}

CAPTURECPP_API bool GetDesktopSize(int nIndex, int* w, int* h)
{
    gMonitorIndex = 0;
    gMonitorCount = 0;
    ::EnumDisplayMonitors(NULL, NULL, MonitorEnumProc, 0);
    if (nIndex < gMonitorCount)
    {
        *w = gMonitorSize[nIndex].right - gMonitorSize[nIndex].left;
        *h = gMonitorSize[nIndex].bottom - gMonitorSize[nIndex].top;
        return true;
    }
    return false;
}

CAPTURECPP_API bool GetCaptureData(LPBYTE lpResult)
{
    return CCaptureCppCppInstance.GetCaptureData(lpResult);
}

CAPTURECPP_API DWORD GetWindowBitmapSize()
{
    return CCaptureCppCppInstance.GetWindowBitmapSize();
}