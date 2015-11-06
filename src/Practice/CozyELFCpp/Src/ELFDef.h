#ifndef __COZY_ELF_DEF__
#define __COZY_ELF_DEF__

#include "cstdint"

#define GETTER(Name) auto Get##Name##() -> decltype(m_##Name##) const { return m_##Name##; }
#define SETTER(Name) void Set##Name##(const decltype(m_##Name##)& value) { m_##Name## = value; }

#ifndef COZY_EXPORT
#define COZY_API _declspec(dllimport)
#else
#define COZY_API _declspec(dllexport)
#endif

class ELFDefBase
{
public:
    typedef uint16_t ELF_HALF;
    typedef int32_t ELF_SWORD;
    typedef uint32_t ELF_WORD;
};

template<int N>
class ELFDef {};

template<>
class ELFDef<32> : public ELFDefBase
{
public:
    typedef uint32_t ELF_OFF;
    typedef uint32_t ELF_ADDR;
};

template<>
class ELFDef<64> : public ELFDefBase
{
public:
    typedef uint64_t ELF_OFF;
    typedef uint64_t ELF_ADDR;
};

#endif // __COZY_ELF_DEF__
