#include "Componet/GeneraterZhihu.h"

NS_BEGIN

GeneraterZhihu::GeneraterZhihu(const std::string& name)
    :m_name(name)
{

}

void GeneraterZhihu::Start()
{
    static const std::string BaseUrl = "http://www.zhihu.com/people/";
    m_to->OnNewUrl(std::make_shared<std::string>(BaseUrl + m_name));
}

void GeneraterZhihu::Stop()
{
    // nothing to do
}

void GeneraterZhihu::To(Cozy::IUrlInPtr to)
{
    m_to = to;
}

NS_END