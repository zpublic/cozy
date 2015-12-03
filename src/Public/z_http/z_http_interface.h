#ifndef _H_Z_HTTP_INTERFACE_H_
#define _H_Z_HTTP_INTERFACE_H_

#include "z_http_def.h"

NS_BEGIN

class IHttpWriter
{
public:
    virtual zl_int32 Write(zl_uchar* pData, zl_uint32 nLength) = 0;
    virtual const zl_uchar* GetData() = 0;
    virtual zl_int32 GetLength() = 0;
};

class IHttpProgress
{
public:
    virtual zl_int32 OnProgress(double dltotal, double dlnow, double ultotal, double ulnow) = 0;
};

NS_END

#endif // _H_Z_HTTP_INTERFACE_H_
