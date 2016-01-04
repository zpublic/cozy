#pragma once
#include <vector>

typedef struct _ADDRESS_INFO
{
    LPBYTE  addr;
    int     size;
}ADDRESS_INFO;

typedef std::vector<ADDRESS_INFO>   ADDRESS_LIST;
