#include "ELFReader.h"
#include <fstream>

ELFBase* ELFReader::Load(const std::string& filename)
{
    ELFBase* _result = nullptr;
    if (!TryLoad(filename, _result))
    {
        throw std::invalid_argument("File is not ELF File");
    }
    return _result;
}

bool ELFReader::TryLoad(const std::string& filename, ELFBase*& output)
{
    switch (CheckELFType(filename))
    {
    case ELFTypeEnum::X86:
        output = ELF<32>::Create(filename);
        return true;
    case ELFTypeEnum::X64:
        output = ELF<64>::Create(filename);
        return true;
    default:
        output = nullptr;
        return false;
    }
}

ELFTypeEnum ELFReader::CheckELFType(const std::string& filename)
{
    std::ifstream fs(filename, std::ios::binary);
    if (fs.is_open())
    {
        unsigned char magic_number[4];
        fs.read(reinterpret_cast<char*>(magic_number), 4 * sizeof(char));
        for (int i = 0; i < 4; ++i)
        {
            if (magic_number[i] != ELFBase::DefaultMagicnumber[i])
            {
                return ELFTypeEnum::NotELF;
            }
        }

        unsigned char elf_type = 0;
        fs.read(reinterpret_cast<char*>(&elf_type), 1 * sizeof(char));
        return elf_type == 1 ? ELFTypeEnum::X86 : ELFTypeEnum::X64;
    }
    fs.close();
    return ELFTypeEnum::NotELF;
}

