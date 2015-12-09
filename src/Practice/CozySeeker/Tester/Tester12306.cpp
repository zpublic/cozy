#include "Tester/Tester12306.h"

#include "Runner/BlockedUrl2ResultRunner.h"
#include "Runner/BlockedUrlGeneraterRunner.h"

#include "Componet/Generater12306.h"
#include "Componet/JpgDownloader.h"

NS_BEGIN

Tester12306::Tester12306(int times)
    :m_times(times)
{

}

void Tester12306::Test()
{
    auto p1 = std::make_shared<Cozy::BlockedUrlGeneraterRunner>();
    auto p2 = std::make_shared<Cozy::BlockedUrl2ResultRunner>();
    auto gen = std::make_shared<Generater12306>();
    for (int i = 0; i < m_times; ++i)
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

StrPtr Tester12306::MakeUrl()
{
    static const std::string Url = "https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=login&rand=sjrand&0.";

    char c[11] = { 0 };
    for (int i = 0; i < 10; ++i)
    {
        c[i] = std::rand() % 10 + '0';
    }
    return std::make_shared<std::string>(Url + std::string(c));
}

NS_END