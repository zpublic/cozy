// stdafx.h : 标准系统包含文件的包含文件，
// 或是经常使用但不常更改的
// 特定于项目的包含文件
//

#pragma once

// 如果必须将位于下面指定平台之前的平台作为目标，请修改下列定义。
// 有关不同平台对应值的最新信息，请参考 MSDN。
#define WINVER			0x0500		// 将此值更改为相应的值，以适用于 Windows 的其他版本。
#define _WIN32_WINNT	0x0501	// 将此值更改为相应的值，以适用于 Windows 的其他版本。
#define _RICHEDIT_VER	0x0200
#define _WIN32_IE		0x0600	// 将此值更改为相应的值，以适用于 IE 的其他版本。

#define _CRT_SECURE_NO_WARNINGS
#define _CRT_NON_CONFORMING_SWPRINTFS

#define _WTL_NO_CSTRING
#define _WTL_NO_WTYPES

#include <atlbase.h>
#include <atlstr.h>
#include <atltypes.h>
#include <atlapp.h>

extern CAppModule _Module;
#include<atlwin.h>
#include <atlcrack.h>
#include <atlsplit.h>
#include <atlframe.h>
#include <atlgdi.h>
#include <atlctrls.h>
#include <atlctrlx.h>
#include <atlmisc.h>
#include <tinyxml/tinyxml.h>
#include "BkWin/CBkDialogViewImplEx.h"
#include "bkres/bkres.h"

