#ifndef __COZY_ELF_OBJECT__
#define __COZY_ELF_OBJECT__

#include "CozyDef.h"
#include "CozyEnum.h"
#include "ElfProgramHeaderObject.h"
#include "ElfSectionHeaderObject.h"

namespace CozyElf
{
    struct Elf32_Ehdr;
    class COZY_API ElfObject
    {
    public:
        ElfObject(unsigned char* data, size_t length);
        ~ElfObject();

        ElfClass GetElfClass() const;
        ElfEndianess GetElfEndianess() const;
        ElfFileType GetElfFileType() const;
        ElfMachineType GetElfMachineType() const;
        Elf32_Word GetElfVersion() const;
        Elf32_Addr GetEntry() const;

        void SetElfClass(ElfClass);
        void SetElfEndianess(ElfEndianess);
        void SetElfFileType(ElfFileType);
        void SetElfMachineType(ElfMachineType);
        void SetElfVersion(Elf32_Word);
        void SetEntry(Elf32_Addr);

        Elf32_Half GetPhdrCount() const;
        Elf32_Half GetShdrCount() const;
        ElfProgramHeaderObject GetPhdr(int index) const;
        ElfSectionHeaderObject GetShdr(int index) const;

        size_t ToRawData(void* dest);

private:
        const char* GetString(Elf32_Word offset) const;

private:
        unsigned char*  m_rawData;
        Elf32_Ehdr*     m_elfBase;
        size_t          m_length;
    };
}

#endif // __COZY_ELF_OBJECT__