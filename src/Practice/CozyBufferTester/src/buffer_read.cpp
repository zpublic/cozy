#include "buffer.h"

bool_t Buffer::ReadBoolean()
{
    Assert(m_dataIterator + sizeof(bool_t) <= m_data.size());

    bool_t ret = m_data[m_dataIterator] != 0;
    SeekIterator(SeekType::Curr, sizeof(bool_t));
    return ret;
}

byte_t Buffer::ReadByte()
{
    Assert(m_dataIterator + sizeof(byte_t) <= m_data.size());

    byte_t ret = m_data[m_dataIterator];
    SeekIterator(SeekType::Curr, sizeof(byte_t));
    return ret;
}

int16_t Buffer::ReadInt16()
{
    Assert(m_dataIterator + sizeof(int16_t) <= m_data.size());

    byte_t low  = ReadByte();
    byte_t high = ReadByte();

    int16_t ret = high;
    ret = (ret << 8) | low;
    return ret;
}

uint16_t Buffer::ReadUInt16()
{
    Assert(m_dataIterator + sizeof(uint16_t) <= m_data.size());

    byte_t low = ReadByte();
    byte_t high = ReadByte();

    uint16_t ret = high;
    ret = (ret << 8) | low;
    return ret;
}

int32_t Buffer::ReadInt32()
{
    Assert(m_dataIterator + sizeof(int32_t) <= m_data.size());

    uint16_t low = ReadUInt16();
    uint16_t high = ReadUInt16();

    int32_t ret = high;
    ret = (ret << 16) | low;
    return ret;
}

uint32_t Buffer::ReadUInt32()
{
    Assert(m_dataIterator + sizeof(uint32_t) <= m_data.size());

    uint16_t low = ReadUInt16();
    uint16_t high = ReadUInt16();

    uint32_t ret = high;
    ret = (ret << 16) | low;
    return ret;
}

int64_t Buffer::ReadInt64()
{
    Assert(m_dataIterator + sizeof(int64_t) <= m_data.size());

    uint32_t low = ReadUInt32();
    uint32_t high = ReadUInt32();

    int64_t ret = high;
    ret = (ret << 32) | low;
    return ret;
}

uint64_t Buffer::ReadUInt64()
{
    Assert(m_dataIterator + sizeof(uint64_t) <= m_data.size());

    uint32_t low = ReadUInt32();
    uint32_t high = ReadUInt32();

    uint64_t ret = high;
    ret = (ret << 32) | low;
    return ret;
}

float_t Buffer::ReadFloat()
{
    Assert(m_dataIterator + sizeof(float_t) <= m_data.size());

    uint32_t tmp = ReadUInt32();
    return *(reinterpret_cast<float_t*>(&tmp));
}

double_t Buffer::ReadDouble()
{
    Assert(m_dataIterator + sizeof(double_t) <= m_data.size());

    uint64_t tmp = ReadUInt64();
    return *(reinterpret_cast<double_t*>(&tmp));
}

wchar_t Buffer::ReadWChar()
{
    Assert(m_dataIterator + sizeof(wchar_t) <= m_data.size());

    return (wchar_t)ReadUInt16();
}

char_t Buffer::ReadChar()
{
    Assert(m_dataIterator + sizeof(char_t) <= m_data.size());

    return (char_t)ReadByte();
}

wstring_t Buffer::ReadWString()
{
    uint32_t len    = ReadUInt32();
    Assert(m_dataIterator + sizeof(wchar_t) * len  <= m_data.size());

    auto result = wstring_t(m_data.begin() + m_dataIterator, m_data.begin() + m_dataIterator + len * sizeof(wchar_t));
    SeekIterator(SeekType::Curr, len * sizeof(wchar_t));
    return result;
}

string_t Buffer::ReadString()
{
    uint32_t len    = ReadUInt32();
    Assert(m_dataIterator + sizeof(char_t) * len <= m_data.size());

    auto result = string_t(m_data.begin() + m_dataIterator, m_data.begin() + m_dataIterator + len * sizeof(char_t));
    SeekIterator(SeekType::Curr, len * sizeof(char_t));
    return result;
}

void Buffer::ReadBytes(byte_t* p, int length)
{
    Assert(m_dataIterator + sizeof(byte_t) * length <= m_data.size());

    std::copy(m_data.begin() + m_dataIterator, m_data.begin() + m_dataIterator + (uint32_t)length, p);
    SeekIterator(SeekType::Curr, sizeof(byte_t) * length);
}

// Specialization Read
template<>
void Buffer::Read<bool_t>(bool_t& n)
{
    n = ReadBoolean();
}

template<>
void Buffer::Read<byte_t>(byte_t& n)
{
    n = ReadByte();
}

template<>
void Buffer::Read<int16_t>(int16_t& n)
{
    n = ReadInt16();
}

template<>
void Buffer::Read<uint16_t>(uint16_t& n)
{
    n = ReadUInt16();
}

template<>
void Buffer::Read<int32_t>(int32_t& n)
{
    n = ReadInt32();
}

template<>
void Buffer::Read<uint32_t>(uint32_t& n)
{
    n = ReadUInt32();
}

template<>
void Buffer::Read<int64_t>(int64_t& n)
{
    n = ReadInt64();
}

template<>
void Buffer::Read<uint64_t>(uint64_t& n)
{
    n = ReadUInt64();
}

template<>
void Buffer::Read<float_t>(float_t& n)
{
    n = ReadFloat();
}

template<>
void Buffer::Read<double_t>(double_t& n)
{
    n = ReadDouble();
}

template<>
void Buffer::Read<char_t>(char_t& n)
{
    n = ReadChar();
}

template<>
void Buffer::Read<wchar_t>(wchar_t& n)
{
    n = ReadWChar();
}

template<>
void Buffer::Read<string_t>(string_t& n)
{
    n = ReadString();
}

template<>
void Buffer::Read<wstring_t>(wstring_t& n)
{
    n = ReadWString();
}