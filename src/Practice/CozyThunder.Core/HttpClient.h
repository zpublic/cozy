#ifndef __COZY_HTTP_CLIENT__
#define __COZY_HTTP_CLIENT__

#include "HttpDef.h"

namespace Cozy
{
    class HttpClient
    {
    public:
        HttpClient();
        ~HttpClient();

    public:
        HttpStatusCode Get(const std::string& strUrl, const IBuffer* input, IBuffer* output);
        HttpStatusCode Post(const std::string& strUrl, const IBuffer* input, IBuffer* output);

        HttpStatusCode DownloadFile(const std::string& strUrl, IBuffer* output);
        std::size_t GetFileSize(const std::string& strUrl);

    public:
        void SetEnableSSL(bool bIsEnableSSL);
        bool GetEnableSSL() const;

        void SetEnableCookie(bool bIsEnableCookie);
        bool GetEnableCookie() const;
        HttpCookie GetCookie() const;

        void SetEnableAutoRedirect(bool bIsEnableAutoRedirect);
        bool GetEnableAutoRedirect() const;

        void SetTimeLimit(HttpLimitType nTimeLimit);
        HttpLimitType GetTimeLimit() const;

        void SetSpeedLimit(HttpLimitType nSpeedLimit);
        HttpLimitType GetSpeedLimit() const;

        void AppendHttpHeader(const HttpHeader& header);

    private:
        std::string __CreateHeader(const HttpHeader& header);
        HttpStatusCode __SendRequest(const std::string& strUrl, const IBuffer* input, IBuffer* output, bool isGetMethod);
        
        static std::size_t __WriteCallback(void* pvData, std::size_t nSize, std::size_t nCount, void* pvParam);

    private:
        HttpHeaderList  m_HeaderList;
        HttpCookie      m_cookie;
        HttpLimitType   m_nSpeedLimit;
        HttpLimitType   m_nTimeLimit;
        bool            m_bEnableSSL;
        bool            m_bEnableCookie;
        bool            m_bEnableAutoRedirect;
    };
}

#endif // __COZY_HTTP_CLIENT__