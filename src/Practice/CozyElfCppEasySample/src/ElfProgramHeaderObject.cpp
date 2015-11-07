#include "ElfProgramHeaderObject.h"

using namespace CozyElf;

ElfProgramHeaderObject::ElfProgramHeaderObject(std::shared_ptr<Elf32_Phdr> obj)
    :m_obj(obj)
{

}

ElfProgramHeaderObject::~ElfProgramHeaderObject()
{

}

SegmentType ElfProgramHeaderObject::GetSegmentType() const
{
    return static_cast<SegmentType>(m_obj->p_type);
}

SegmentFlags ElfProgramHeaderObject::GetSegmentFlags() const
{
    return static_cast<SegmentFlags>(m_obj->p_flags);
}

Elf32_Addr ElfProgramHeaderObject::GetPhysicalAddress() const
{
    return m_obj->p_paddr;
}

Elf32_Addr ElfProgramHeaderObject::GetVirtualAddress() const
{
    return m_obj->p_vaddr;
}

Elf32_Word ElfProgramHeaderObject::GetMemorySize() const
{
    return m_obj->p_memsz;
}

Elf32_Word ElfProgramHeaderObject::GetAlignment() const
{
    return m_obj->p_align;
}

Elf32_Word ElfProgramHeaderObject::GetFileSize() const
{
    return m_obj->p_filesz;
}