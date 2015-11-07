#include "ElfObject.h"

using namespace CozyElf;

ElfObject::ElfObject(Elf32* obj)
    :m_obj(obj)
{

}

ElfObject::~ElfObject()
{

}

ElfClass ElfObject::GetElfClass() const
{
    return (ElfClass)m_obj->m_header.e_ident[5];
}

Endianess ElfObject::GetEndian() const
{
    return (Endianess)m_obj->m_header.e_ident[6];
}

ElfFileType ElfObject::GetFileType() const
{
    return (ElfFileType)m_obj->m_header.e_type;
}

MachineType ElfObject::GetMachineType() const
{
    return (MachineType)m_obj->m_header.e_machine;
}

Elf32_Word ElfObject::GetElfVersion() const
{
    return m_obj->m_header.e_version;
}

Elf32_Addr ElfObject::GetEntry() const
{
    return m_obj->m_header.e_entry;
}
