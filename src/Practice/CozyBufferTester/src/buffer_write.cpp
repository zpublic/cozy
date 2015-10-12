#include "buffer.h"
#include "windows.h"
#include <cstring>

void Buffer::Write(bool_t n)
{
    m_data.push_back(byte_t(n));
    SeekIterator(SeekType::Curr, sizeof(bool_t));
}

void Buffer::Write(byte_t n)
{
    m_data.push_back(n);
    SeekIterator(SeekType::Curr, sizeof(byte_t));
}

void Buffer::Write(int16_t n)
{
    byte_t low      = n & 0x00FFU;
    byte_t high     = (n & 0xFF00U) >> 8;
    m_data.push_back(low);
    m_data.push_back(high);
    SeekIterator(SeekType::Curr, sizeof(int16_t));
}

void Buffer::Write(uint16_t n)
{
    byte_t low      = n & 0x00FFU;
    byte_t high     = (n & 0xFF00U) >> 8;
    m_data.push_back(low);
    m_data.push_back(high);
    SeekIterator(SeekType::Curr, sizeof(uint16_t));
}

void Buffer::Write(int32_t n)
{
    uint16_t low    = n & 0x0000FFFFU;
    uint16_t high   = (n & 0xFFFF0000U) >> 16;
    Write(low);
    Write(high);
}

void Buffer::Write(uint32_t n)
{
    uint16_t low    = n & 0x0000FFFFU;
    uint16_t high   = (n & 0xFFFF0000U) >> 16;
    Write(low);
    Write(high);
}

void Buffer::Write(int64_t n)
{
    uint32_t low    = n & 0x00000000FFFFFFFF;
    uint32_t high   = (n & 0xFFFFFFFF00000000) >> 32;
    Write(low);
    Write(high);
}

void Buffer::Write(uint64_t n)
{
    uint32_t low = n & 0x00000000FFFFFFFF;
    uint32_t high = (n & 0xFFFFFFFF00000000) >> 32;
    Write(low);
    Write(high);
}

void Buffer::Write(float_t n)
{
    uint32_t tmp = *(reinterpret_cast<uint32_t*>(&n));
    Write(tmp);
}

void Buffer::Write(double_t n)
{
    uint64_t tmp = *(reinterpret_cast<uint64_t*>(&n));
    Write(tmp);
}

void Buffer::Write(char_t n)
{
    Write((byte)n);
}

void Buffer::Write(wchar_t n)
{
    Write((uint16_t)n);
}

void Buffer::Write(const cstr_t n)
{
    int nBufSize = MultiByteToWideChar(GetACP(), 0, n, -1, NULL, 0);
    wchar_t *wsBuf = new wchar_t[nBufSize];
    MultiByteToWideChar(GetACP(), 0, n, -1, wsBuf, nBufSize);
    Write(nBufSize);
    for (int i = 0; i < nBufSize; ++i)
    {
        Write(wsBuf[i]);
    }
    delete[] wsBuf;
}

void Buffer::Write(const cwstr_t n)
{
    uint32_t len = std::wcslen(n);
    Write(len);
    for (uint32_t i = 0; i < len; ++i)
    {
        Write(n[i]);
    }
}

void Buffer::Write(const string_t& n)
{
    Write(n.size());
    for (uint32_t i = 0; i < n.size(); ++i)
    {
        Write(n[i]);
    }
}

void Buffer::Write(const wstring_t& n)
{
    Write(n.size());
    for (uint32_t i = 0; i < n.size(); ++i)
    {
        Write(n[i]);
    }
}

void Buffer::Write(byte_t* p, int length)
{
    m_data.insert(m_data.end(), p, p + length);
    SeekIterator(SeekType::Curr, sizeof(byte_t) * length);
}