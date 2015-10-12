#include "buffer.h"

bool_t Buffer::PeekBoolean()
{
    auto olditer    = m_dataIterator;

    auto ret        = ReadBoolean();

    m_dataIterator  = olditer;
    return ret;
}

byte_t Buffer::PeekByte()
{
    auto olditer    = m_dataIterator;

    auto ret        = ReadByte();

    m_dataIterator  = olditer;
    return ret;
}

int16_t Buffer::PeekInt16()
{
    auto olditer    = m_dataIterator;

    byte_t low      = ReadByte();
    byte_t high     = ReadByte();
    int16_t ret     = high;
    ret             = (ret << 8) | low;

    m_dataIterator  = olditer;
    return ret;
}

uint16_t Buffer::PeekUInt16()
{
    auto olditer    = m_dataIterator;

    byte_t low      = ReadByte();
    byte_t high     = ReadByte();
    uint16_t ret    = high;
    ret             = (ret << 8) | low;

    m_dataIterator  = olditer;
    return ret;
}

int32_t Buffer::PeekInt32()
{
    auto olditer    = m_dataIterator;

    uint16_t low    = ReadUInt16();
    uint16_t high   = ReadUInt16();
    int32_t ret     = high;
    ret             = (ret << 16) | low;

    m_dataIterator  = olditer;
    return ret;
}

uint32_t Buffer::PeekUInt32()
{
    auto olditer    = m_dataIterator;

    uint16_t low    = ReadUInt16();
    uint16_t high   = ReadUInt16();
    uint32_t ret    = high;
    ret             = (ret << 16) | low;

    m_dataIterator  = olditer;
    return ret;
}

int64_t Buffer::PeekInt64()
{
    auto olditer    = m_dataIterator;

    uint32_t low    = ReadUInt32();
    uint32_t high   = ReadUInt32();
    int64_t ret     = high;
    ret             = (ret << 32) | low;

    m_dataIterator  = olditer;
    return ret;
}

uint64_t Buffer::PeekUInt64()
{
    auto olditer    = m_dataIterator;

    uint32_t low    = ReadUInt32();
    uint32_t high   = ReadUInt32();
    uint64_t ret    = high;
    ret             = (ret << 32) | low;

    m_dataIterator  = olditer;
    return ret;
}

float_t Buffer::PeekFloat()
{
    auto olditer    = m_dataIterator;

    uint32_t tmp    = ReadUInt32();

    m_dataIterator  = olditer;
    return *(reinterpret_cast<float_t*>(&tmp));
}

double_t Buffer::PeekDouble()
{
    auto olditer    = m_dataIterator;

    uint64_t tmp    = ReadUInt64();

    m_dataIterator  = olditer;
    return *(reinterpret_cast<double_t*>(&tmp));
}

char_t Buffer::PeekChar()
{
    auto olditer    = m_dataIterator;

    char_t ret      = ReadChar();

    m_dataIterator  = olditer;
    return ret;
}

wchar_t Buffer::PeekWChar()
{
    auto olditer    = m_dataIterator;

    wchar_t ret     = ReadWChar();

    m_dataIterator  = olditer;
    return ret;
}

wstring_t Buffer::PeekWString()
{
    auto olditer    = m_dataIterator;

    auto ret        = ReadWString();

    m_dataIterator  = olditer;
    return ret;
}

string_t Buffer::PeekString()
{
    auto olditer    = m_dataIterator;

    auto ret        = ReadString();

    m_dataIterator = olditer;
    return ret;
}

void Buffer::PeekBytes(byte_t* p, int length)
{
    auto olditer    = m_dataIterator;

    ReadBytes(p, length);

    m_dataIterator  = olditer;
}