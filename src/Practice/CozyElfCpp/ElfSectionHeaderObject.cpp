#include "ElfSectionHeaderObject.h"
#include "ElfStructs.h"
using namespace CozyElf;


ElfSectionHeaderObject::ElfSectionHeaderObject(unsigned char* data, Elf32_Shdr* objBase, std::function<const char*(Elf32_Off)> callback)
    :m_rawData(data), m_objBase(objBase), m_strCallback(callback)
{

}

ElfSectionHeaderObject::~ElfSectionHeaderObject()
{

}

const char* ElfSectionHeaderObject::GetName() const
{
    if (m_strCallback != nullptr)
    {
        return m_strCallback(m_objBase->sh_name);
    }
    return nullptr;
}

SectionFlags ElfSectionHeaderObject::GetSectionFlags() const
{
    return static_cast<SectionFlags>(m_objBase->sh_flags);
}

SectionType ElfSectionHeaderObject::GetSectionType() const
{
    return static_cast<SectionType>(m_objBase->sh_type);
}

Elf32_Addr ElfSectionHeaderObject::GetAddress() const
{
    return m_objBase->sh_addr;
}

Elf32_Off ElfSectionHeaderObject::GetOffset() const
{
    return m_objBase->sh_offset;
}

Elf32_Word ElfSectionHeaderObject::GetSectionFileSize() const
{
    return m_objBase->sh_size;
}

Elf32_Word ElfSectionHeaderObject::GetAddressAlign() const
{
    return m_objBase->sh_addralign;
}

Elf32_Word ElfSectionHeaderObject::GetEntSize() const
{
    return m_objBase->sh_entsize;
}

bool ElfSectionHeaderObject::SetName(const char* filename)
{
    auto old_name = GetName();
    size_t old_len = std::strlen(old_name);
    size_t new_len = std::strlen(filename);
    if (new_len > old_len)
    {
        return false;
    }
    std::memcpy(const_cast<char*>(old_name), filename, new_len);
    return true;
}

void ElfSectionHeaderObject::SetSectionFlags(SectionFlags)
{

}

void ElfSectionHeaderObject::SetSectionType(SectionType)
{

}

void ElfSectionHeaderObject::SetAddress(Elf32_Addr)
{

}

void ElfSectionHeaderObject::SetOffset(Elf32_Off)
{

}

void ElfSectionHeaderObject::SetSectionFileSize(Elf32_Word)
{

}

void ElfSectionHeaderObject::SetAddressAlign(Elf32_Word)
{

}

void ElfSectionHeaderObject::SetEntSize(Elf32_Word)
{

}