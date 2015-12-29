#include "Componet/ZhihuUrl2Result.h"
#include <iostream>

NS_BEGIN

void ZhihuUrl2Result::OnNewUrl(Cozy::StrPtr url)
{
    std::lock_guard<std::mutex> lock(m_mutex);
    std::cout << *url << std::endl;
}

NS_END