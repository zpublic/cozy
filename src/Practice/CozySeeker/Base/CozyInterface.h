#ifndef __COZY_INTERFACE_URL_IN__
#define __COZY_INTERFACE_URL_IN__

#include "Base/CozySeekerDef.h"

NS_BEGIN

class IUrlIn
{
public:
    virtual void OnNewUrl(StrPtr url) = 0;
};

class IUrlOut
{
public:
    virtual void To(IUrlInPtr to) = 0;
};

class IControllable
{
public:
    virtual void Start()    = 0;
    virtual void Stop()     = 0;
};

class IUrl2Result : public IUrlIn
{

};

class IUrl2Url : public IControllable, public IUrlIn, public IUrlOut
{

};

class IUrlGenerater : public IControllable, public IUrlOut
{

};

class IUrl2ResultRunner : public IControllable, public IUrlIn
{
public:
    virtual void To(IUrl2ResultPtr to) = 0;
};

class IUrl2UrlRunner : public IControllable, public IUrlIn, public IUrlOut
{
public:
    virtual void SetProcessor(IUrl2UrlPtr p) = 0;
};

class IUrlGeneraterRunner : public IControllable, public IUrlIn, public IUrlOut
{
public:
    virtual void From(IUrlGeneraterPtr i) = 0;
};

NS_END

#endif // __COZY_INTERFACE_URL_IN__
