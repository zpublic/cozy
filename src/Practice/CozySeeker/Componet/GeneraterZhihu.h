#ifndef __COZY_COMPONET_GENERATER_ZHIHU__
#define __COZY_COMPONET_GENERATER_ZHIHU__

#include "Base/CozyInterface.h"

NS_BEGIN

class GeneraterZhihu : public IUrlGenerater
{
public:
    GeneraterZhihu(const std::string& name);

    virtual void Start();
    virtual void Stop();
    virtual void To(Cozy::IUrlInPtr to);

private:
    std::string     m_name;
    IUrlInPtr       m_to;
};

NS_END

#endif // __COZY_COMPONET_GENERATER_ZHIHU__