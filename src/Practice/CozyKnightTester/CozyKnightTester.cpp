// CozyKnightTester.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "CozyKnightCore.h"
#include <iostream>
#include "CozyTask.h"
#include "MemoryTester.h"

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

        CozyTask task;
        core.SearchFirst(MemoryTester(data.Data, 4), 4, task);
        std::cout << task.GetLength() << std::endl;

        const AddressInfo* p = task.GetData();
        for(int i = 0; i < task.GetLength(); ++i)
        {
            std::printf("%p\n", p[i].GetAddress());
        }

        system("pause");
        data.dwValue = 666;
        core.Search(task, MemoryTester(data.Data, 4));

        p = task.GetData();
        for(int i = 0; i < task.GetLength(); ++i)
        {
            std::printf("%p\n", p[i].GetAddress());
        }

        core.Detch();
    }
    system("pause");

    return 0;
}

