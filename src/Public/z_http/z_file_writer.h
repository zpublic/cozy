#ifndef _H_Z_FILE_WRITER_H_
#define _H_Z_FILE_WRITER_H_

#include "z_http_interface.h"
#include <cstdio>

NS_BEGIN

class ZLFileWriter : public IHttpWriter
{
public:
    ZLFileWriter(const std::string& strFileName);
    virtual ~ZLFileWriter();

    // IHttpWriter
    virtual zl_int32 Write(zl_uchar* pData, zl_uint32 nLength);
    virtual const zl_uchar* GetData();
    virtual zl_int32 GetLength();

private:
    std::string     m_strFileName;
    FILE*           m_pFile;
    zl_uint32       m_nLength;
};

NS_END

#endif // _H_Z_FILE_WRITER_H_