#include "Componet/Generater12306.h"

NS_BEGIN

void Generater12306::Start()
{
    if (m_to != nullptr)
    {
        for (auto u : m_urls)
        {
            m_to->OnNewUrl(u);
        }
    }
}

void Generater12306::Stop()
{

}

void Generater12306::To(Cozy::IUrlInPtr to)
{
    m_to = to;
}

void Generater12306::Add(Cozy::StrPtr str)
{
    m_urls.push_back(str);
}

NS_END