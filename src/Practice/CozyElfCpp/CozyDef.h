#ifndef __COZY_ELF_DEF__
#define __COZY_ELF_DEF__

#include <cstdint>
#include "windows.h"

#ifndef COZY_API_EXPORT
#define COZY_API _declspec(dllimport)
#else
#define COZY_API _declspec(dllexport)
#endif

#define SAFE_DELETE(ptr) do { if ( ptr != nullptr ) { delete ptr; ptr = nullptr; } } while (0)
#define SAFE_DELETE_ARRAY(ptr) do { if ( ptr != nullptr ) { delete[] ptr; ptr = nullptr; } } while (0)
#define SAFE_CLOSE(handle) do { if ( handle != INVALID_HANDLE_VALUE ) { ::CloseHandle(handle); handle = INVALID_HANDLE_VALUE; } } while (0)

namespace CozyElf
{
    typedef uint16_t Elf32_Half;
    typedef uint32_t Elf32_Addr;
    typedef uint32_t Elf32_Word;
    typedef uint32_t Elf32_Off;
}

#endif // __COZY_ELF_DEF__