#include "z_http_client.h"

NS_BEGIN

ZLHttpClient::ZLHttpClient()
{

}

ZLHttpClient::~ZLHttpClient()
{

}

zl_uint32 ZLHttpClient::DownLoad(
    const std::string&  strUrl,
    IHttpWriter*        pWirter,
    zl_uint32           nTimeLimit  = 0,
    IHttpProgress*      pProgress   = NULL)
{
    return 0;
}

zl_uint32 ZLHttpClient::DownloadFile(
    const std::string&  strUrl,
    const std::string&  strFilename,
    zl_uint32           nTimeLimit  = 0,
    IHttpProgress*      pProgress   = NULL)
{
    return 0;
}

zl_uint32 ZLHttpClient::DownloadMem(
    const std::string&  strUrl,
    ZLMemWriter*        pMem,
    zl_uint32           nTimeLimit  = 0,
    IHttpProgress*      pProgress   = NULL)
{
    return 0;
}

zl_uint32 ZLHttpClient::HttpGet()
{
    return 0;
}

zl_uint32 ZLHttpClient::HttpPost()
{
    return 0;
}

NS_END