#ifndef _H_Z_MEM_WRITER_H_
#define _H_Z_MEM_WRITER_H_

#include "z_http_interface.h"
#include "z_stream_writer.h"

NS_ZL_BEGIN

class ZLMemWriter : public IHttpWriter
{
public: 
    ZLMemWriter();
    ~ZLMemWriter();

    // IHttpWriter
    virtual zl_int32 Write(zl_uchar* pData, zl_uint32 nLength);
    virtual const zl_uchar* GetData();
    virtual zl_int32 GetLength();

private:
    ZLStreamWriter m_stream;
};

NS_ZL_END

#endif // _H_Z_MEM_WRITER_H_
