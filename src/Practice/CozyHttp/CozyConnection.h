#ifndef __COZY_CONNECTION__
#define __COZY_CONNECTION__

#include "uv.h"

class CozyConnection
{
    friend class CozyHttpServer;
public:
    CozyConnection(uv_tcp_t* server);
    ~CozyConnection();

    void Set(const char* data, ssize_t len);
    ssize_t Read(char* data);

private:
    void buf_clear();

    uv_tcp_t*   m_server;
    uv_buf_t    m_buff;
};

#endif // __COZY_CONNECTION__