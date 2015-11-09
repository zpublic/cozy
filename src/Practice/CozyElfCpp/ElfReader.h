#ifndef __COZY_ELF_READER__
#define __COZY_ELF_READER__

#include "CozyDef.h"
#include <string>
#include "ElfObject.h"

namespace CozyElf
{
    class COZY_API ElfReader
    {
    public:
        static ElfObject* Load(const std::string& filename);
        static bool TryLoad(const std::string& filename);
    };
}

#endif // __COZY_ELF_READER__