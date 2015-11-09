#ifndef __COZY_ELF_DEF__
#define __COZY_ELF_DEF__

#include "../../Public/z_platform/z_platform_type.h"

#define SAFE_DELETE(ptr) do { if ( ptr != nullptr ) { delete ptr; ptr = nullptr; } } while (0)
#define SAFE_DELETE_ARRAY(ptr) do { if ( ptr != nullptr ) { delete[] ptr; ptr = nullptr; } } while (0)

namespace CozyElf
{
    typedef zl_uint16 Elf32_Half;
    typedef zl_uint32 Elf32_Addr;
    typedef zl_uint32 Elf32_Word;
    typedef zl_uint32 Elf32_Off;
}

#endif // __COZY_ELF_DEF__