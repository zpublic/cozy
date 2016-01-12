// CozyKnightTester.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "iknight.h"
#include <iostream>

int _tmain(int argc, _TCHAR* argv[])
{
    /*LoadLibrary;
    GetProcAddress("GetInterface");
    IKnight* knight = GetInterface();
    knight->Attach(hProcess);

    IKnightTask* task1 = knight->CreateTask();
    task1->Search(1);
    task1->SearchRange(1, 10);
    ADDRESS_LIST r = task1->GetResultAddress();

    knight->SaveAddress(r);
    knight->ModifyValue(r, 100);*/

    HMODULE hDllLib = ::LoadLibrary(_T("CozyKnightCore.dll"));
    if(!hDllLib)
    {
        return 0;
    }

    typedef IKnight* (*GetInstanceFunc)();
    GetInstanceFunc lpfnGetInstance = (GetInstanceFunc)::GetProcAddress(hDllLib, "GetInstance");

    IKnight* core = lpfnGetInstance();

    DWORD dwPid = 0;
    std::cin >>dwPid;

    HANDLE hProcess = ::OpenProcess(PROCESS_ALL_ACCESS, FALSE, dwPid);
    if(hProcess != NULL)
    {
        core->Attach(hProcess);

        IKnightTask* task = core->CreateTask();

        task->SearchDoubleWord(42);
        ADDRESS_LIST addrList = task->GetResultAddress();
        std::cout << addrList.size() << std::endl;

        typedef std::vector<ADDRESS_INFO>::iterator AddrIter;
        for(AddrIter iter = addrList.begin(); iter != addrList.end(); ++iter)
        {
            std::printf("%p\n", iter->addr);
        }

        core->DeleteTask(task);

        core->Detach();
        core->Release();
    }
    system("pause");

    return 0;
}

