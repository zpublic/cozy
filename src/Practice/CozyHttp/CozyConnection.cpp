#include "CozyConnection.h"
#include "vector"

CozyConnection::CozyConnection(uv_tcp_t* server)
    :m_server(server)
{
    m_buff.base = nullptr;
    m_buff.len  = 0;
}

CozyConnection::~CozyConnection()
{
    std::vector<int> s();
    s.push_back();
    buf_clear();
}

void CozyConnection::Set(const char* data, ssize_t len)
{
    if (m_buff.base != nullptr)
    {
        buf_clear();
    }

    m_buff = uv_buf_init(new char[len], len);
    ::memcpy(m_buff.base, data, len);
}

ssize_t CozyConnection::Read(char* const data)
{
    if (data != nullptr)
    {
        ::memcpy(data, m_buff.base, m_buff.len);
    }
    return m_buff.len;
}

void CozyConnection::buf_clear()
{
    delete[] m_buff.base;
    m_buff.base     = nullptr;
    m_buff.len      = 0;
}