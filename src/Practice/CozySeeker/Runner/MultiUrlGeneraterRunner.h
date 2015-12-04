#ifndef __COZY_RUNNER_MULTI_URL_GENERATER_RUNNER__
#define __COZY_RUNNER_MULTI_URL_GENERATER_RUNNER__

#include "Base/CozySeekerDef.h"
#include "Base/CozyInterface.h"
#include "Base/AsyncInvoker.hpp"
#include <functional>
#include <vector>

NS_BEGIN

class MultiUrlGeneraterRunner : public std::enable_shared_from_this<MultiUrlGeneraterRunner>, public IUrlGeneraterRunner
{
public:
    MultiUrlGeneraterRunner(int threadCount /*= 1*/);
    virtual ~MultiUrlGeneraterRunner();

    // IUrlGeneraterRunner
    virtual void Start();
    virtual void Stop();
    virtual void To(IUrlInPtr to);
    virtual void From(IUrlGeneraterPtr ptr);
    virtual void OnNewUrl(StrPtr url);

protected:
    void OnInvoke(StrPtr ptr);

private:
    std::vector<IUrlGeneraterPtr>   m_Gens;
    std::vector<IUrlInPtr>          m_Tos;

    AsyncInvoker<StrPtr>            m_AsyncInvoker;
};

NS_END

#endif // __COZY_RUNNER_MULTI_URL_GENERATER_RUNNER__