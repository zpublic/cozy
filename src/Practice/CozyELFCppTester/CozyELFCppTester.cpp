// CozyELFCppTester.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include "../CozyElfCppEasySample/src/ElfReader.h"

int main()
{
    CozyElf::ElfReader reader;
    auto result = reader.Load("D:\\1.so");
    delete result;
    return 0;
}

