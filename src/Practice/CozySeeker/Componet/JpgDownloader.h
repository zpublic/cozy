#ifndef __COZY_COMPONET_JPGDOWNLOADER__
#define __COZY_COMPONET_JPGDOWNLOADER__

#include "Base/CozyInterface.h"
#include <atomic>
#include "z_http_client.h"

NS_BEGIN

class JpgDownloader : public IUrl2Result
{
public:
    JpgDownloader();

    virtual void OnNewUrl(Cozy::StrPtr url);

private:
    zl::http::ZLHttpClient      m_httpClient;
    std::atomic<int>            m_count;
};

NS_END

#endif // __COZY_COMPONET_JPGDOWNLOADER__
