#ifndef __COZY_BUFFER__
#define __COZY_BUFFER__

#include "buffer_base.h"
#include <vector>

class Buffer
{
public:
    enum class SeekType : int
    {
        Begin,
        End,
        Curr,
    };

public:
    // Read
    bool_t ReadBoolean();
    byte_t ReadByte();
    int16_t ReadInt16();
    uint16_t ReadUInt16();
    int32_t ReadInt32();
    uint32_t ReadUInt32();
    int64_t ReadInt64();
    uint64_t ReadUInt64();
    float_t ReadFloat();
    double_t ReadDouble();
    char_t ReadChar();
    wchar_t ReadWChar();
    string_t ReadString();
    wstring_t ReadWString();
    void ReadBytes(byte_t* p, int length);

    template<class T>
    void Read(T& n)
    {
        
    }

public:
    // Write
    void Write(bool_t n);
    void Write(byte_t n);
    void Write(int16_t n);
    void Write(uint16_t n);
    void Write(int32_t n);
    void Write(uint32_t n);
    void Write(int64_t n);
    void Write(uint64_t n);
    void Write(float_t n);
    void Write(double_t n);
    void Write(char_t n);
    void Write(wchar_t n);
    void Write(cstr_t n);
    void Write(cwstr_t n);
    void Write(const string_t& n);
    void Write(const wstring_t& n);
    void Write(byte_t* p, int length);

    template<class T>
    void Write(const T& n)
    {
        
    }

public:
    // Peek
    bool_t PeekBoolean();
    byte_t PeekByte();
    int16_t PeekInt16();
    uint16_t PeekUInt16();
    int32_t PeekInt32();
    uint32_t PeekUInt32();
    int64_t PeekInt64();
    uint64_t PeekUInt64();
    float_t PeekFloat();
    double_t PeekDouble();
    char_t PeekChar();
    wchar_t PeekWChar();
    string_t PeekString();
    wstring_t PeekWString();
    void PeekBytes(byte_t* p, int length);

    template<class T>
    void Peek(T& n)
    {

    }

public:
    Buffer(const Buffer& o)
        :m_data(o.m_data), m_dataIterator(o.m_dataIterator)
    {

    }

    Buffer(Buffer&& o)
        :m_data(std::move(o.m_data)), m_dataIterator(std::move(o.m_dataIterator))
    {

    }

    Buffer& operator=(const Buffer& o)
    {
        m_data          = o.m_data;
        m_dataIterator  = o.m_dataIterator;
        return *this;
    }

    Buffer& operator=(Buffer&& o)
    {
        m_data          = std::move(o.m_data);
        m_dataIterator  = std::move(o.m_dataIterator);
        return *this;
    }

    Buffer()                            = default;
    ~Buffer()                           = default;
    bool operator==(const Buffer& o)    = delete;
    bool operator!=(const Buffer& o)    = delete;

public:

    // Iteratir
    void SeekIterator(SeekType type, int offset);

    void Clear();
private:
    std::vector<byte_t> m_data;
    std::size_t m_dataIterator;
};

class ISerialize
{
    virtual void Serialize(Buffer& buff) = 0;
    virtual void Deserialize(Buffer& buff) = 0;
};

template<> void Buffer::Read<bool_t>(bool_t& n);
template<> void Buffer::Read<byte_t>(byte_t& n);
template<> void Buffer::Read<int16_t>(int16_t& n);
template<> void Buffer::Read<uint16_t>(uint16_t& n);
template<> void Buffer::Read<int32_t>(int32_t& n);
template<> void Buffer::Read<uint32_t>(uint32_t& n);
template<> void Buffer::Read<int64_t>(int64_t& n);
template<> void Buffer::Read<uint64_t>(uint64_t& n);
template<> void Buffer::Read<float_t>(float_t& n);
template<> void Buffer::Read<double_t>(double_t& n);
template<> void Buffer::Read<char_t>(char_t& n);
template<> void Buffer::Read<wchar_t>(wchar_t& n);
template<> void Buffer::Read<string_t>(string_t& n);
template<> void Buffer::Read<wstring_t>(wstring_t& n);

#endif // __COZY_BUFFER__