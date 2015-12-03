#include "z_http_client.h"
#include "z_file_writer.h"
#include "z_mem_writer.h"
#include "z_curl_warpper.h"

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
    ZLCurlWarpper curlObj;
    curlObj.SetWriteCallback(pWirter);
    curlObj.SetProgressCallback(pProgress);

    if (nTimeLimit)
    {
        curlObj.SetTimeLimit(nTimeLimit);
    }

    if (!curlObj.Perform(strUrl))
    {
        return curlObj.GetStatusCode();
    }
    return 0;
}

zl_uint32 ZLHttpClient::DownloadFile(
    const std::string&  strUrl,
    const std::string&  strFilename,
    zl_uint32           nTimeLimit  = 0,
    IHttpProgress*      pProgress   = NULL)
{
    ZLFileWriter writer(strFilename);
    return DownLoad(strUrl, &writer, nTimeLimit, pProgress);
}

zl_uint32 ZLHttpClient::DownloadMem(
    const std::string&  strUrl,
    ZLMemWriter*        pMem,
    zl_uint32           nTimeLimit  = 0,
    IHttpProgress*      pProgress   = NULL)
{
    return DownLoad(strUrl, pMem, nTimeLimit, pProgress);
}

zl_uint32 ZLHttpClient::HttpGet(
    const std::string&  strUrl,
    IHttpWriter*        pWriter,
    zl_int32            nTimeLimit  = 0,
    IHttpProgress*      pProgress   = NULL)
{
    ZLCurlWarpper curl;
    return _HttpGet(curl, strUrl, pWriter, nTimeLimit, pProgress);
}

zl_uint32 ZLHttpClient::HttpGet(
    const std::string&  strUrl,
    IHttpWriter*        pWriter,
    const std::vector<HttpHeader> vecHeaders,
    zl_int32            nTimeLimit   = 0,
    IHttpProgress*      pProgress    = NULL)
{
    ZLCurlWarpper curl;
    for (std::vector<HttpHeader>::const_iterator citer = vecHeaders.cbegin(); citer != vecHeaders.cend(); ++citer)
    {
        curl.AppendHeaderList(FormatHeader(citer->first, citer->second));
    }
    return _HttpGet(curl, strUrl, pWriter, nTimeLimit, pProgress);
}

zl_uint32 ZLHttpClient::_HttpGet(
    ZLCurlWarpper&      curl,
    const std::string&  strUrl,
    IHttpWriter*        pWriter,
    zl_int32            nTimeLimit,
    IHttpProgress*      pProgress)
{
    curl.SetWriteCallback(pWriter);

    if (nTimeLimit)
    {
        curl.SetTimeLimit(nTimeLimit);
    }
    if (pProgress)
    {
        curl.SetProgressCallback(pProgress);
    }

    if (!curl.Perform(strUrl))
    {
        return curl.GetStatusCode();
    }
    return 0;
}

zl_uint32 ZLHttpClient::HttpPost(
    const std::string&  strUrl,
    zl_uchar*           pData,
    zl_uint32           nLength,
    IHttpWriter*        pWriter,
    zl_int32            nTimeLimit  = 0,
    IHttpProgress*      pProgress   = NULL)
{
    ZLCurlWarpper curl;
    return _HttpPost(curl, strUrl, pData, nLength, pWriter, nTimeLimit, pProgress);
}

zl_uint32 ZLHttpClient::HttpPost(
    const std::string&  strUrl,
    zl_uchar*           pData,
    zl_uint32           nLength,
    IHttpWriter*        pWriter,
    const std::vector<HttpHeader>& vecHeaders,
    zl_int32            nTimeLimit  = 0,
    IHttpProgress*      pProgress   = NULL)
{
    ZLCurlWarpper curl;
    for (std::vector<HttpHeader>::const_iterator citer = vecHeaders.cbegin(); citer != vecHeaders.cend(); ++citer)
    {
        curl.AppendHeaderList(FormatHeader(citer->first, citer->second));
    }
    return _HttpPost(curl, strUrl, pData, nLength, pWriter, nTimeLimit, pProgress);
}

zl_uint32 ZLHttpClient::_HttpPost(
    ZLCurlWarpper&      curl,
    const std::string&  strUrl,
    zl_uchar*           pData,
    zl_uint32           nLength,
    IHttpWriter*        pWriter,
    zl_int32            nTimeLimit,
    IHttpProgress*      pProgress)
{
    curl.SetMethod(HttpMethod::PostMethod);
    curl.SetWriteCallback(pWriter);

    curl.SetPostData(pData, nLength);

    if (nTimeLimit)
    {
        curl.SetTimeLimit(nTimeLimit);
    }
    if (pProgress)
    {
        curl.SetProgressCallback(pProgress);
    }

    if (!curl.Perform(strUrl))
    {
        return curl.GetStatusCode();
    }
    return 0;
}

std::string ZLHttpClient::FormatHeader(const std::string& key, const std::string& value)
{
    zl_uint32 nSize = key.size() + value.size() + 2 + 1;
    zl_char* pData  = new zl_char[nSize];
    std::memset(pData, 0, nSize);

    zl_uint32 nOffset = 0;

    std::memcpy(pData + nOffset, key.data(), key.size());
    nOffset += key.size();

    std::memcpy(pData + nOffset, ": ", 2);
    nOffset += 2;

    std::memcmp(pData + nOffset, value.data(), value.size());

    std::string strResult(pData);

    delete pData;
    return strResult;
}

NS_END