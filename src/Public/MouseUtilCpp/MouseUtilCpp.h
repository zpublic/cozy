// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the MOUSEUTILCPP_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// MOUSEUTILCPP_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef MOUSEUTILCPP_EXPORTS
#define MOUSEUTILCPP_API __declspec(dllexport)
#else
#define MOUSEUTILCPP_API __declspec(dllimport)
#endif

#include "windows.h"

enum BUTTON : DWORD
{
    LEFT    = 0,
    RIGHT   = 1,
    MIDDLE  = 2,
};

class CMouseUtilCpp
{
public:
    CMouseUtilCpp(void);

    bool GetCursorPosition(long* lpXPosition, long* lpYPosition);

    bool SetCursorPosition(int iXPosition, int iYPosition);

    bool CursorClip(const RECT* rect);

    void MouseEvent(DWORD dwFlags, DWORD dx, DWORD dy, DWORD dwData, ULONG_PTR dwExtraInfo);

    void MouseClick(BUTTON key, DWORD dx, DWORD dy);
};

extern MOUSEUTILCPP_API CMouseUtilCpp CMouseUtilCppInstance;

extern "C" MOUSEUTILCPP_API bool GetCursorPosition(long* lpXPosition, long* lpYPosition);

extern "C" MOUSEUTILCPP_API bool SetCursorPosition(int iXPosition, int iYPosition);

extern "C" MOUSEUTILCPP_API bool CursorClip(long lLeft, long lTop, long lRight, long lBottom);

extern "C" MOUSEUTILCPP_API bool CursorUnClip();

extern "C" MOUSEUTILCPP_API void MouseEvent(DWORD dwFlags, DWORD dx, DWORD dy, DWORD dwData, ULONG_PTR dwExtraInfo);

extern "C" MOUSEUTILCPP_API void MouseClick(BUTTON key, DWORD dx, DWORD dy);

extern "C" MOUSEUTILCPP_API void LeftClick(DWORD dx, DWORD dy);

extern "C" MOUSEUTILCPP_API void RightClick(DWORD dx, DWORD dy);

extern "C" MOUSEUTILCPP_API void MiddleClick(DWORD dx, DWORD dy);
