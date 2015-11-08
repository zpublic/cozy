#ifndef __COZY_ELF_WRITER__
#define __COZY_ELF_WRITER__

#include "CozyDef.h"
#include <string>

namespace CozyElf
{
    class ElfObject;

    class COZY_API ElfWriter
    {
    public:
        void Write(ElfObject* obj, const std::string& filename);
    };
}

#endif // __COZY_ELF_WRITER__