#ifndef __COZY_HTTP_REQUEST__
#define __COZY_HTTP_REQUEST__

#include <unordered_map>

class CozyHttpRequest
{
public:
    CozyHttpRequest();
    ~CozyHttpRequest();

    CozyHttpRequest(const CozyHttpRequest& o)               = default;
    CozyHttpRequest& operator=(const CozyHttpRequest& o)    = default;


    bool AddFiled(const std::string& filed);

    void SetValue(const std::string& filed, const std::string& value);
    void SetUrl(const std::string& url);
    void SetHttpMethod(unsigned int method);
    void SetHttpVersion(short major, short minor);
    void SetContextLength(unsigned long long len);
    void SetBody(const std::string& body);

    bool GetFiled(const std::string& filed, std::string& output) const;
    std::string GetLastFiled() const;
    std::string GetUrl() const;
    std::string GetBody() const;
    void GetHttpVersion(short* major, short* minor) const;
    unsigned long long GetContextLength() const;

private:
    std::unordered_map<std::string, std::string>    m_http_header;
    std::string                                     m_last_filed;

    std::string                                     m_url;
    std::string                                     m_body;
    unsigned int                                    m_status;
    unsigned int                                    m_http_version;
    unsigned long long                              m_context_length;
};

#endif // __COZY_HTTP_REQUEST__