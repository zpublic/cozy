#ifndef __COZY_ELF_SECTION_HEADER_OBJECT__
#define __COZY_ELF_SECTION_HEADER_OBJECT__

#include "ElfDef.h"
#include "ElfStructs.h"
#include <memory>
#include <functional>

namespace CozyElf
{
    class COZY_API ELfSectionHeaderObject
    {
    public:
        ELfSectionHeaderObject(std::shared_ptr<Elf32_Shdr> obj, std::function<std::string(Elf32_Word)> GetNameCallback);
        ~ELfSectionHeaderObject();

        std::string GetName() const;

    private:
        std::shared_ptr<Elf32_Shdr>             m_obj;
        std::function<std::string(Elf32_Word)>  m_nameCallback;
    };
}

#endif // __COZY_ELF_SECTION_HEADER_OBJECT__