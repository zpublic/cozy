// CozyKnightTester.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "CozyKnightCore.h"
#include <iostream>

union TestBlock
{
    DWORD dwValue;
    BYTE Data[4];
};

int _tmain(int argc, _TCHAR* argv[])
{
    CozyKnightCore core;
    DWORD dwPid = 0;
    std::cin >>dwPid;
    HANDLE hProcess = ::OpenProcess(PROCESS_ALL_ACCESS, FALSE, dwPid);
    if(hProcess != NULL)
    {
        core.Attch(hProcess);
        TestBlock data;
        data.dwValue = 42;

        std::vector<AddressInfo> vecData;
        core.SearchFirst(MemoryTester(data.Data, 4), 4, vecData);
        std::cout << vecData.size() << std::endl;

        for(int i = 0; i < vecData.size(); ++i)
        {
            std::printf("%p\n", vecData[i].GetAddress());
        }

        system("pause");
        data.dwValue = 666;
        core.Search(vecData, MemoryTester(data.Data, 4));
        for(int i = 0; i < vecData.size(); ++i)
        {
            std::printf("%p\n", vecData[i].GetAddress());
        }

        core.Detch();
    }
    system("pause");

    return 0;
}

