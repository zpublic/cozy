#include "z_mem_writer.h"

NS_BEGIN

ZLMemWriter::ZLMemWriter()
{

}

ZLMemWriter::~ZLMemWriter()
{

}

zl_int32 ZLMemWriter::Write(zl_uchar* pData, zl_uint32 nLength)
{
    m_stream.Write(pData, nLength);
    return nLength;
}

const zl_uchar* ZLMemWriter::GetData()
{
    return const_cast<const zl_uchar*>(m_stream.GetStream());
}

zl_int32 ZLMemWriter::GetLength()
{
    return m_stream.GetLength();
}

NS_END