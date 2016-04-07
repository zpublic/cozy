// CozyThunderTester.cpp : 定义控制台应用程序的入口点。
//

#include "../CozyThunder.Core/ICozyThunder.h"

#include <iostream>
#include <mutex>
#include "windows.h"
#include "TestCallback.h"

int main()
{
    LPCTSTR lpszLibrary     = TEXT("CozyThunder.Core.dll");
    HINSTANCE hInstLibrary  = ::LoadLibrary(lpszLibrary);
    TestCallback cb;
    if (hInstLibrary == nullptr)
    {
        std::cout << "load library error" << std::endl;
        goto Exit0;
    }

    typedef Cozy::ICozyThunder* (*CreateInstanceFunc)();
    auto createFunc = (CreateInstanceFunc)GetProcAddress(hInstLibrary, "createThunder");
    if (createFunc == nullptr)
    {
        std::cout << "load function error" << std::endl;
        goto Exit0;
    }

    Cozy::ICozyThunder* pthunder = createFunc();
    auto task = pthunder->CreateTask(L"D:/qq.exe.cfg");

    task->SetRemotePath(L"http://dldir1.qq.com/qqfile/qq/QQ8.2/17724/QQ8.2.exe");
    task->SetLocalPath(L"D:/qq.exe");

    task->SetTaskCallback(&cb);
    task->Start();
    task->Stop();
    pthunder->SaveTask(task);

    task->Start();

    cb.Wait();
    task->Stop();
    pthunder->SaveTask(task);
    pthunder->ClearTask(task);

    /*auto task = pthunder->LoadTask(L"D:/qq.exe.cfg");
    task->SetTaskCallback(&cb);
    task->Start();
    cb.Wait();
    task->Stop();*/
    

Exit0:
    ::FreeLibrary(hInstLibrary);

    return 0;
}

