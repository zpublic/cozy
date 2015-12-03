#include "z_stream_writer.h"
#include <iterator>

NS_BEGIN

ZLStreamWriter::ZLStreamWriter()
{

}

ZLStreamWriter::~ZLStreamWriter()
{

}

void ZLStreamWriter::Write(zl_uchar* pData, zl_uint32 nLength)
{
    std::copy(pData, pData + nLength, std::back_inserter(m_vecData));
}

zl_uchar* ZLStreamWriter::GetStream()
{
    return &m_vecData[0];
}

zl_int32 ZLStreamWriter::GetLength() const
{
    return m_vecData.size();
}

void ZLStreamWriter::Clear()
{
    m_vecData.clear();
}

NS_END