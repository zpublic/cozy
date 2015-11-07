#include "ElfReader.h"
#include <fstream>
using namespace CozyElf;

ElfReader::ElfReader()
{

}

ElfReader::~ElfReader()
{

}

std::shared_ptr<Elf32> ElfReader::Load(const std::string& filename)
{
    std::ifstream fs(filename, std::ios::binary);
    if (fs.is_open())
    {
        if (TryLoad(fs))
        {
            auto ptr = std::make_shared<Elf32>();
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

void ElfReader::ReadHeader(std::ifstream& fs, std::shared_ptr<Elf32> object)
{
    fs.seekg(std::ios::beg);
    fs.read(reinterpret_cast<char*>(&object->m_header), sizeof(Elf32_Ehdr));
}

void ElfReader::ReadProgramheader(std::ifstream& fs, std::shared_ptr<Elf32> object)
{
    if (object->m_header.e_phoff != 0)
    {
        fs.seekg(object->m_header.e_phoff, std::ios::beg);

        int phnum = object->m_header.e_phnum;
        object->m_program_header = std::vector<std::shared_ptr<Elf32_Phdr>>();
        object->m_program_header.reserve(phnum);

        for (int i = 0; i < phnum; ++i)
        {
            auto item = std::make_shared<Elf32_Phdr>();
            fs.read(reinterpret_cast<char*>(item.get()), object->m_header.e_phentsize);
            object->m_program_header.push_back(item);
        }
    }
}

void ElfReader::ReadSectionHeader(std::ifstream& fs, std::shared_ptr<Elf32> object)
{
    if (object->m_header.e_shoff != 0)
    {
        fs.seekg(object->m_header.e_shoff, std::ios::beg);

        int shnum = object->m_header.e_shnum;
        object->m_section_header = std::vector<std::shared_ptr<Elf32_Shdr>>();
        object->m_section_header.reserve(shnum);

        for (int i = 0; i < shnum; ++i)
        {
            auto item = std::make_shared<Elf32_Shdr>();
            fs.read(reinterpret_cast<char*>(item.get()), object->m_header.e_shentsize);
            object->m_section_header.push_back(item);
        }
    }
}