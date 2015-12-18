#include "Componet/ZhihuEnvInit.h"
#include "z_curl_warpper.h"
#include "z_mem_writer.h"
#include "z_file_writer.h"
#include "Wininet.h"
#include <iostream>
#include <string>
#include <iterator>
#include "Document.h"
#include "Node.h"

#pragma comment(lib, "Wininet.lib")

NS_BEGIN

bool ZhihuEnvInit::Init()
{
    if (!_IsLogin())
    {
        _Login();
        if (!_IsLogin())
        {
            return false;
        }
    }
    return true;
}

bool ZhihuEnvInit::_IsLogin()
{
    auto client = _MakeClient();
    if (client != nullptr)
    {
        client->SetUseAutoRedirect(false);
        return client->Perform("http://www.zhihu.com/settings/profile");
    }
    return false;
}

bool ZhihuEnvInit::_Login()
{
    std::srand((unsigned)time(nullptr));

    auto client = _MakeClient();
    client->SetEnableSSL(true);
    client->SetUseCookie(true);

    std::string ck;
    _GetCookie(ck);
    client->SetCookie(ck);

    zl::http::ZLMemWriter writer;
    client->SetWriteCallback(&writer);
    client->Perform("http://www.zhihu.com");

    auto str = std::string(writer.GetData(), writer.GetData() + writer.GetLength());

    CDocument doc;
    doc.parse(str.c_str());

    auto xsrf   = _GetXSRF(doc);
    auto cap    = _GetCaptcha(doc);

    _SetCookie(client->GetCookie());
    
    static const std::string Url = "http://www.zhihu.com/login/email";

    std::string content;
    std::string tmp;

    tmp = "_xsrf=" + xsrf;
    std::copy(tmp.begin(), tmp.end(), std::back_inserter(content));

    tmp = "&password=""951210";
    std::copy(tmp.begin(), tmp.end(), std::back_inserter(content));

    tmp = "&remember_me=true";
    std::copy(tmp.begin(), tmp.end(), std::back_inserter(content));

    tmp = "&email=805037171@163.com";
    std::copy(tmp.begin(), tmp.end(), std::back_inserter(content));

    client = _MakeClient();
    client->SetEnableSSL(true);
    client->SetUseCookie(true);

    client->SetMethod(HttpMethod::PostMethod);
    client->SetPostData(reinterpret_cast<unsigned char*>(&content[0]), content.size());
    client->AppendHeaderList("Host: www.zhihu.com");
    client->AppendHeaderList("Origin: http://www.zhihu.com");
    client->AppendHeaderList("Pragma: no-cache");
    client->AppendHeaderList("Referer: http://www.zhihu.com/");
    client->AppendHeaderList("X-Requested-With: XMLHttpRequest");

    _GetCookie(str);
    client->SetCookie(str);

    zl::http::ZLMemWriter memwriter;
    client->SetWriteCallback(&memwriter);

    if (client->Perform(Url))
    {
        _SetCookie(client->GetCookie());

        std::string result(memwriter.GetData(), memwriter.GetData() + memwriter.GetLength());
        std::cout << result << std::endl;
        return true;
    }
    return false;
}

ZhihuEnvInit::ClientPtr ZhihuEnvInit::_MakeClient()
{
    static const std::string ua = "User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)";

    auto ptr = std::make_shared<zl::http::ZLCurlWarpper>();
    ptr->SetTimeLimit(3);
    ptr->AppendHeaderList(ua);
    ptr->SetUseCookie(true);

    std::string cookie;
    _GetCookie(cookie);
    ptr->SetCookie(cookie);

    return ptr;
}

bool ZhihuEnvInit::_GetCookie(std::string& str)
{
    DWORD dwSize = 256;
    if (::InternetGetCookieA("http://www.zhihu.com", nullptr, nullptr, &dwSize))
    {
        std::vector<char> data(dwSize);
        if (::InternetGetCookieA("http://www.zhihu.com", nullptr, data.data(), &dwSize))
        {
            str = std::string(data.begin(), data.end());
            return true;
        }
    }
    return false;
}

bool ZhihuEnvInit::_SetCookie(const std::string& str)
{
    return ::InternetSetCookieA("http://www.zhihu.com", nullptr, str.c_str());
}

std::string ZhihuEnvInit::_GetXSRF(CDocument& doc)
{
   return doc.find("input[name=\"_xsrf\"]").nodeAt(0).attribute("value");
}

std::string ZhihuEnvInit::_GetCaptcha(CDocument& doc)
{
    static const std::string capUrl = "http://www.zhihu.com/captcha.gif?r=";

    auto Capid  = std::to_string(std::rand());
    auto actUrl = capUrl + Capid;

    auto client = _MakeClient();
    client->SetUseCookie(true);
    std::string str;
    _GetCookie(str);
    client->SetCookie(str);

    {
        zl::http::ZLFileWriter writer(Capid + ".gif");
        client->SetWriteCallback(&writer);

        if (!client->Perform(actUrl))
        {
            return "";
        }
    }

     _SetCookie(client->GetCookie());

    std::string result;
    std::cout << "cap" << std::endl;
    std::cin >> result;

    return result;
}

NS_END