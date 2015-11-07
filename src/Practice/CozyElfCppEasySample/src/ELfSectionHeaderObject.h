#ifndef __COZY_ELF_SECTION_HEADER_OBJECT__
#define __COZY_ELF_SECTION_HEADER_OBJECT__

#include "ElfDef.h"
#include "ElfStructs.h"

namespace CozyElf
{
    class COZY_API ELfSectionHeaderObject
    {
    public:
        ELfSectionHeaderObject(Elf32_Shdr* obj);
        ~ELfSectionHeaderObject();

    private:
        Elf32_Shdr* m_obj;
    };
}

#endif // __COZY_ELF_SECTION_HEADER_OBJECT__