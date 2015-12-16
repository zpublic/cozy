#ifndef __COZY_COMPONET_ZHIHU_URL_TO_RESULT__
#define __COZY_COMPONET_ZHIHU_URL_TO_RESULT__

#include "Base/CozyInterface.h"
#include <mutex>

NS_BEGIN

class ZhihuUrl2Result : public IUrl2Result
{
public:
    virtual void OnNewUrl(Cozy::StrPtr url);

private:
    std::mutex m_mutex;
};

NS_END

#endif // __COZY_COMPONET_ZHIHU_URL_TO_RESULT__