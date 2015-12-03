#ifndef _H_Z_HTTP_CLIENT_H_
#define _H_Z_HTTP_CLIENT_H_

#include "z_http_interface.h"
#include <vector>

NS_BEGIN

class ZLMemWriter;
class IHttpProgress;
class ZLCurlWarpper;

class ZLHttpClient
{
public:
    typedef std::pair<std::string, std::string> HttpHeader;

    ZLHttpClient();
    ~ZLHttpClient();

        zl_uint32 DownloadFile(
        const std::string&  strUrl,
        const std::string&  strFilename, 
        zl_uint32           nTimeLimit  /* = 0*/,
        IHttpProgress*      pProgress   /* = NULL*/);

    zl_uint32 DownloadMem(
        const std::string&  strUrl,
        ZLMemWriter*        pMem,
        zl_uint32           nTimeLimit  /* = 0*/,
        IHttpProgress*      pProgress   /* = NULL*/);

    zl_uint32 DownLoad(
        const std::string&  strUrl,
        IHttpWriter*        pWirter,
        zl_uint32           nTimeLimit  /* = 0*/,
        IHttpProgress*      pProgress   /* = NULL*/);

    zl_uint32 HttpGet(
        const std::string&  strUrl,
        IHttpWriter*        pWriter,
        zl_int32            nTimeLimit  /* = 0*/,
        IHttpProgress*      pProgress   /* = NULL*/);

    zl_uint32 HttpGet(
        const std::string&  strUrl,
        IHttpWriter*        pWriter,
        const std::vector<HttpHeader> vecHeaders,
        zl_int32            nTimeLimit  /* = 0*/,
        IHttpProgress*      pProgress   /* = NULL*/);

    zl_uint32 HttpPost(
        const std::string&  strUrl,
        zl_uchar*           pData,
        zl_uint32           nLength,
        IHttpWriter*        pWriter,
        zl_int32            nTimeLimit  /*= 0*/,
        IHttpProgress*      pProgress   /*= NULL*/);

    zl_uint32 HttpPost(
        const std::string&  strUrl,
        zl_uchar*           pData,
        zl_uint32           nLength,
        IHttpWriter*        pWriter,
        const std::vector<HttpHeader>& vecHeaders,
        zl_int32            nTimeLimit  /*= 0*/,
        IHttpProgress*      pProgress   /*= NULL*/);

protected:
    static std::string FormatHeader(const std::string& key, const std::string& value);

    zl_uint32 _HttpGet(
        ZLCurlWarpper*      curl,
        const std::string&  strUrl,
        IHttpWriter*        pWriter,
        zl_int32            nTimeLimit,
        IHttpProgress*      pProgress);

    zl_uint32 _HttpPost(
        ZLCurlWarpper*      curl,
        const std::string&  strUrl,
        zl_uchar*           pData,
        zl_uint32           nLength,
        IHttpWriter*        pWriter,
        zl_int32            nTimeLimit,
        IHttpProgress*      pProgress);
};

NS_END

#endif // _H_Z_HTTP_CLIENT_H_