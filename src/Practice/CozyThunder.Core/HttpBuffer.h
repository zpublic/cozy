#include "HttpDef.h"

    class HttpBuffer : public IBuffer
    {
    public:
        std::integral_constant<int, 4096> DefaultBufferSize;

    public:
        explicit HttpBuffer();
        explicit HttpBuffer(std::size_t size);
        explicit HttpBuffer(byte_t* data, std::size_t size);

        virtual std::size_t GetSize() const;
        virtual const byte_t* GetData() const;
        virtual std::size_t Write(byte_t* data, std::size_t size, std::size_t offset = 0);
        virtual void Clear();

    private:
        std::vector<byte_t> m_vecData;
    };
