// CozyThunderTester.cpp : 定义控制台应用程序的入口点。
//

#include "../CozyThunder.Core/ICozyThunder.h"
#include "windows.h"
#include <iostream>

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
    task->SetRemotePath(L"https://www.baidu.com/img/bd_logo1.png");
    task->SetLocalPath(L"D:/1.png");
    task->Start();

    int n;
    std::cin >> n;
Exit0:
    FreeLibrary(hInstLibrary);

    return 0;
}

