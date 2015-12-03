#include "z_curl_warpper.h"

NS_BEGIN

ZLCurlWarpper::ZLCurlWarpper()
{
    m_nTimeLimit    = 0;
    m_nStatusCode   = 0;
    m_nPostDataLen  = 0;
    m_pPostData     = NULL;
    m_pWriter       = NULL;
    m_pProgress     = NULL;
    m_bEnableSSL    = false;
    m_eHttpMethod   = HttpMethod::GetMethod;
    m_nSpeedLimit   = 0;
}

ZLCurlWarpper::~ZLCurlWarpper()
{

}

void ZLCurlWarpper::SetTimeLimit(zl_int32 nTimeSec)
{
    m_nTimeLimit = nTimeSec;
}

zl_int32 ZLCurlWarpper::GetTimeLimit() const
{
    return m_nTimeLimit;
}

void ZLCurlWarpper::SetMethod(HttpMethod eMethod)
{
    m_eHttpMethod = eMethod;
}

HttpMethod ZLCurlWarpper::GetMethod() const
{
    return m_eHttpMethod;
}

zl_int32 ZLCurlWarpper::GetStatusCode() const
{
    return m_nStatusCode;
}

void ZLCurlWarpper::AppendHeaderList(const std::string& strHeader)
{
    if (strHeader.size() > 0)
    {
        m_headerList.push_back(strHeader);
    }
}

bool ZLCurlWarpper::SetPostData(zl_uchar* pBuffer, zl_uint32 nSize)
{
    m_pPostData     = pBuffer;
    m_nPostDataLen  = nSize;
    return true;
}

bool ZLCurlWarpper::SetWriteCallback(IHttpWriter* pWriterCallback)
{
    m_pWriter = pWriterCallback;
    return false;
}

bool ZLCurlWarpper::SetProgressCallback(IHttpProgress* pProgressCallback)
{
    m_pProgress = pProgressCallback;
    return true;
}

void ZLCurlWarpper::SetEnableSSL(bool bIsEnable)
{
    m_bEnableSSL = bIsEnable;
}

bool ZLCurlWarpper::Perform(const std::string& strUrl)
{
    bool bRet               = false;
    char* strIp             = NULL;
    curl_slist* pHeaderList = NULL;
    CURLcode nRetCode;

    CURL* pCurl = ::curl_easy_init();
    nRetCode    = ::curl_easy_setopt(pCurl, CURLOPT_URL, strUrl.c_str());
    if (nRetCode != CURLcode::CURLE_OK)
    {
        goto Exit0;
    }

    if (!_SetMethodInfo(pCurl))
    {
        goto Exit0;
    }

    pHeaderList = _GetHeaderList(pCurl);
    if (pHeaderList)
    {
        ::curl_easy_setopt(pCurl, CURLOPT_HTTPHEADER, pHeaderList);
    }

    nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_WRITEFUNCTION, ZLCurlWarpper::WriteCallback);
    if (nRetCode != CURLE_OK) goto Exit0;

    nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_WRITEDATA, static_cast<void*>(m_pWriter));
    if (nRetCode != CURLE_OK) goto Exit0;

    if (m_pProgress != NULL)
    {
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_NOPROGRESS, false);
        if (nRetCode != CURLE_OK) goto Exit0;

        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_PROGRESSFUNCTION, ZLCurlWarpper::ProgressCallBack);
        if (nRetCode != CURLE_OK) goto Exit0;

        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_PROGRESSDATA, static_cast<void*>(m_pProgress));
        if (nRetCode != CURLE_OK) goto Exit0;
    }

    if (m_nSpeedLimit)
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_MAX_RECV_SPEED_LARGE, static_cast<curl_off_t>(m_nSpeedLimit));
    if (nRetCode != CURLE_OK) goto Exit0;

    if (m_bEnableSSL)
    {
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_SSL_VERIFYPEER, FALSE);
        if (nRetCode != CURLE_OK) goto Exit0;
    }

    if (m_nTimeLimit)
    {
        ::curl_easy_setopt(pCurl, CURLOPT_CONNECTTIMEOUT, static_cast<curl_off_t>(m_nTimeLimit));
        ::curl_easy_setopt(pCurl, CURLOPT_TIMEOUT, static_cast<curl_off_t>(m_nTimeLimit));
    }

    nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_FOLLOWLOCATION, 1);
    if (nRetCode != CURLE_OK) goto Exit0;

    nRetCode = ::curl_easy_perform(pCurl);

    ::curl_easy_getinfo(pCurl, CURLINFO_PRIMARY_IP, &strIp);
    if (strIp)
    {
        m_strConnectAddr = std::string(strIp);
    }

    if (nRetCode != CURLE_OK)
    {
        m_nStatusCode = nRetCode;
        goto Exit0;
    }

    ::curl_easy_getinfo(pCurl, CURLINFO_RESPONSE_CODE, &m_nStatusCode);
    if (m_nStatusCode == 200) bRet = true;

Exit0:
    if (pCurl)
    {
        ::curl_easy_cleanup(pCurl);
    }

    _ClearHeaderList(pHeaderList);

    return bRet;
}

bool ZLCurlWarpper::_SetMethodInfo(CURL* pCurl)
{
    CURLcode nRetCode = CURLcode::CURLE_OK;

    if (m_eHttpMethod == HttpMethod::PostMethod)
    {
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_POST, true);
        if (nRetCode != CURLcode::CURLE_OK) goto Exit0;

        if (m_pPostData)
        {
            nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_POSTFIELDS, m_pPostData);
            if (nRetCode != CURLcode::CURLE_OK) goto Exit0;

            nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_POSTFIELDSIZE, m_nPostDataLen);
            if (nRetCode != CURLcode::CURLE_OK) goto Exit0;
        }
    }

Exit0:
    return nRetCode == CURLcode::CURLE_OK;
}

curl_slist* ZLCurlWarpper::_GetHeaderList(CURL* pCurl)
{
    curl_slist *headers = NULL;
    for (std::vector<std::string>::const_iterator citer = m_headerList.cbegin(); citer != m_headerList.cend(); ++citer)
    {
        headers = ::curl_slist_append(headers, citer->c_str());
    }
    return headers;
}

size_t ZLCurlWarpper::WriteCallback(void* pvData, size_t nSize, size_t nCount, void* pvParam)
{
    IHttpWriter* pWrite = static_cast<IHttpWriter*>(pvParam);
    size_t nDataSize    = nSize * nCount;
    if (pWrite)
    {
        pWrite->Write(static_cast<zl_uchar*>(pvData), nDataSize);
    }
    return nDataSize;
}

size_t ZLCurlWarpper::ProgressCallBack(void *pvData, double dltotal, double dlnow, double ultotal, double ulnow)
{
    size_t nReturn              = 0;
    IHttpProgress *pProgress    = static_cast<IHttpProgress*>(pvData);
    if (pProgress)
        nReturn = pProgress->OnProgress(dltotal, dlnow, ultotal, ulnow);
    return nReturn;
}

void ZLCurlWarpper::_ClearHeaderList(curl_slist* pHeader)
{
    m_headerList.clear();
    if (pHeader)
    {
        ::curl_slist_free_all(pHeader);
    }
}

zl_int32 ZLCurlWarpper::GetSpeedLimit() const
{
    return m_nSpeedLimit;
}

void ZLCurlWarpper::SetSpeedLimit(zl_int32 nSpeedLimit)
{
    m_nSpeedLimit = nSpeedLimit;
}
std::string ZLCurlWarpper::GetConnectAddr() const
{
    return m_strConnectAddr;
}

NS_END
