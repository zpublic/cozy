#include "ElfProgramHeaderObject.h"
#include "ElfStructs.h"
using namespace CozyElf;

ElfProgramHeaderObject::ElfProgramHeaderObject(unsigned char* data, Elf32_Phdr* objBase)
    :m_rawData(data), m_ObjBase(objBase)
{

}

ElfProgramHeaderObject::~ElfProgramHeaderObject()
{

}

ProgramHeaderType ElfProgramHeaderObject::GetProgramHeaderType() const
{
    return static_cast<ProgramHeaderType>(m_ObjBase->p_type);
}

Elf32_Word ElfProgramHeaderObject::GetFlags() const
{
    return m_ObjBase->p_flags;
}

Elf32_Addr ElfProgramHeaderObject::GetVirtualAddress() const
{
    return m_ObjBase->p_vaddr;
}

Elf32_Addr ElfProgramHeaderObject::GetPhysicalAddress() const
{
    return m_ObjBase->p_paddr;
}

Elf32_Word ElfProgramHeaderObject::GetFileSize() const
{
    return m_ObjBase->p_filesz;
}

Elf32_Word ElfProgramHeaderObject::GetMemorySize() const
{
    return m_ObjBase->p_memsz;
}

Elf32_Word ElfProgramHeaderObject::GetAlign() const
{
    return m_ObjBase->p_align;
}

void ElfProgramHeaderObject::SetProgramHeaderType(ProgramHeaderType type)
{
    m_ObjBase->p_type = static_cast<Elf32_Word>(type);
}

void ElfProgramHeaderObject::SetFlags(Elf32_Word flags)
{
    m_ObjBase->p_flags = flags;
}

void ElfProgramHeaderObject::SetVirtualAddress(Elf32_Addr addr)
{
    m_ObjBase->p_vaddr = addr;
}

void ElfProgramHeaderObject::SetPhysicalAddress(Elf32_Addr addr)
{
    m_ObjBase->p_paddr = addr;
}

void ElfProgramHeaderObject::SetFileSize(Elf32_Word size)
{
    m_ObjBase->p_filesz = size;
}

void ElfProgramHeaderObject::SetMemorySize(Elf32_Word size)
{
    m_ObjBase->p_memsz = size;
}

void ElfProgramHeaderObject::SetAlign(Elf32_Word align)
{
    m_ObjBase->p_align = align;
}