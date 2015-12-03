#ifndef _H_Z_HTTP_DEF_H_
#define _H_Z_HTTP_DEF_H_

#include <string>
#include "z_platform_type.h"

#ifndef _UNICODE
#define zl_tchar wchar_t
#else
#define zl_tchar char
#endif

enum HttpMethod
{
    GetMethod,
    PostMethod,
};

#define NS_BEGIN namespace zl { namespace http {

#define NS_END } }

#endif // _H_Z_HTTP_DEF_H_
