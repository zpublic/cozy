#include "ElfReader.h"
#include <fstream>

unsigned char COZY_API DefaultMagicNumber[] = { 0x7f, 0x45, 0x4c, 0x46 };

using namespace CozyElf;

ElfObject* ElfReader::Load(const std::string& filename)
{
    if (TryLoad(filename))
    {
        std::ifstream fs(filename, std::ios::binary);
        if (fs.is_open())
        {
            fs.seekg(0, std::ios::end);
            auto length = static_cast<size_t>(fs.tellg());
            fs.seekg(0, std::ios::beg);

            auto data       = new unsigned char[length];
            fs.read(reinterpret_cast<char*>(data), length);
            auto elfObj     = new ElfObject(data, length);
            delete[] data;
            return elfObj;
        }
    }
    return nullptr;
}

bool ElfReader::TryLoad(const std::string& filename)
{
    std::ifstream fs(filename, std::ios::binary);
    if (fs.is_open())
    {
        unsigned char magic_num[4];
        fs.read(reinterpret_cast<char*>(magic_num), 4);
        for (int i = 0; i < 4; ++i)
        {
            if (magic_num[i] != DefaultMagicNumber[i])
            {
                return false;
            }
        }

        unsigned char tag = 0;
        fs.read(reinterpret_cast<char*>(&tag), 1);
        if (tag != 1)
        {
            return false;
        }

        fs.read(reinterpret_cast<char*>(&tag), 1);
        if (tag != 1)
        {
            return false;
        }
        return true;
    }
    return false;
}