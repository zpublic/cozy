#include "ElfReader.h"
#include <fstream>
using namespace CozyElf;

ElfReader::ElfReader()
{

}

ElfReader::~ElfReader()
{

}

Elf32* ElfReader::Load(const std::string& filename)
{
    std::ifstream fs(filename, std::ios::binary);
    if (fs.is_open())
    {
        if (TryLoad(fs))
        {
            Elf32* ptr = new Elf32();
            ReadHeader(fs, ptr);
            ReadProgramheader(fs, ptr);
            ReadSectionHeader(fs, ptr);
            return ptr;
        }
    }
    return nullptr;
}

bool ElfReader::TryLoad(std::ifstream& fs)
{
    fs.seekg(std::ios::beg);

    // 检查魔数
    unsigned char magic_number[4];
    fs.read(reinterpret_cast<char*>(magic_number), 4);

    for (int i = 0; i < 4; ++i)
    {
        if (magic_number[i] != DefaultMagicNumber[i])
        {
            return false;
        }
    }

    // 检查格式是否为32位
    char Class = 0;
    fs.read(&Class, 1);
    if (Class != 1)
    {
        return false;
    }

    // 检查大小端
    char Endianess = 0;
    fs.read(&Endianess, 1);
    if (Endianess != 1)
    {
        return false;
    }
    return true;
}

void ElfReader::ReadHeader(std::ifstream& fs, Elf32* object)
{
    fs.seekg(std::ios::beg);
    fs.read(reinterpret_cast<char*>(&object->m_header), sizeof(Elf32_Ehdr));
}

void ElfReader::ReadProgramheader(std::ifstream& fs, Elf32* object)
{
    if (object->m_header.e_phoff != 0)
    {
        fs.seekg(object->m_header.e_phoff, std::ios::beg);

        int phnum = object->m_header.e_phnum;
        object->m_program_header = std::vector<Elf32_Phdr>(phnum);
        for (int i = 0; i < phnum; ++i)
        {
            fs.read(reinterpret_cast<char*>(&object->m_program_header[i]), object->m_header.e_phentsize);
        }
    }
}

void ElfReader::ReadSectionHeader(std::ifstream& fs, Elf32* object)
{
    if (object->m_header.e_shoff != 0)
    {
        fs.seekg(object->m_header.e_shoff, std::ios::beg);

        int shnum = object->m_header.e_shnum;
        object->m_section_header = std::vector<Elf32_Shdr>(shnum);
        for (int i = 0; i < shnum; ++i)
        {
            fs.read(reinterpret_cast<char*>(&object->m_section_header[i]), object->m_header.e_shentsize);
        }
    }
}