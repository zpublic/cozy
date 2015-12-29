// cozy_knight.cpp : 定义应用程序的入口点。
//

#include "stdafx.h"
#include "cozy_knight.h"
#include "CozyKnightMainDlg.h"

void InitRes()
{
#define IDR_DEF_COMMON_SKIN		60000
#define IDR_DEF_COMMON_STYLE	60001
#define IDR_DEF_COMMON_STRING	60002

	BkPngPool::Reload();
	BkString::Load(IDR_DEF_COMMON_STRING);
	BkSkin::LoadSkins(IDR_DEF_COMMON_SKIN);
	BkStyle::LoadStyles(IDR_DEF_COMMON_STYLE);
}

int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
	InitRes();

	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	CozyKnightMainDlg win;
	win.DoModal();
	return 0;
}