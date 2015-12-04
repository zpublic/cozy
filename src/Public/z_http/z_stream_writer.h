#ifndef _H_Z_STREAM_WRITER_H_
#define _H_Z_STREAM_WRITER_H_

#include "z_http_interface.h"
#include <vector>

NS_ZL_BEGIN

class ZLStreamWriter
{
public:
    ZLStreamWriter();
    ~ZLStreamWriter();

    void Write(zl_uchar* pData, zl_uint32 nLength);
    zl_uchar* GetStream();
    zl_int32 GetLength() const;
    void Clear();

private:
    std::vector<zl_uchar> m_vecData;
};

NS_ZL_END

#endif // _H_Z_STREAM_WRITER_H_
