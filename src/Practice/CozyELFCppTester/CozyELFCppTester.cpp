// CozyELFCppTester.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "../CozyELFCpp/Src/ELFReader.h"

int main()
{
    auto ret = ELFReader::Load("D:\\1.so");
    return 0;
}

