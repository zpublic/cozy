#include "ElfObject.h"
#include <cstring>
#include "ElfStructs.h"

using namespace CozyElf;

ElfObject::ElfObject(unsigned char* data, size_t length)
    :m_rawData(nullptr),m_elfBase(nullptr),m_length(length)
{
    m_rawData = new unsigned char[length];
    m_elfBase = reinterpret_cast<Elf32_Ehdr*>(m_rawData);
    std::memcpy(m_rawData, data, length);
}


ElfObject::~ElfObject()
{
    delete[] m_rawData;
}

ElfClass ElfObject::GetElfClass() const
{
    return static_cast<ElfClass>(m_elfBase->e_ident[5]);
}

ElfEndianess ElfObject::GetElfEndianess() const
{
    return static_cast<ElfEndianess>(m_elfBase->e_ident[6]);
}

ElfFileType ElfObject::GetElfFileType() const
{
    return static_cast<ElfFileType>(m_elfBase->e_type);
}

ElfMachineType ElfObject::GetElfMachineType() const
{
    return static_cast<ElfMachineType>(m_elfBase->e_machine);
}

Elf32_Word ElfObject::GetElfVersion() const
{
    return m_elfBase->e_version;
}

Elf32_Addr ElfObject::GetEntry() const
{
    return m_elfBase->e_entry;
}

Elf32_Half ElfObject::GetPhdrCount() const
{
    return m_elfBase->e_phnum;
}

Elf32_Half ElfObject::GetShdrCount() const
{
    return m_elfBase->e_shnum;
}

ElfProgramHeaderObject ElfObject::GetPhdr(int index) const
{
    return ElfProgramHeaderObject(m_rawData, reinterpret_cast<Elf32_Phdr*>(m_rawData + m_elfBase->e_phoff + m_elfBase->e_phentsize * index));
}

ElfSectionHeaderObject ElfObject::GetShdr(int index) const
{
    return ElfSectionHeaderObject(
        m_rawData, 
        reinterpret_cast<Elf32_Shdr*>(m_rawData + m_elfBase->e_shoff + m_elfBase->e_shentsize * index), 
        std::bind(&ElfObject::GetString, this, std::placeholders::_1));
}

const char* ElfObject::GetString(Elf32_Word offset) const
{
    Elf32_Shdr* stringTable = reinterpret_cast<Elf32_Shdr*>(m_rawData + m_elfBase->e_shoff + m_elfBase->e_shentsize * m_elfBase->e_shstrndx);
    return reinterpret_cast<const char*>(m_rawData + stringTable->sh_offset + offset);
}

size_t ElfObject::ToRawData(void* dest)
{
    if (m_rawData == 0)
    {
        return 0;
    }

    if (dest != nullptr)
    {
        std::memcpy(dest, m_rawData, m_length);
    }
    return m_length;
}

void ElfObject::SetElfClass(ElfClass type)
{
    m_elfBase->e_ident[5] = static_cast<unsigned char>(type);
}
void ElfObject::SetElfEndianess(ElfEndianess type)
{
    m_elfBase->e_ident[6] = static_cast<unsigned char>(type);
}

void ElfObject::SetElfFileType(ElfFileType type)
{
    m_elfBase->e_type = static_cast<Elf32_Half>(type);
}

void ElfObject::SetElfMachineType(ElfMachineType type)
{
    m_elfBase->e_machine = static_cast<Elf32_Half>(type);
}

void ElfObject::SetElfVersion(Elf32_Word version)
{
    m_elfBase->e_version = static_cast<Elf32_Word>(version);
}

void ElfObject::SetEntry(Elf32_Addr addr)
{
    m_elfBase->e_entry = addr;
}