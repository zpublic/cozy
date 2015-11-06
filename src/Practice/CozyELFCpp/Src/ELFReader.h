#ifndef __COZY_ELF_READER__
#define __COZY_ELF_READER__

#include <string>
#include "ELF.h"
#include "ELFEnum.h"
#include "ELFDef.h"

class COZY_API ELFReader
{
public:
    static ELFBase* Load(const std::string& filename);

    static bool TryLoad(const std::string& filename, ELFBase*& output);

    static ELFTypeEnum CheckELFType(const std::string& filename);
};

#endif // __COZY_ELF_READER__