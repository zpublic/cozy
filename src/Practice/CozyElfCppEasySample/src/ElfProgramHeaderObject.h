#ifndef __COZY_ELF_PROGRAM_HEADER_OBJECT__
#define __COZY_ELF_PROGRAM_HEADER_OBJECT__

#include "ElfDef.h"
#include "ElfStructs.h"

namespace CozyElf
{
    class COZY_API ElfProgramHeaderObject
    {
    public:
        ElfProgramHeaderObject(Elf32_Phdr* obj);
        ~ElfProgramHeaderObject();

    private:
        Elf32_Phdr* m_obj;
    };
}

#endif // __COZY_ELF_PROGRAM_HEADER_OBJECT__