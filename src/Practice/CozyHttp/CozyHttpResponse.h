#ifndef __COZY_HTTP_RESPONSE__
#define __COZY_HTTP_RESPONSE__

#include <map>
#include <unordered_map>
#include <sstream>

enum class Status_Code : unsigned int
{
    OK                      = 200,
    Bad_Request             = 400,
    Unauthorized            = 401,
    Forbidden               = 403,
    Not_Found               = 404,
    Internal_Server_Error   = 500,
    Server_Unavailable      = 503,
};

extern std::map<unsigned int, std::string> ReasonDict;

class CozyHttpResponse
{
public:
    CozyHttpResponse();
    ~CozyHttpResponse();

    CozyHttpResponse(const CozyHttpResponse& o)             = default;
    CozyHttpResponse& operator=(const CozyHttpResponse& o)  = default;

    void AddHeader(const std::string& filed, const std::string& value);

    void SetReasonPhrase(const std::string& reason);
    void SetStatusCode(unsigned int code);
    void SetStatusCode(Status_Code code);

    void SetHttpVersion(short major, short minor);
    void SetContext(const std::string& body);

    std::string GetReasonPhrase() const;
    std::string GetContext() const;
    unsigned int GetStatusCode() const;
    void GetHttpVersion(short* major, short* minor) const;

    void GetResponseData(std::string& output);

private:
    std::unordered_map<std::string, std::string>    m_http_hander;
    std::stringstream                               m_response_data;
    std::string                                     m_reason_phrase;
    std::string                                     m_context;
    unsigned int                                    m_status_code;
    unsigned int                                    m_http_version;
    bool                                            m_is_data_dirty;

};

#endif // __COZY_HTTP_RESPONSE__