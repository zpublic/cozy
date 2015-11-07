// CozyELFCppTester.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "../CozyElfCppEasySample/src/ElfReader.h"

int main()
{
    CozyElf::ElfReader reader;
    reader.Load("D:\\1.so");

    return 0;
}

