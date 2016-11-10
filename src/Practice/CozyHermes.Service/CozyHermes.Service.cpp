#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include "nanomsg/nn.h"
#include "nanomsg/reqrep.h"

int server(const char *url)
{
    int fd;
    fd = nn_socket(AF_SP, NN_REP);
    if (fd < 0)
    {
        fprintf(stderr, "nn_socket: %s\n", nn_strerror(nn_errno()));
        return (-1);
    }
    if (nn_bind(fd, url) < 0)
    {
        fprintf(stderr, "nn_bind: %s\n", nn_strerror(nn_errno()));
        nn_close(fd);
        return (-1);
    }

    for (;;)
    {
        char req[128];
        char rsp[] = "hehe";
        struct tm *now;
        char *daytime;
        int rc;
        char *fmt;

        rc = nn_recv(fd, req, sizeof(req), 0);
        if (rc < 0)
        {
            fprintf(stderr, "nn_recv: %s\n", nn_strerror(nn_errno()));
            break;
        }
        rc = nn_send(fd, rsp, strlen(rsp), 0);
        if (rc < 0)
        {
            fprintf(stderr, "nn_send: %s (ignoring)\n", nn_strerror(nn_errno()));
        }
    }
    nn_close(fd);
    return (-1);
}

int client(const char *url, const char *username)
{
    int fd;
    int rc;
    char *output;
    char *msg;

    fd = nn_socket(AF_SP, NN_REQ);
    if (fd < 0)
    {
        fprintf(stderr, "nn_socket: %s\n", nn_strerror(nn_errno()));
        return (-1);
    }
    if (nn_connect(fd, url) < 0)
    {
        fprintf(stderr, "nn_socket: %s\n", nn_strerror(nn_errno()));
        nn_close(fd);
        return (-1);
    }
    if (nn_send(fd, username, strlen(username), 0) < 0)
    {
        fprintf(stderr, "nn_send: %s\n", nn_strerror(nn_errno()));
        nn_close(fd);
        return (-1);
    }
    rc = nn_recv(fd, &msg, NN_MSG, 0);
    if (rc < 0)
    {
        fprintf(stderr, "nn_recv: %s\n", nn_strerror(nn_errno()));
        nn_close(fd);
        return (-1);
    }
    nn_close(fd);

    output = (char*)malloc(rc + 1);
    memcpy(output, msg, rc);
    output[rc] = '\0';

    nn_freemsg(msg);
    printf("%s\n", output);
    free(output);
    return (0);
}

int main()
{
    printf("hello world!");
    char c = getchar();
    if (c == 's')
        server("tcp://127.0.0.1:48360");
    else
        client("tcp://127.0.0.1:48360", "zap");
    return 0;
}

