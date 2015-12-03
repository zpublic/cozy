#ifndef _H_Z_HTTP_CLIENT_H_
#define _H_Z_HTTP_CLIENT_H_

#include "z_http_interface.h"
#include "z_curl_warpper.h"
#include "z_mem_writer.h"

NS_BEGIN

class ZLHttpClient
{
public:
    ZLHttpClient();
    ~ZLHttpClient();

    zl_uint32 DownLoad(
        const std::string&  strUrl,
        IHttpWriter*        pWirter,
        zl_uint32           nTimeLimit  /* = 0*/,
        IHttpProgress*      pProgress   /* = NULL*/);

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

    zl_uint32 HttpGet();
    zl_uint32 HttpPost();
};

NS_END

#endif // _H_Z_HTTP_CLIENT_H_