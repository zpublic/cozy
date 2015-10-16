// CozyHttp.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "uv.h"
#include "CozyHttpServer.h"

int main()
{
    CozyHttpServer s = CozyHttpServer();
    s.Init("0.0.0.0", 80, 64);
    s.Start();
    return 0;
}

