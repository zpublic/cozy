#include "CozyHttpServer.h"
#include "CozyConnection.h"
#include "CozyHttpRequest.h"
#include "CozyHttpResponse.h"

const char* rspData =
"HTTP/1.1 200 OK\n"
"Content-Length: 10\n\n"
"Hello Tcp!";

void CozyHttpServer::alloc_buffer(uv_handle_t* handle, size_t suggested_size, uv_buf_t* buf)
{
    *buf = :: uv_buf_init(new char[suggested_size], suggested_size);
}

CozyHttpServer::CozyHttpServer()
    :m_work_cb(nullptr)
{
    m_loop = ::uv_loop_new();
}

CozyHttpServer::~CozyHttpServer()
{
    delete m_parser;
    ::uv_loop_delete(m_loop);
}

void CozyHttpServer::Init(const std::string& ip, int port, int maxConn)
{
    m_maxConn = maxConn;
    InitParser();

    ::uv_tcp_init(m_loop, &m_server);
    m_server.data = this;

    ::uv_ip4_addr(ip.c_str(), port, &m_addr);
    ::uv_tcp_bind(&m_server, reinterpret_cast<sockaddr*>(&m_addr), 0);
}

void CozyHttpServer::InitParser()
{
    memset(&m_settings, 0, sizeof(m_settings));
    m_settings.on_header_field      = OnHeaderFiled;
    m_settings.on_header_value      = OnHeaderValue;
    m_settings.on_url               = OnUrl;
    m_settings.on_message_complete  = OnComplete;
    m_settings.on_body              = OnBody;

    m_parser = new http_parser();
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

    CozyConnection* conn        = new CozyConnection(reinterpret_cast<uv_tcp_t*>(server));
    conn->m_instance            = server_ptr;
    client->data                = conn;

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
        CozyHttpRequest request;
        CozyHttpResponse response;
        CozyConnection* conn        = reinterpret_cast<CozyConnection*>(client->data);
        
        http_parser* parser         = conn->m_instance->m_parser;
        http_parser_init(parser, HTTP_REQUEST);
        parser->data                = &request;

        ssize_t nparsed             = http_parser_execute(parser, &conn->m_instance->m_settings, buf->base, buf->len);
        
        if (nparsed != nread)
        {
            
            ::uv_close(reinterpret_cast<uv_handle_t*>(client), _OnClose);
            goto Exit0;
        }

        if (conn->m_instance->m_work_cb != nullptr)
        {
            conn->m_instance->m_work_cb(request, response);
        }

        std::string output;
        response.GetResponseData(output);
        conn->Set(output.c_str(), output.size());

        if (conn->m_buff.base != nullptr && conn->m_buff.len > 0)
        {
            uv_write_t *wreq    = new uv_write_t();
            wreq->data          = conn;
            ::uv_write(wreq, client, &conn->m_buff, 1, _OnWrite);
        }
    }

Exit0:
    ::delete[] buf->base;
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

void CozyHttpServer::SetCallback(work_cb cb)
{
    m_work_cb = cb;
}

int CozyHttpServer::OnHeaderFiled(http_parser* parser, const char* at, size_t length)
{
    CozyHttpRequest* req = reinterpret_cast<CozyHttpRequest*>(parser->data);

    req->AddFiled(std::string(at, at + length));
    return 0;
}

int CozyHttpServer::OnHeaderValue(http_parser* parser, const char* at, size_t length)
{
    CozyHttpRequest* req = reinterpret_cast<CozyHttpRequest*>(parser->data);

    req->SetValue(req->GetLastFiled(), std::string(at, at + length));
    return 0;
}

int CozyHttpServer::OnUrl(http_parser* parser, const char* at, size_t length)
{
    CozyHttpRequest* req = reinterpret_cast<CozyHttpRequest*>(parser->data);

    req->SetUrl(std::string(at, at + length));
    return 0;
}

int CozyHttpServer::OnComplete(http_parser* parser)
{
    CozyHttpRequest* req = reinterpret_cast<CozyHttpRequest*>(parser->data);
    
    req->SetHttpMethod(parser->method);
    req->SetHttpVersion(parser->http_major, parser->http_minor);
    return 0;
}

int CozyHttpServer::OnHeaderComplete(http_parser* parser)
{
    CozyHttpRequest* req = reinterpret_cast<CozyHttpRequest*>(parser->data);

    req->SetContextLength(parser->content_length);
    return 0;
}

int CozyHttpServer::OnBody(http_parser* parser, const char* at, size_t length)
{
    CozyHttpRequest* req = reinterpret_cast<CozyHttpRequest*>(parser->data);

    req->SetBody(std::string(at, at + length));
    return 0;
}

