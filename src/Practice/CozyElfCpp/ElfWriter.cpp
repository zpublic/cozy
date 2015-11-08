#include "ElfWriter.h"
#include "ElfObject.h"
#include <fstream>
using namespace CozyElf;

void ElfWriter::Write(ElfObject* obj, const std::string& filename)
{
    if (obj != nullptr)
    {
        std::ofstream ofs(filename, std::ios::binary);
        if (ofs.is_open())
        {
            size_t length = obj->ToRawData(nullptr);
            unsigned char* data = new unsigned char(length);
            ofs.write(reinterpret_cast<char*>(data), length);
            delete[] data;
        }
    }
}