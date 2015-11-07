#include "ElfProgramHeaderObject.h"

using namespace CozyElf;

ElfProgramHeaderObject::ElfProgramHeaderObject(Elf32_Phdr* obj)
    :m_obj(obj)
{

}

ElfProgramHeaderObject::~ElfProgramHeaderObject()
{

}
