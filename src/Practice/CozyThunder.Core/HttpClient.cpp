#include "stdafx.h"
#include "HttpDef.h"
#include "HttpClient.h"
#include <iterator>
#include <vector>
#include "curl/curl.h"

using namespace Cozy;

HttpClient::HttpClient()
    :m_nSpeedLimit(InvalidLimit),
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

std::string HttpClient::__CreateHeader(const HttpHeader& header)
{
    std::string res = header.first;
    res += ": ";
    res += header.second;
    return res;
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
	HttpStatusCode statusCode = InvalidStatus;

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

    nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_WRITEDATA, static_cast<void*>(output));
    if (nRetCode != CURLE_OK) goto Exit0;

    if (m_nSpeedLimit)
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_MAX_RECV_SPEED_LARGE, static_cast<curl_off_t>(m_nSpeedLimit));
    if (nRetCode != CURLE_OK) goto Exit0;

    if (m_bEnableSSL)
    {
        nRetCode = ::curl_easy_setopt(pCurl, CURLOPT_SSL_VERIFYPEER, TRUE);
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
    ::curl_easy_getinfo(pCurl, CURLINFO_RESPONSE_CODE, &statusCode);

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

    return statusCode;
}

HttpStatusCode HttpClient::DownloadFile(const std::string& strUrl, IBuffer* output)
{
    return __SendRequest(strUrl, nullptr, output, true);
}

std::size_t HttpClient::GetFileSize(const std::string& strUrl)
{
    CURL *handle = curl_easy_init();

    ::curl_easy_setopt(handle, CURLOPT_URL, strUrl.c_str());

    ::curl_easy_setopt(handle, CURLOPT_HEADER, 1);

    ::curl_easy_setopt(handle, CURLOPT_NOBODY, 1);

    ::curl_easy_setopt(handle, CURLOPT_SSL_VERIFYPEER, TRUE);

    if (curl_easy_perform(handle) == CURLE_OK) 
    {
        double len = 0.0;
        if (CURLE_OK == ::curl_easy_getinfo(handle, CURLINFO_CONTENT_LENGTH_DOWNLOAD, &len))
        {
            return static_cast<std::size_t>(len + 0.5);
        }
    }

    return 0;
}