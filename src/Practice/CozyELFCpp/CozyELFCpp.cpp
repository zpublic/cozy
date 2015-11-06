// CozyELFCpp.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "Src/ELFReader.h"

void test()
{
    ELFBase* h = ELFReader::Load("");
}

