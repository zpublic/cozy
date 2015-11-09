#ifndef __COZY_ELF_SECTION_HEADER_OBJECT__
#define __COZY_ELF_SECTION_HEADER_OBJECT__

#include "CozyDef.h"
#include <functional>
#include "CozyEnum.h"

namespace CozyElf
{
    struct Elf32_Shdr;
    class ElfObject;

    class COZY_API ElfSectionHeaderObject
    {
    public:
        ElfSectionHeaderObject(unsigned char* data, Elf32_Shdr* objBasestd, std::function<const char*(Elf32_Off)>);
        ~ElfSectionHeaderObject();
        const char* GetName() const;
        SectionFlags GetSectionFlags() const;
        SectionType GetSectionType() const;
        Elf32_Addr GetAddress() const;
        Elf32_Off GetOffset() const;
        Elf32_Word GetSectionFileSize() const;
        Elf32_Word GetAddressAlign() const;
        Elf32_Word GetEntSize() const;

         bool SetName(const char*);
         void SetSectionFlags(SectionFlags);
         void SetSectionType(SectionType);
         void SetAddress(Elf32_Addr);
         void SetOffset(Elf32_Off);
         void SetSectionFileSize(Elf32_Word);
         void SetAddressAlign(Elf32_Word);
         void SetEntSize(Elf32_Word);

    private: 
        std::function<const char*(Elf32_Word)> m_strCallback;

        unsigned char*  m_rawData;
        Elf32_Shdr*     m_objBase;
    };
}

#endif // __COZY_ELF_SECTION_HEADER_OBJECT__