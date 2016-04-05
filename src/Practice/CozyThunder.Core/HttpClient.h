
#include "HttpDef.h"
    class HttpClient
    {
    public:
        HttpClient();
        ~HttpClient();

    public:
        HttpStatusCode Get(const std::string& strUrl, const IBuffer* input, IBuffer* output);
        HttpStatusCode Post(const std::string& strUrl, const IBuffer* input, IBuffer* output);

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

        HttpStatusCode GetLastStatusCode() const;

    private:
        std::string __CreateHeader(const HttpHeader& header);
        std::size_t __WriteCallback(void* pvData, std::size_t nSize, std::size_t nCount, void* pvParam);
        HttpStatusCode __SendRequest(const std::string& strUrl, const IBuffer* input, IBuffer* output, bool isGetMethod);

    private:
        HttpHeaderList  m_HeaderList;
        HttpCookie      m_cookie;
        HttpStatusCode  m_lastStatus;
        HttpLimitType   m_nSpeedLimit;
        HttpLimitType   m_nTimeLimit;
        bool            m_bEnableSSL;
        bool            m_bEnableCookie;
        bool            m_bEnableAutoRedirect;
    };

