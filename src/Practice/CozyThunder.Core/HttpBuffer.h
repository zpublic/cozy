#ifndef __COZY_HTTP_BUFFER__
#define __COZY_HTTP_BUFFER__

#include "HttpDef.h"

namespace Cozy
{
    class HttpBuffer : public IBuffer
    {
    public:
        explicit HttpBuffer();
        explicit HttpBuffer(byte_t* data, std::size_t size);

        virtual std::size_t GetSize() const;
        virtual const byte_t* GetData() const;
        virtual std::size_t Write(byte_t* data, std::size_t size);
        virtual void Clear();

    private:
        std::vector<byte_t> m_vecData;
    };
}

#endif // __COZY_HTTP_BUFFER__    
