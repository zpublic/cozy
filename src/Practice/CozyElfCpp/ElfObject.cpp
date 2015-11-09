#include "ElfObject.h"
#include <cstring>
#include "CozyEnum.h"

using namespace CozyElf;

zl_uchar DefaultMagicNumber[] = { 0x7f, 0x45, 0x4c, 0x46 };

ElfObject::ElfObject()
    :m_segment_table(nullptr), m_section_table(nullptr), m_filename(nullptr), m_string_table(nullptr), m_file(nullptr)
{
    Clear();
}

bool ElfObject::Init(const zl_char* pszFilename)
{
    Clear();
    m_file = std::fopen(pszFilename, "rb+");

    if (m_file != nullptr)
    {
        if (TryRead())
        {
            std::fseek(m_file, 0, SEEK_SET);
            if (!std::fread(&m_elf_header, sizeof(Elf32_Ehdr), 1, m_file))
            {
                return false;
            }

            // 初始化SegmentTable
            m_segment_num       = m_elf_header.e_phnum;
            m_segment_table     = new Elf32_Phdr[m_segment_num];

            std::fseek(m_file, m_elf_header.e_phoff, SEEK_SET);
            if (std::fread(m_segment_table, sizeof(Elf32_Phdr), m_segment_num, m_file) !=  m_segment_num)
            {
                return false;
            }


            // 初始化SectionTable
            m_section_num       = m_elf_header.e_shnum;
            m_section_table     = new Elf32_Shdr[m_section_num];

            std::fseek(m_file, m_elf_header.e_shoff, SEEK_SET);
            if (std::fread(m_section_table, sizeof(Elf32_Shdr), m_section_num, m_file) !=  m_section_num)
            {
                return false;
            }

            // 初始化字符串表
            InitStringTable();

            m_filename = pszFilename;

            return true;
        }
        
    }
    return false;
}

bool ElfObject::TryRead()
{
    if (m_file != nullptr)
    {
        std::fseek(m_file, 0, SEEK_SET);

        zl_uchar magic_number_and_other[6];
        if (!std::fread(magic_number_and_other, 6, 1, m_file))
        {
            return false;
        }

        for (int i = 0; i < 4; ++i)
        {
            if (magic_number_and_other[i] != DefaultMagicNumber[i])
            {
                return false;
            }
        }

        if (static_cast<ElfClass>(magic_number_and_other[4]) != ElfClass::X86)
        {
            return false;
        }

        if (static_cast<ElfEndianess>(magic_number_and_other[5]) != ElfEndianess::LittleEndian)
        {
            return false;
        }

        return true;
    }
    return false;
}

void ElfObject::Clear()
{
    m_segment_num    = 0;
    m_section_num    = 0;
    m_file_size      = 0;

    if (m_file != nullptr)
    {
        std::fclose(m_file);
        m_file = nullptr;
    }

    SAFE_DELETE_ARRAY(m_segment_table);
    SAFE_DELETE_ARRAY(m_section_table);
    SAFE_DELETE_ARRAY(m_string_table);

    std::memset(&m_elf_header, 0, sizeof(m_elf_header));
}

void ElfObject::Release()
{
    Clear();
    delete this;
}

Elf32_Ehdr* ElfObject::GetElfHeader()
{
    if (m_filename == nullptr) return nullptr;

    return &m_elf_header;
}

Elf32_Phdr* ElfObject::GetSegmentTable(zl_int32* pNum)
{
    if (m_filename == nullptr) return nullptr;
    if (pNum != nullptr)
    {
        *pNum = m_segment_num;
    }
    return m_segment_table;
}

Elf32_Shdr* ElfObject::GetSectionTable(zl_int32* pNum)
{
    if (m_filename == nullptr) return nullptr;
    if (pNum != nullptr)
    {
        *pNum = m_section_num;
    }
    return m_section_table;
}

Elf32_Addr ElfObject::GetEntryPoint() const
{
    if (m_filename == nullptr) return -1;
    return m_elf_header.e_entry;
}

const zl_char* ElfObject::GetString(Elf32_Off offset) const
{
    if (m_filename == nullptr || m_string_table == nullptr) return nullptr;
    return m_string_table + offset;
}

void ElfObject::InitStringTable()
{
    Elf32_Off stroff        = m_section_table[m_elf_header.e_shstrndx].sh_offset;
    Elf32_Word length       = m_section_table[m_elf_header.e_shstrndx].sh_size;
    m_string_table          = new zl_char[length];

    std::fseek(m_file, stroff, SEEK_SET);
    std::fread(m_string_table, length, 1, m_file);
}

void ElfObject::SaveElfHeader()
{
    if (m_file != nullptr)
    {
        std::fseek(m_file, 0, SEEK_SET);
        std::fwrite(&m_elf_header, sizeof(m_elf_header), 1, m_file);
    }
}

void ElfObject::SaveSegmentTable()
{
    if (m_file != nullptr && m_segment_table != nullptr)
    {
        Elf32_Off offset    = m_elf_header.e_phoff;
        Elf32_Word length   = sizeof(Elf32_Phdr) * m_segment_num;

        SaveToFile(m_segment_table, offset, length);
    }
}

void ElfObject::SaveSectionTable()
{
    if (m_file != nullptr && m_section_table != nullptr)
    {
        Elf32_Off offset    = m_elf_header.e_shoff;
        Elf32_Word length   = sizeof(Elf32_Shdr) * m_section_num;
        
        SaveToFile(m_section_table, offset, length);
    }
}

void ElfObject::SaveStringTable()
{
    if (m_file != nullptr && m_string_table != nullptr)
    {
        Elf32_Off stroff    = m_section_table[m_elf_header.e_shstrndx].sh_offset;
        Elf32_Word length   = m_section_table[m_elf_header.e_shstrndx].sh_size;

        SaveToFile(m_string_table, stroff, length);
    }
}

const zl_char* ElfObject::GetFileName() const
{
    return m_filename;
}

zl_uint32 ElfObject::GetFileSize() const
{
    return m_file_size;
}

zl_int32 ElfObject::SectionToFile(zl_uint32 dwIndex) const
{
    if (dwIndex >= m_section_num) return -1;

    return m_section_table[dwIndex].sh_offset;
}

zl_int32 ElfObject::FileToSection(zl_uint32 dwOffset) const
{
    for (zl_uint16 i = 0; i < m_section_num; ++i)
    {
        if (dwOffset >= m_section_table[i].sh_offset && dwOffset < m_section_table[i].sh_offset + m_section_table[i].sh_size)
        {
            return i;
        }
    }
    return -1;
}

void ElfObject::SaveToFile(const void* src, zl_uint32 offset, zl_uint32 length)
{
    std::fseek(m_file, offset, SEEK_SET);
    std::fwrite(src, length, 1, m_file);
}