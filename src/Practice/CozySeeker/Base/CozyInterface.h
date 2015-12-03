#ifndef __COZY_INTERFACE_URL_IN__
#define __COZY_INTERFACE_URL_IN__

#include <string>

class IUrlIn
{
public:
    virtual void OnNewUrl(const std::string& url) = 0;
};

class IUrlOut
{
public:
    virtual void To(IUrlIn& to) = 0;
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
    virtual void To(IUrl2Result& to) = 0;
};

class IUrl2UrlRunner : public IControllable, public IUrlIn, public IUrlOut
{
public:
    virtual void SetProcessor(IUrl2Url& p) = 0;
}

class IUrlGeneraterRunner : public IControllable, public IUrlIn, public IUrlOut
{
public:
    virtual void From(IUrlGenerater& i) = 0;
}

#endif // __COZY_INTERFACE_URL_IN__
