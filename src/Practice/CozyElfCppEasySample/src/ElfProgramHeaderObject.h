#ifndef __COZY_ELF_PROGRAM_HEADER_OBJECT__
#define __COZY_ELF_PROGRAM_HEADER_OBJECT__

#include "ElfDef.h"
#include "ElfStructs.h"
#include "ElfEnum.h"
#include <memory>

namespace CozyElf
{
    class COZY_API ElfProgramHeaderObject
    {
    public:
        ElfProgramHeaderObject(std::shared_ptr<Elf32_Phdr> obj);
        ~ElfProgramHeaderObject();

        SegmentType GetSegmentType() const;
        SegmentFlags GetSegmentFlags() const;
        Elf32_Addr GetPhysicalAddress() const;
        Elf32_Addr GetVirtualAddress() const;
        Elf32_Word GetMemorySize() const;
        Elf32_Word GetAlignment() const;
        Elf32_Word GetFileSize() const;

    private:
        std::shared_ptr<Elf32_Phdr> m_obj;
    };
}

#endif // __COZY_ELF_PROGRAM_HEADER_OBJECT__