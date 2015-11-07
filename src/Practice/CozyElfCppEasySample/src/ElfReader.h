#ifndef __COZY_ELF_READER__
#define __COZY_ELF_READER__

#include "ElfStructs.h"
#include <string>

namespace CozyElf
{
    class COZY_API ElfReader
    {
    public:
        ElfReader();
        ~ElfReader();

        Elf32* Load(const std::string& filename);

    private:
        bool TryLoad(std::ifstream& fs);

        void ReadHeader(std::ifstream& fs, Elf32* object);
        void ReadProgramheader(std::ifstream& fs, Elf32* object);
        void ReadSectionHeader(std::ifstream& fs, Elf32* object);
    };
}

#endif // __COZY_ELF_READER__