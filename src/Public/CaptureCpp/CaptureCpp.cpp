// CaptureCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CaptureCpp.h"

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