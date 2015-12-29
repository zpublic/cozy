#ifndef __COZY_COMPONET_ZHIHU_ENV_INIT__
#define __COZY_COMPONET_ZHIHU_ENV_INIT__

#include "Base/CozyInterface.h"
#include <memory>

namespace zl
{
    namespace http
    {
        class ZLCurlWarpper;
    }
}

class CDocument;

NS_BEGIN

class ZhihuEnvInit : public ISeekerEnvInit
{
public:
    typedef std::shared_ptr<class zl::http::ZLCurlWarpper> ClientPtr;
public:
    virtual bool Init();

private:
    ClientPtr _MakeClient();
    std::string _GetXSRF(CDocument& doc);
    std::string _GetCaptcha(CDocument& doc);

    bool _GetCookie(std::string& str);
    bool _SetCookie(const std::string& str);
    bool _IsLogin();
    bool _Login();
};

NS_END

#endif // __COZY_COMPONET_ZHIHU_ENV_INIT__
