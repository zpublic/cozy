#ifndef __COZY_RUNNER_BLOCKED_URL_TO_RESULT_RUNNER__
#define __COZY_RUNNER_BLOCKED_URL_TO_RESULT_RUNNER__

#include "Base/CozyInterface.h"

NS_BEGIN

class BlockedUrl2ResultRunner : public IUrl2ResultRunner
{
public:
    BlockedUrl2ResultRunner();
    ~BlockedUrl2ResultRunner();

    // IUrl2ResultRunner
    virtual void OnNewUrl(StrPtr url);
    virtual void To(IUrl2ResultPtr to);

    virtual void Start();
    virtual void Stop();

private:
    IUrl2ResultPtr m_trans;
};

NS_END

#endif // __COZY_RUNNER_BLOCKED_URL_TO_RESULT_RUNNER__