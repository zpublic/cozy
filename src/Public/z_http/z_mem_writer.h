#ifndef _H_Z_MEM_WRITER_H_
#define _H_Z_MEM_WRITER_H_

#include "z_http_interface.h"

NS_BEGIN

class ZLMemWriter : public IHttpWriter
{
public: 
    ZLMemWriter();
    ~ZLMemWriter();
};

NS_END


#endif // _H_Z_MEM_WRITER_H_
