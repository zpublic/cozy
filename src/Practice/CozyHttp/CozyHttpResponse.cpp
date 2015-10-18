#include "CozyHttpResponse.h"

CozyHttpResponse::CozyHttpResponse()
{
    m_status_code   = 0;
    m_http_version  = 0;
    m_is_data_dirty = true;
}

CozyHttpResponse::~CozyHttpResponse()
{

}

void CozyHttpResponse::AddHeader(const std::string& filed, const std::string& value)
{
    m_http_hander[filed]    = value;
    m_is_data_dirty         = true;
}

void CozyHttpResponse::SetReasonPhrase(const std::string& reason)
{
    m_reason_phrase = reason;
    m_is_data_dirty = true;
}

void CozyHttpResponse::SetStatusCode(unsigned int code)
{
    m_status_code   = code;
    m_is_data_dirty = true;
}

void CozyHttpResponse::SetHttpVersion(short major, short minor)
{
    m_http_version  = ((major << 16) | minor);
    m_is_data_dirty = true;
}

void CozyHttpResponse::SetContext(const std::string& body)
{
    m_context = body;

    char c[256];
    std::sprintf(c, "%u", m_context.size());

    m_http_hander["Content-Length"] = std::string(c);
    m_is_data_dirty = true;
}

std::string CozyHttpResponse::GetContext() const
{
    return m_context;
}

std::string CozyHttpResponse::GetReasonPhrase() const
{
    return m_reason_phrase;
}

unsigned int CozyHttpResponse::GetStatusCode() const
{
    return m_status_code;
}

void CozyHttpResponse::GetHttpVersion(short* major, short* minor) const
{
    *major = (m_http_version >> 16);
    *minor = (m_http_version & 0x0000FFFF);
}

void CozyHttpResponse::GetResponseData(std::string& output)
{
    if (m_is_data_dirty)
    {
        m_response_data.clear();

        // HTTP-Version
        m_response_data << "HTTP/";
        short major = 0;
        short minor = 0;
        GetHttpVersion(&major, &minor);
        m_response_data << major << '.' << minor << ' ';

        // Status-Code
        m_response_data << m_status_code << ' ';

        // Reason-Phrase
        m_response_data << m_reason_phrase << std::endl;

        // Header
        for (auto& filed : m_http_hander)
        {
            m_response_data << filed.first << ':' << filed.second << std::endl;
        }

        m_response_data << std::endl;

        // Context
        m_response_data << m_context;

        m_is_data_dirty = false;
    }

    output.clear();
    output = m_response_data.str(); 
}