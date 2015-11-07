#ifndef __COZY_ELF_DEF__
#define __COZY_ELF_DEF__

#include <cstdint>

#ifndef COZY_EXPORT
#define COZY_API _declspec(dllimport)
#else
#define COZY_API _declspec(dllexport)
#endif

namespace CozyElf
{
    typedef uint16_t Elf32_Half;
    typedef uint32_t Elf32_Addr;
    typedef uint32_t Elf32_Off;
    typedef int32_t Elf32_Sword;
    typedef uint32_t Elf32_Word;
}

#endif // __COZY_ELF_DEF__