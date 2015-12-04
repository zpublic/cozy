// CozySeeker.cpp : 定义控制台应用程序的入口点。
//

#pragma comment(lib, "libcurl.lib") // your lib here

#include <vector>
#include <atomic>
#include <iostream>
#include "Runner/BlockedUrl2ResultRunner.h"
#include "Runner/BlockedUrlGeneraterRunner.h"
#include "z_http_interface.h"
#include "z_http_client.h"

class Generater12306 : public Cozy::IUrlGenerater
{
public:
    virtual void Start()
    {
        if (m_to != nullptr)
        {
            for (auto u : m_urls)
            {
                m_to->OnNewUrl(u);
            }
        }
    }

    virtual void Stop()
    {

    }

    virtual void To(Cozy::IUrlInPtr to)
    {
        m_to = to;
    }

    void Add(Cozy::StrPtr str)
    {
        m_urls.push_back(str);
    }

private:
    Cozy::IUrlInPtr m_to;
    std::vector<Cozy::StrPtr> m_urls;
};

static const std::string Url = "https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=login&rand=sjrand&0.";

std::shared_ptr<std::string> MakeUrl()
{
    char c[11] = { 0 };
    for (int i = 0; i < 10; ++i)
    {
        c[i] = std::rand() % 10 + '0';
    }
    return std::make_shared<std::string>(Url + std::string(c));
}

class JpgDownloader : public Cozy::IUrl2Result
{
public:
    JpgDownloader()
        : m_httpClient(true), m_count(0)
    {

    }

    virtual void OnNewUrl(Cozy::StrPtr url)
    {
        char c[255];
        sprintf_s(c, "%d.jpg", m_count.fetch_add(1));
        m_httpClient.DownloadFile(*url, std::string(c));
        std::cout << *url << std::endl;
    }

private:
    zl::http::ZLHttpClient      m_httpClient;
    std::atomic<int>            m_count;
};

void Sample12306()
{
    auto p1 = std::make_shared<Cozy::BlockedUrlGeneraterRunner>();
    auto p2 = std::make_shared<Cozy::BlockedUrl2ResultRunner>();
    auto gen = std::make_shared<Generater12306>();
    for (int i = 0; i < 3; ++i)
    {
        gen->Add(MakeUrl());
    }
    auto res = std::make_shared<JpgDownloader>();

    p1->From(gen);
    p1->To(p2);
    p2->To(res);

    p2->Start();
    p1->Start();
}

int main()
{
    Sample12306();

    system("pause");
    return 0;
}