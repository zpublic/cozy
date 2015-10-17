#include "CozyHttpRequest.h"

CozyHttpRequest::CozyHttpRequest()
{
    m_status        = 0;
    m_http_version  = 0;
}

CozyHttpRequest::~CozyHttpRequest()
{

}

bool CozyHttpRequest::AddFiled(const std::string& filed)
{
    if (m_http_header.find(filed) != m_http_header.end()) return false;

    m_http_header[filed]    = "";
    m_last_filed            = filed;
    return true;
}

void CozyHttpRequest::SetValue(const std::string& filed, const std::string& value)
{
    m_http_header[filed] = value;
}

std::string CozyHttpRequest::GetLastFiled() const
{
    return m_last_filed;
}

bool CozyHttpRequest::GetFiled(const std::string& filed, std::string& output) const
{
    auto iter = m_http_header.find(filed);
    if (iter == m_http_header.end()) return false;

    output = iter->second;
    return true;
}

void CozyHttpRequest::SetUrl(const std::string& url)
{
    m_url = url;
}

std::string CozyHttpRequest::GetUrl() const
{
    return m_url;
}

void CozyHttpRequest::SetHttpMethod(unsigned int method)
{
    m_status = method;
}

void CozyHttpRequest::SetHttpVersion(short major, short minor)
{
    m_http_version = ((major << 16) | minor);
}

void CozyHttpRequest::GetHttpVersion(short* major, short* minor) const
{
    *major = (m_http_version >> 16);
    *minor = (m_http_version & 0x0000FFFF);
}

void CozyHttpRequest::SetContextLength(unsigned long long len)
{
    m_context_length = len;
}

unsigned long long CozyHttpRequest::GetContextLength() const
{
    return m_context_length;
}

std::string CozyHttpRequest::GetBody() const
{
    return m_body;
}

void CozyHttpRequest::SetBody(const std::string& body)
{
    m_body = body;
}