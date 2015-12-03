#ifndef _H_Z_CURL_WARPPER_H_
#define _H_Z_CURL_WARPPER_H_

#include "z_http_def.h"
#include "z_http_interface.h"
#include "curl/curl.h"
#include <vector>

struct curl_slist;

NS_BEGIN

class ZLCurlWarpper
{
public:
    ZLCurlWarpper();
    ~ZLCurlWarpper();

public:
    zl_int32 GetTimeLimit() const;
    HttpMethod GetMethod() const;
    zl_int32 GetStatusCode() const;
    std::string GetConnectAddr() const;

    void SetSpeedLimit(zl_int32 nSpeedLimit);
    zl_int32 GetSpeedLimit() const;
    void SetTimeLimit(zl_int32 nTimeSec);
    void SetMethod(HttpMethod eMethod);
    bool SetPostData(zl_uchar* pBuffer, zl_uint32 nSize);
    bool SetWriteCallback(IHttpWriter* pWriterCallback);
    bool SetProgressCallback(IHttpProgress* pProgressCallback);
    void SetEnableSSL(bool bIsEnable);

    void AppendHeaderList(const std::string& strHeader);
    bool Perform(const std::string& strUrl);

protected:
    bool _SetMethodInfo(CURL* pCurl);
    curl_slist* _GetHeaderList(CURL* pCurl);
    void _ClearHeaderList(curl_slist* pHeader);

    static size_t WriteCallback(void* pvData, size_t nSize, size_t nCount, void* pvParam);
    static size_t ProgressCallBack(void *userdata, double dltotal, double dlnow, double ultotal, double ulnow);

private:
    std::vector<std::string>    m_headerList;
    std::string                 m_strConnectAddr;
    zl_uchar*                   m_pPostData;
    IHttpWriter*                m_pWriter;
    IHttpProgress*              m_pProgress;
    zl_int32                    m_nTimeLimit;
    zl_int32                    m_nSpeedLimit;
    zl_int32                    m_nStatusCode;
    zl_uint32                   m_nPostDataLen;
    HttpMethod                  m_eHttpMethod;
    bool                        m_bEnableSSL;
};

NS_END


#endif // _H_Z_CURL_WARPPER_H_
