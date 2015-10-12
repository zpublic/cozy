// CozyBufferTester.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include "src/buffer.h"
#include "src/buffer_helper.h"

class TestClass : public ISerialize
{
public:
    int         a;
    double      b;
    short       c;
    float       d;
    bool        e;
    std::string f;

public:
    // ISerialize
    virtual void Serialize(Buffer& buff)
    {
        SerializeObject(TestClass, buff, *this);
    }

    virtual void Deserialize(Buffer& buff)
    {
        DeserializeObject(TestClass, buff, *this);
    }
};

int main()
{
    RegisterSerialize(TestClass, a);
    RegisterSerialize(TestClass, b);
    RegisterSerialize(TestClass, c);
    RegisterSerialize(TestClass, d);
    RegisterSerialize(TestClass, e);
    RegisterSerialize(TestClass, f);

    Buffer buffer;
    TestClass t;
    TestClass o;

    t.a = 1;
    t.b = 1.2;
    t.c = 2;
    t.d = 2.3f;
    t.e = true;
    t.f = "123";

    ISerialize *p = &t;
    t.Serialize(buffer);
    buffer.SeekIterator(Buffer::SeekType::Begin, 0);
    o.Deserialize(buffer);

    std::cout << o.a << std::endl;
    std::cout << o.b << std::endl;
    std::cout << o.c << std::endl;
    std::cout << o.d << std::endl;
    std::cout << o.e << std::endl;
    std::cout << o.f << std::endl;


    system("pause");
    return 0;
}


