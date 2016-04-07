// CozyThunderTester.cpp : 定义控制台应用程序的入口点。
//

#include "../CozyThunder.Core/ICozyThunder.h"

#include "windows.h"
#include <iostream>
#include <mutex>

int main()
{
    LPCTSTR lpszLibrary     = TEXT("CozyThunder.Core.dll");
    HINSTANCE hInstLibrary  = ::LoadLibrary(lpszLibrary);

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
    auto task = pthunder->CreateTask(L"");

    task->SetRemotePath(L"https://cmake.org/files/v3.5/cmake-3.5.1-win32-x86.msi");
    task->SetLocalPath(L"D:/1.msi");

    typedef Cozy::ICozyThunderTaskCallback* (*CreateCallbackFunc)();
    auto createCallbackFun = (CreateCallbackFunc)GetProcAddress(hInstLibrary, "createCallback");

    auto callback = createCallbackFun();
    task->SetTaskCallback(callback);
    task->Start();
    task->Stop();

    task->Start(); //再次开始暂时不能用

Exit0:
    FreeLibrary(hInstLibrary);

    return 0;
}

