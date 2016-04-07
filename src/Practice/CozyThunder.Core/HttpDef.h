#ifndef __COZY_HTTP_DEF__
#define __COZY_HTTP_DEF__

#include <vector>

namespace Cozy
{
    typedef unsigned char                           byte_t;
    typedef std::pair<std::string, std::string>     HttpHeader;
    typedef std::vector<HttpHeader>                 HttpHeaderList;
    typedef std::string                             HttpCookie;
    typedef int                                     HttpStatusCode;
    typedef unsigned                                HttpLimitType;

    static const int                                InvalidStatus;
    static const int                                InvalidLimit;

    class IBuffer
    {
    public:
        virtual std::size_t GetSize() const = 0;
        virtual const byte_t* GetData() const = 0;
        virtual std::size_t Write(byte_t* data, std::size_t size) = 0;
        virtual void Clear() = 0;
    };
}

#endif // __COZY_HTTP_DEF__