#include "CozyHttpServer.h"
#include "CozyConnection.h"

void CozyHttpServer::alloc_buffer(uv_handle_t* handle, size_t suggested_size, uv_buf_t* buf)
{
    *buf = :: uv_buf_init(new char[suggested_size], suggested_size);
}

CozyHttpServer::CozyHttpServer()
{
    m_loop = ::uv_loop_new();
}

CozyHttpServer::~CozyHttpServer()
{
    ::uv_loop_delete(m_loop);
}

void CozyHttpServer::Init(const std::string& ip, int port, int maxConn)
{
    m_maxConn = maxConn;

    ::uv_tcp_init(m_loop, &m_server);
    m_server.data = this;

    ::uv_ip4_addr(ip.c_str(), port, &m_addr);
    ::uv_tcp_bind(&m_server, reinterpret_cast<sockaddr*>(&m_addr), 0);
}

void CozyHttpServer::Start()
{
    ::uv_listen(reinterpret_cast<uv_stream_t*>(&m_server), m_maxConn, _OnConnect);
    ::uv_run(m_loop, UV_RUN_DEFAULT);
}

void CozyHttpServer::Stop()
{
    ::uv_loop_close(m_loop);
}

void CozyHttpServer::_OnConnect(uv_stream_t* server, int status)
{
    CozyHttpServer* server_ptr  = reinterpret_cast<CozyHttpServer*>(server->data);

    uv_tcp_t* client            = new uv_tcp_t();
    ::uv_tcp_init(server_ptr->m_loop, client);

    CozyConnection* conn    = new CozyConnection(reinterpret_cast<uv_tcp_t*>(server));
    client->data            = conn;

    if (!::uv_accept(server, reinterpret_cast<uv_stream_t*>(client)))
    {
        ::uv_read_start(reinterpret_cast<uv_stream_t*>(client), alloc_buffer, _OnRead);
    }
    else
    {
        ::uv_close(reinterpret_cast<uv_handle_t*>(client), _OnClose);
    }
}

void CozyHttpServer::_OnRead(uv_stream_t* client, ssize_t nread, const uv_buf_t* buf)
{
    if (nread > 0)
    {
        CozyConnection* conn = reinterpret_cast<CozyConnection*>(client->data);

        conn->Set(buf->base, nread); // TEST

        if (conn->m_buff.len > 0)
        {
            uv_write_t *wreq = new uv_write_t();
            wreq->data = conn;
            ::uv_write(wreq, client, &conn->m_buff, 1, _OnWrite);
        }
    }

    ::delete[](buf->base);
}

void CozyHttpServer::_OnWrite(uv_write_t* req, int status)
{
    ::uv_close(reinterpret_cast<uv_handle_t*>(req->handle), _OnClose);
    delete req;
}

void CozyHttpServer::_OnClose(uv_handle_t* handle)
{
    delete reinterpret_cast<CozyConnection*>(handle->data);
    delete handle;
}