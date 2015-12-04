#ifndef _COZY_RUNNER_BLOCKED_URL_TO_URL_RUNNER__
#define _COZY_RUNNER_BLOCKED_URL_TO_URL_RUNNER__

#include "Base/CozyInterface.h"

NS_BEGIN

class BlockedUrl2UrlRunner : public IUrl2UrlPtr
{
public:
    BlockedUrl2UrlRunner();
    ~BlockedUrl2UrlRunner();

    // IUrl2UrlPtr
    void SetProcessor(IUrl2UrlPtr p);
    void OnNewUrl(StrPtr url);
    void Start();
    void Stop();
    void To(IUrlInPtr to);

private:
    IUrlInPtr   m_urlIn;
    IUrl2UrlPtr m_Process;
};

NS_END

#endif // _COZY_RUNNER_BLOCKED_URL_TO_URL_RUNNER__