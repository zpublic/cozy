#ifndef __COZY_COMPONET_GENERATER12306__
#define __COZY_COMPONET_GENERATER12306__ 

#include "Base/CozyInterface.h"
#include <vector>

NS_BEGIN

class Generater12306 : public Cozy::IUrlGenerater
{
public:
    virtual void Start();
    virtual void Stop();
    virtual void To(Cozy::IUrlInPtr to);
    void Add(Cozy::StrPtr str);

private:
    Cozy::IUrlInPtr m_to;
    std::vector<Cozy::StrPtr> m_urls;
};

NS_END

#endif // __COZY_COMPONET_GENERATER12306__
