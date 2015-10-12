#ifndef __COZY_BUFFER_BASE__
#define __COZY_BUFFER_BASE__

#include <string>
#include <cassert>
#include <functional>

typedef bool bool_t;
typedef unsigned char byte_t;
typedef signed short int16_t;
typedef unsigned short uint16_t;
typedef signed int int32_t;
typedef unsigned int uint32_t;
typedef signed long long int64_t;
typedef unsigned long long uint64_t;
typedef float float_t;
typedef double double_t;
typedef char char_t;
typedef char* cstr_t;
typedef wchar_t* cwstr_t;
typedef std::string string_t;
typedef std::wstring wstring_t;

#ifdef _DEBUG
#define Assert(x) assert(x)
#else
#define Assert(x) 
#endif

class AutoCallObject
{
public:
    AutoCallObject(const std::function<void(void)>& func);
};

#endif // __COZY_BUFFER_BASE__
