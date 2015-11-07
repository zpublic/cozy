#ifndef __COZY_ELF_STRUCTS__
#define __COZY_ELF_STRUCTS__

#pragma warning(disable:4251)

#include "ELFDef.h"
#include <vector>

namespace CozyElf
{
    static const unsigned char DefaultMagicNumber[] = { 0x7f, 0x45, 0x4c, 0x46 };

    struct COZY_API Elf32_Ehdr
    {
        static const int    EI_NIDENT = 16;
        unsigned char       e_ident[EI_NIDENT];
        Elf32_Half          e_type;
        Elf32_Half          e_machine;
        Elf32_Word          e_version;
        Elf32_Addr          e_entry;
        Elf32_Off           e_phoff;
        Elf32_Off           e_shoff;
        Elf32_Word          e_flags;
        Elf32_Half          e_ehsize;
        Elf32_Half          e_phentsize;
        Elf32_Half          e_phnum;
        Elf32_Half          e_shentsize;
        Elf32_Half          e_shnum;
        Elf32_Half          e_shstrndx;
    };

    struct COZY_API Elf32_Phdr
    {
        Elf32_Word      p_type;
        Elf32_Off       p_offset;
        Elf32_Addr      p_vaddr;
        Elf32_Addr      p_paddr;
        Elf32_Word      p_filesz;
        Elf32_Word      p_memsz;
        Elf32_Word      p_flags;
        Elf32_Word      p_align;
    };

    struct COZY_API Elf32_Shdr
    {
        Elf32_Word  sh_name;
        Elf32_Word  sh_type;
        Elf32_Word  sh_flags;
        Elf32_Addr  sh_addr;
        Elf32_Off   sh_offset;
        Elf32_Word  sh_size;
        Elf32_Word  sh_link;
        Elf32_Word  sh_info;
        Elf32_Word  sh_addralign;
        Elf32_Word  sh_entsize;
    };

    struct COZY_API Elf32
    {
        Elf32_Ehdr              m_header;
        std::vector<Elf32_Phdr> m_program_header;
        std::vector<Elf32_Shdr> m_section_header;
    };
}

#endif