#ifndef __COZY_RUNNER_BLOCKED_URL_GENERATER_RUNNER__
#define __COZY_RUNNER_BLOCKED_URL_GENERATER_RUNNER__

#include "Base/CozyInterface.h"

NS_BEGIN

class BlockedUrlGeneraterRunner :public std::enable_shared_from_this<BlockedUrlGeneraterRunner>, public IUrlGeneraterRunner
{
public:
    BlockedUrlGeneraterRunner();
    ~BlockedUrlGeneraterRunner();

    // IUrlGeneraterRunner
    void OnNewUrl(StrPtr url);
    void From(IUrlGeneraterPtr i);
    void To(IUrlInPtr to);
    void Start();
    void Stop();

private:
    IUrlGeneraterPtr    m_gen;
    IUrlInPtr           m_to;
};

NS_END

#endif // __COZY_RUNNER_BLOCKED_URL_GENERATER_RUNNER__