// CozySeeker.cpp : 定义控制台应用程序的入口点。
//

#pragma comment(lib, "D:\\curl\\lib\\Debug\\libcurl_imp.lib") // your lib here

#include "Base/CozyInterface.h"
#include "Tester/Tester12306.h"

int main()
{
    Cozy::ISeekerTesterPtr tester = std::make_shared<Cozy::Tester12306>(5);
    tester->Test();

    system("pause");
    return 0;
}