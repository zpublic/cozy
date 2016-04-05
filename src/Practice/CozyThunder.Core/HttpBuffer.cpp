#include "stdafx.h"
#include "HttpBuffer.h"


HttpBuffer::HttpBuffer()
    :HttpBuffer(DefaultBufferSize)
{

}

HttpBuffer::HttpBuffer(std::size_t size)
    :m_vecData(size)
{

}

HttpBuffer::HttpBuffer(byte_t *data, std::size_t size)
    :m_vecData(data, data + size)
{

}

std::size_t HttpBuffer::GetSize() const
{
    return m_vecData.size();
}

const byte_t* HttpBuffer::GetData() const
{
    return m_vecData.data();
}

std::size_t HttpBuffer::Write(byte_t* data, std::size_t size, std::size_t offset)
{
    std::size_t actWriteSize = size;
    if (offset + size > m_vecData.size())
    {
        actWriteSize = m_vecData.size() - offset;
    }

    m_vecData.insert(m_vecData.begin() + offset, data, data + actWriteSize);
    return actWriteSize;
}

void HttpBuffer::Clear()
{
    m_vecData.clear();
}