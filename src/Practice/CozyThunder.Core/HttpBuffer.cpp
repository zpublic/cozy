#include "stdafx.h"
#include "HttpBuffer.h"

using namespace Cozy;

HttpBuffer::HttpBuffer()
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

std::size_t HttpBuffer::Write(byte_t* data, std::size_t size)
{
    m_vecData.insert(m_vecData.end(), data, data + size);
    return size;
}

void HttpBuffer::Clear()
{
    m_vecData.clear();
}