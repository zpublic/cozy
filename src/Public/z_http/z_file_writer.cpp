#include "z_file_writer.h"

NS_BEGIN

ZLFileWriter::ZLFileWriter(const std::string& strFileName)
    : m_strFileName(strFileName), m_pFile(NULL), m_nLength(0)
{
    m_pFile = std::fopen(strFileName.c_str(), "wb");
}

ZLFileWriter::~ZLFileWriter()
{
    if (m_pFile)
    {
        std::fclose(m_pFile);
        m_pFile     = NULL;
        m_nLength   = 0;
    }
}

zl_int32 ZLFileWriter::Write(zl_uchar* pData, zl_uint32 nLength)
{
    if (m_pFile)
    {
        std::fwrite(pData, nLength, 1, m_pFile);
    }
    m_nLength += nLength;
    return 1;
}

const zl_uchar* ZLFileWriter::GetData()
{
    return NULL;
}

zl_int32 ZLFileWriter::GetLength()
{
    return m_nLength;
}

NS_END