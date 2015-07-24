// CaptureCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CaptureCpp.h"

CAPTURECPP_API CCaptureCpp CcaptureCppInstance;

CCaptureCpp::CCaptureCpp(void)
{

}

void CCaptureCpp::GetWindowSize(HWND hwnd, LPLONG, LPLONG y)
{
    RECT rect;
    ::GetWindowRect(hwnd, &rect);
    *x = rect.right - rect.left;;
    *y = rect.bottom - rect.top;
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

bool CCaptureCpp::GetWindowHDC(HWND *lpHwnd, HDC *lpHdc)
{
    if (lpHwnd != nullptr && lpHdc != nullptr)
    {
        *lpHwnd = ::GetDesktopWindow();
        *lpHdc = ::GetWindowDC(*lpHwnd);
        return true;
    }
    return false;
}

DWORD CCaptureCpp::GetCaptureDataSize(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBITMAP lpBitmap)
{
    POINT size;

    if (width == 0 && height == 0)
    {
        GetWindowSize(hwnd, &(size.x), &(size.y));
    }
    else
    {
        size.x = width;
        size.y = height;
    }

    HDC memHdc = ::CreateCompatibleDC(hdc);
    HBITMAP hBmp = ::CreateCompatibleBitmap(hdc, size.x, size.y);
    ::SelectObject(memHdc, hBmp);

    BITMAP bmp;
    WORD cClrBits;
    if (!::GetObject(hBmp, sizeof(BITMAP), (LPVOID)&bmp))
    {
        return 0;
    }

    cClrBits = GetClrBits((WORD)(bmp.bmPlanes * bmp.bmBitsPixel));

    *lpBitmap = bmp;
    return (bmp.bmWidth + 7) / 8 * bmp.bmHeight * cClrBits + sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER) + (1 << cClrBits) *sizeof(RGBQUAD);
}

DWORD CCaptureCpp::GetCaptureData(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBYTE lpResult)
{
    POINT size;
    if (width == 0 && height == 0)
    {
        GetWindowSize(hwnd, &(size.x), &(size.y));
    }
    else
    {
        size.x = width;
        size.y = height;
    }
    HDC memHdc = ::CreateCompatibleDC(hdc);
    HBITMAP hBmp = ::CreateCompatibleBitmap(hdc, size.x, size.y);
    ::SelectObject(memHdc, hBmp);
    ::BitBlt(memHdc, x, y, size.x, size.y, hdc, 0, 0, SRCCOPY);

    BITMAP bmp;
    PBITMAPINFO pbmi;
    WORD cClrBits;
    if (!::GetObject(hBmp, sizeof(BITMAP), (LPVOID)&bmp))
    {
        return 0;
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

    pbmi->bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
    pbmi->bmiHeader.biWidth = bmp.bmWidth;
    pbmi->bmiHeader.biHeight = bmp.bmHeight;
    pbmi->bmiHeader.biPlanes = bmp.bmPlanes;
    pbmi->bmiHeader.biBitCount = bmp.bmBitsPixel;

    if (cClrBits < 24)
        pbmi->bmiHeader.biClrUsed = (1 << cClrBits);

    pbmi->bmiHeader.biCompression = BI_RGB;
    pbmi->bmiHeader.biSizeImage = (pbmi->bmiHeader.biWidth + 7) / 8 * pbmi->bmiHeader.biHeight * cClrBits;
    pbmi->bmiHeader.biClrImportant = 0;

    ::GetDIBits(memHdc, hBmp, 0, (WORD)(WORD)pbmi->bmiHeader.biHeight, lpResult, pbmi, DIB_RGB_COLORS);

    ::LocalFree(pbmi);
    return 1;
}

DWORD CCaptureCpp::AppendBitmapHeader(LPBYTE lpResult, LPBITMAP lpBitmap)
{
    WORD cClrBits = GetClrBits((WORD)(lpBitmap->bmPlanes * lpBitmap->bmBitsPixel));
    PBITMAPINFO pbmi;
    if (cClrBits != 24)
    {
        pbmi = (PBITMAPINFO)::LocalAlloc(LPTR, sizeof(BITMAPINFOHEADER) + sizeof(RGBQUAD) * (1 << cClrBits));
    }
    else
    {
        pbmi = (PBITMAPINFO)::LocalAlloc(LPTR, sizeof(BITMAPINFOHEADER));
    }
    pbmi->bmiHeader.biSize          = sizeof(BITMAPINFOHEADER);
    pbmi->bmiHeader.biWidth         = lpBitmap->bmWidth;
    pbmi->bmiHeader.biHeight        = lpBitmap->bmHeight;
    pbmi->bmiHeader.biPlanes        = lpBitmap->bmPlanes;
    pbmi->bmiHeader.biBitCount      = lpBitmap->bmBitsPixel;
    if (cClrBits < 24)
        pbmi->bmiHeader.biClrUsed   = (1 << cClrBits);
    pbmi->bmiHeader.biCompression   = BI_RGB;
    pbmi->bmiHeader.biSizeImage     = (pbmi->bmiHeader.biWidth + 7) / 8 * pbmi->bmiHeader.biHeight * cClrBits;
    pbmi->bmiHeader.biClrImportant  = 0;

    BITMAPFILEHEADER hdr;
    PBITMAPINFOHEADER pbih;
    pbih = (PBITMAPINFOHEADER)pbmi;

    hdr.bfType      = 0x4d42;
    hdr.bfSize      = (DWORD)(sizeof(BITMAPFILEHEADER) + pbih->biSize + pbih->biClrUsed * sizeof(RGBQUAD) + pbih->biSizeImage);
    hdr.bfReserved1 = 0;
    hdr.bfReserved2 = 0;
    hdr.bfOffBits   = (DWORD)sizeof(BITMAPFILEHEADER) + pbih->biSize + pbih->biClrUsed * sizeof(RGBQUAD);

    DWORD offset    = 0;
    ::CopyMemory(lpResult + offset, (LPVOID)&hdr, sizeof(BITMAPFILEHEADER));
    offset += sizeof(BITMAPFILEHEADER);

    ::CopyMemory(lpResult + offset, (LPVOID)pbih, sizeof(BITMAPINFOHEADER) + pbih->biClrUsed * sizeof(RGBQUAD));
    offset += sizeof(BITMAPINFOHEADER) + pbih->biClrUsed * sizeof(RGBQUAD);

    ::LocalFree(pbmi);
    return offset;
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

CAPTURECPP_API DWORD AppendBitmapHeader(LPBYTE lpData, LPBITMAP lpBitmap)
{
    return CCaptureCppCppInstance.AppendBitmapHeader(lpData, lpBitmap);
}

CAPTURECPP_API bool GetWindowHDC(HWND *lpHwnd, HDC *lpHdc)
{
    return CCaptureCppCppInstance.GetWindowHDC(lpHwnd, lpHdc);
}

CAPTURECPP_API DWORD GetCaptureData(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBYTE lpResult)
{
    return CCaptureCppCppInstance.GetCaptureData(hwnd, hdc, x, y, width, height, lpResult);
}

CAPTURECPP_API DWORD GetCaptureDataSize(HWND hwnd, HDC hdc, int x, int y, int width, int height, LPBITMAP lpBitmap)
{
    return CCaptureCppCppInstance.GetCaptureDataSize(hwnd, hdc, x, y, width, height, lpBitmap);
}

CAPTURECPP_API void GetWindowSize(HWND hwnd, LONG *x, LONG *y)
{
    return CCaptureCppCppInstance.GetWindowSize(hwnd, x, y);
}