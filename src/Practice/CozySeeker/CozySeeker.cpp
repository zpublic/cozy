// CozySeeker.cpp : 定义控制台应用程序的入口点。
//

#include "Base//AsyncInvoker.hpp"

#include <string>
#include <iostream>

int main()
{
    Cozy::AsyncInvoker<int> inv;
    inv.SetCallback([](const int& i) {std::cout << i << std::endl; });

    inv.Start();
    for (int i = 0; i < 42; ++i)
    {
        inv.Add(42);
    }

    system("pause");
    inv.Stop();

    return 0;
}

