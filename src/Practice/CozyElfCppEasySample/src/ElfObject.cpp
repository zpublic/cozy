#include "ElfObject.h"

using namespace CozyElf;

ElfObject::ElfObject(std::shared_ptr<Elf32> obj)
    :m_obj(obj)
{
    Init();
}

ElfObject::~ElfObject()
{

}

ElfClass ElfObject::GetElfClass() const
{
    return static_cast<ElfClass>(m_obj->m_header.e_ident[5]);
}

Endianess ElfObject::GetEndian() const
{
    return static_cast<Endianess>(m_obj->m_header.e_ident[6]);
}

ElfFileType ElfObject::GetFileType() const
{
    return static_cast<ElfFileType>(m_obj->m_header.e_type);
}

MachineType ElfObject::GetMachineType() const
{
    return static_cast<MachineType>(m_obj->m_header.e_machine);
}

Elf32_Word ElfObject::GetElfVersion() const
{
    return m_obj->m_header.e_version;
}

Elf32_Addr ElfObject::GetEntry() const
{
    return m_obj->m_header.e_entry;
}

void ElfObject::Init()
{
    if (m_obj != nullptr)
    {
        for (auto &h : m_obj->m_program_header)
        {
            m_PhdrObjs.push_back(ElfProgramHeaderObject(h));
        }
    }
}