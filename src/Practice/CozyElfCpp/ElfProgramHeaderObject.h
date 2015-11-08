#ifndef __COZY_ELF_PROGRAM_HEADER_OBJECT__
#define __COZY_ELF_PROGRAM_HEADER_OBJECT__

#include "CozyDef.h"
#include "CozyEnum.h"

namespace CozyElf
{
    struct Elf32_Phdr;

    class COZY_API ElfProgramHeaderObject
    {
    public:
        ElfProgramHeaderObject(unsigned char* data, Elf32_Phdr* objBase);
        ~ElfProgramHeaderObject();

        ProgramHeaderType GetProgramHeaderType() const;
        Elf32_Word GetFlags() const;
        Elf32_Addr GetVirtualAddress() const;
        Elf32_Addr GetPhysicalAddress() const;
        Elf32_Word GetFileSize() const;
        Elf32_Word GetMemorySize() const;
        Elf32_Word GetAlign() const;

        void SetProgramHeaderType(ProgramHeaderType);
        void SetFlags(Elf32_Word);
        void SetVirtualAddress(Elf32_Addr);
        void SetPhysicalAddress(Elf32_Addr);
        void SetFileSize(Elf32_Word);
        void SetMemorySize(Elf32_Word);
        void SetAlign(Elf32_Word);


    private:
        unsigned char*  m_rawData;
        Elf32_Phdr*     m_ObjBase;
    };
}

#endif // __COZY_ELF_PROGRAM_HEADER_OBJECT__