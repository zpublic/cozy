#include "stdafx.h"
#include "HttpClient.h"
#include <iterator>

#include "curl/curl.h"


HttpClient::HttpClient()
    :m_lastStatus(InvalidStatus),
    m_nSpeedLimit(InvalidLimit),
    m_nTimeLimit(InvalidLimit),
    m_bEnableSSL(false),
    m_bEnableCookie(false),
    m_bEnableAutoRedirect(true)
{

}

HttpClient::~HttpClient()
{

}

HttpStatusCode HttpClient::Get(const std::string& strUrl, const IBuffer* input, IBuffer* output)
{
    return __SendRequest(strUrl, input, output, true);
}

int HttpClient::Post(const std::string& strUrl, const IBuffer* input, IBuffer* output)
{
    return __SendRequest(strUrl, input, output, false);
}

void HttpClient::SetEnableSSL(bool bIsEnableSSL)
{
    m_bEnableSSL = bIsEnableSSL;
}

bool HttpClient::GetEnableSSL() const
{
    return m_bEnableSSL;
}

void HttpClient::SetEnableCookie(bool bIsEnableCookie)
{
    m_bEnableCookie = bIsEnableCookie;
}

bool HttpClient::GetEnableCookie() const
{
    return m_bEnableCookie;
}

HttpCookie HttpClient::GetCookie() const
{
    return m_cookie;
}

void HttpClient::SetEnableAutoRedirect(bool bIsEnableAutoRedirect)
{
    m_bEnableAutoRedirect = bIsEnableAutoRedirect;
}

bool HttpClient::GetEnableAutoRedirect() const
{
    return m_bEnableAutoRedirect;
}

void HttpClient::SetTimeLimit(HttpLimitType nTimeLimit)
{
    m_nTimeLimit = nTimeLimit;
}

HttpLimitType HttpClient::GetTimeLimit() const
{
    return m_nTimeLimit;
}

void HttpClient::SetSpeedLimit(HttpLimitType nSpeedLimit)
{
    m_nSpeedLimit = nSpeedLimit;
}

HttpLimitType HttpClient::GetSpeedLimit() const
{
    return m_nSpeedLimit;
}

void HttpClient::AppendHttpHeader(const HttpHeader& header)
{
    if (header.first.size() > 0 && header.second.size() > 0)
    {
        m_HeaderList.push_back(header);
    }
}

HttpStatusCode HttpClient::GetLastStatusCode() const
{
    return m_lastStatus;
}

std::string HttpClient::__CreateHeader(const HttpHeader& header)
{
    std::string buffer ('\0', header.first.size() + header.second.size() + 1);

    return "";
}

std::size_t HttpClient::__WriteCallback(void* pvData, std::size_t nSize, std::size_t nCount, void* pvParam)
{
    IBuffer* pWrite = static_cast<IBuffer*>(pvParam);
    size_t nDataSize = nSize * nCount;
    if (pWrite)
    {
        pWrite->Write(static_cast<byte_t*>(pvData), nDataSize);
    }
    return nDataSize;
}

HttpStatusCode HttpClient::__SendRequest(const std::string& strUrl, const IBuffer* input, IBuffer* output, bool isGetMethod)
{
    bool bRet = false;
    char* strIp = NULL;
    curl_slist* pHeaderList = NULL;
    CURLcode nRetCode;

    CURL* pCurl = ::curl_easy_init();
    nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_URL, strUrl.c_str());
    if (nRetCode != CURLcode::CURLE_OK)
    {
        goto Exit0;
    }

    if (!isGetMethod)
    {
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_POST, true);
        if (nRetCode != CURLcode::CURLE_OK) goto Exit0;

        if (input->GetSize() > 0)
        {
            nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_POSTFIELDS, input->GetData());
            if (nRetCode != CURLcode::CURLE_OK) goto Exit0;

            nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_POSTFIELDSIZE, input->GetSize());
            if (nRetCode != CURLcode::CURLE_OK) goto Exit0;
        }
    }

    curl_slist *headers = NULL;
    for (auto citer = m_HeaderList.cbegin(); citer != m_HeaderList.cend(); ++citer)
    {
        headers = ::curl_slist_append(headers, __CreateHeader(*citer).c_str());
    }
    if (headers)
    {
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_HTTPHEADER, headers);
        if (nRetCode != CURLE_OK) goto Exit0;
    }

    if (!m_bEnableAutoRedirect)
    {
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_FOLLOWLOCATION, false);
        ::curl_easy_setopt(pCurl, CURLOPT_MAXREDIRS, -1);
        if (nRetCode != CURLE_OK) goto Exit0;
    }
    else
    {
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_FOLLOWLOCATION, true);
        ::curl_easy_setopt(pCurl, CURLOPT_MAXREDIRS, 0);
        if (nRetCode != CURLE_OK) goto Exit0;
    }

    nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_WRITEFUNCTION, &HttpClient::__WriteCallback);
    if (nRetCode != CURLE_OK) goto Exit0;

    nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_WRITEDATA, static_cast<void*>(&output));
    if (nRetCode != CURLE_OK) goto Exit0;

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

    if (m_bEnableCookie)
    {
        if (m_cookie.size() > 0)
        {
            nRetCode = curl_easy_setopt(pCurl, CURLOPT_COOKIE, m_cookie.data());
        }
        else
        {
            nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_COOKIEFILE, "");
        }
        if (nRetCode != CURLE_OK) goto Exit0;
    }

    nRetCode = ::curl_easy_perform(pCurl);
    ::curl_easy_getinfo(pCurl, CURLINFO_RESPONSE_CODE, &m_lastStatus);

    if (m_bEnableCookie)
    {
        struct curl_slist *cookies = nullptr;
        struct curl_slist *nc = nullptr;

        auto res = ::curl_easy_getinfo(pCurl, CURLINFO_COOKIELIST, &cookies);
        nc = cookies;
        while (nc)
        {
            std::copy(nc->data, nc->data + std::strlen(nc->data), std::back_inserter(m_cookie));
            nc = nc->next;
        }
        ::curl_slist_free_all(cookies);
    }

Exit0:
    if (pCurl)
    {
        ::curl_easy_cleanup(pCurl);
    }

    m_HeaderList.clear();
    if (pHeaderList)
    {
        ::curl_slist_free_all(pHeaderList);
    }

    return InvalidStatus;
}