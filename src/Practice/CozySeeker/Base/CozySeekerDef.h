#ifndef __COZY_SEEKER_DEF__
#define __COZY_SEEKER_DEF__

#define NS_BEGIN namespace Cozy {

#define NS_END }

#include <string>
#include <memory>

NS_BEGIN

typedef std::shared_ptr<class IUrlIn> IUrlInPtr;
typedef std::shared_ptr<class IUrlGenerater> IUrlGeneraterPtr;
typedef std::shared_ptr<class IUrl2Result> IUrl2ResultPtr;
typedef std::shared_ptr<class IUrl2Url> IUrl2UrlPtr;
typedef std::shared_ptr<class ISeekerTester> ISeekerTesterPtr;
typedef std::shared_ptr<std::string> StrPtr;

NS_END

#endif // __COZY_SEEKER_DEF__
