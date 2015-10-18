// CozyHttp.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "uv.h"
#include "CozyHttpServer.h"
#include "CozyHttpRequest.h"
#include "CozyHttpResponse.h"
#include <iostream>

void OnWork(const CozyHttpRequest& req, CozyHttpResponse& rsp)
{
    short major = 0;
    short minor = 0;

    rsp.SetStatusCode(Status_Code::OK);

    req.GetHttpVersion(&major, &minor);
    rsp.SetHttpVersion(major, minor);

    rsp.AddHeader("Content-Type", "text/html");
    rsp.SetContext(req.GetUrl());
}

int main()
{
    CozyHttpServer s = CozyHttpServer();
    s.Init("0.0.0.0", 80, 64);
    s.SetCallback(OnWork);
    s.Start();
    return 0;
}



