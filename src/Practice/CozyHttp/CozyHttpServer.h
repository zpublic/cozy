#ifndef __COZY_HTTP_SERVER__
#define __COZY_HTTP_SERVER__

#include "uv.h"
#include <string>
#include "http_parser.h"

class CozyConnection;
class CozyHttpRequest;
class CozyHttpResponse;

class CozyHttpServer
{
    typedef void(*work_cb)(const CozyHttpRequest& req, CozyHttpResponse& rsp);
public:

    CozyHttpServer();
    ~CozyHttpServer();

    void Init(const std::string& ip, int port, int maxConn);
    void Start();
    void Stop();

    void SetCallback(work_cb cb);
protected:
    void InitParser();
    static int OnHeaderFiled(http_parser* parser, const char* at, size_t length);
    static int OnHeaderValue(http_parser* parser, const char* at, size_t length);
    static int OnUrl(http_parser* parser, const char* at, size_t length);
    static int OnComplete(http_parser* parser);
    static int OnHeaderComplete(http_parser* parser);
    static int OnBody(http_parser* parser, const char* at, size_t length);

protected:
    static void _OnConnect(uv_stream_t* handle, int status);
    static void _OnRead(uv_stream_t * handle, ssize_t nread, const uv_buf_t* buf);
    static void _OnWrite(uv_write_t* req, int status);
    static void _OnClose(uv_handle_t* handle);

    static void alloc_buffer(uv_handle_t* handle, size_t suggested_size, uv_buf_t* buf);

private:
    uv_loop_t*              m_loop;
    uv_tcp_t                m_server;
    int                     m_maxConn;
    sockaddr_in             m_addr;
    work_cb                 m_work_cb;

private:
    http_parser*            m_parser;
    http_parser_settings    m_settings;
};

#endif // __COZY_HTTP_SERVER__