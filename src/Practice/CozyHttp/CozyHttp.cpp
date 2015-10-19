// CozyHttp.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "uv.h"
#include "CozyHttpServer.h"
#include "CozyHttpRequest.h"
#include "CozyHttpResponse.h"
#include <iostream>
#include "windows.h"

const char* not_found_path = ".\\404.html";

void DoNotFound(const CozyHttpRequest& req, CozyHttpResponse& rsp)
{
    rsp.SetStatusCode(Status_Code::Not_Found);

    HANDLE hFile = ::CreateFileA(not_found_path, GENERIC_WRITE | GENERIC_READ, 0, nullptr, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (hFile == INVALID_HANDLE_VALUE)
    {
        goto Exit1;
    }

    DWORD dwFileSize = ::GetFileSize(hFile, nullptr);
    if (dwFileSize <= 0)
    {
        goto Exit1;
    }
    char* lpbuff = new char[dwFileSize + 1];

    DWORD dwRead = 0;
    if (!::ReadFile(hFile, lpbuff, dwFileSize, &dwRead, nullptr))
    {
        goto Exit0;
    }
    lpbuff[dwFileSize] = 0;
    rsp.SetContext(std::string(lpbuff, lpbuff + dwFileSize));

Exit0:
    delete[] lpbuff;
Exit1:
    CloseHandle(hFile);
}

void ParserUrl(const CozyHttpRequest& req, CozyHttpResponse& rsp)
{
    std::string url = ".";
    url             += req.GetUrl();

    for (auto& c : url)
    {
        if (c == '/')
        {
            c = '\\';
        }
    }
    if (url == ".\\")
    {
        url = ".\\index.html";
    }

    HANDLE hFile = ::CreateFileA(url.c_str(), GENERIC_WRITE | GENERIC_READ, 0, nullptr, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (hFile == INVALID_HANDLE_VALUE)
    {
        DoNotFound(req, rsp);
        goto Exit1;
    }
    DWORD dwFileSize = ::GetFileSize(hFile, nullptr);
    if (dwFileSize <= 0)
    {
        rsp.SetStatusCode(Status_Code::OK);
        goto Exit1;
    }
    char* lpbuff = new char[dwFileSize + 1];

    DWORD dwRead = 0;
    if (!::ReadFile(hFile, lpbuff, dwFileSize, &dwRead, nullptr))
    {
        goto Exit0;
    }
    lpbuff[dwFileSize] = 0;
    rsp.SetContext(std::string(lpbuff, lpbuff + dwFileSize));
    rsp.SetStatusCode(Status_Code::OK);

Exit0:
    delete[] lpbuff;
Exit1:
    CloseHandle(hFile);
}

void OnWork(const CozyHttpRequest& req, CozyHttpResponse& rsp)
{
    short major = 0;
    short minor = 0;

    req.GetHttpVersion(&major, &minor);
    rsp.SetHttpVersion(major, minor);

    rsp.AddHeader("Content-Type", "text/html");
    ParserUrl(req, rsp);
}

int main()
{
    CozyHttpServer s = CozyHttpServer();
    s.Init("0.0.0.0", 80, 64);
    s.SetCallback(OnWork);
    s.Start();
    return 0;
}



