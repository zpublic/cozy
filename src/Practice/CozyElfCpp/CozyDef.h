#ifndef __COZY_ELF_DEF__
#define __COZY_ELF_DEF__

#ifndef COZY_API_EXPORT
#define COZY_API _declspec(dllimport)
#else
#define COZY_API _declspec(dllexport)
#endif

#include <cstdint>

namespace CozyElf
{
    typedef uint16_t Elf32_Half;
    typedef uint32_t Elf32_Addr;
    typedef uint32_t Elf32_Word;
    typedef uint32_t Elf32_Off;
}

#endif // __COZY_ELF_DEF__