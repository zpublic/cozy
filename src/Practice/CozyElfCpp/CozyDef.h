#ifndef __COZY_ELF_DEF__
#define __COZY_ELF_DEF__

#include <cstdint>

#define SAFE_DELETE(ptr) do { if ( ptr != nullptr ) { delete ptr; ptr = nullptr; } } while (0)
#define SAFE_DELETE_ARRAY(ptr) do { if ( ptr != nullptr ) { delete[] ptr; ptr = nullptr; } } while (0)

namespace CozyElf
{
    typedef uint16_t Elf32_Half;
    typedef uint32_t Elf32_Addr;
    typedef uint32_t Elf32_Word;
    typedef uint32_t Elf32_Off;
}

#endif // __COZY_ELF_DEF__