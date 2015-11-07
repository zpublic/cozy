#include "ELfSectionHeaderObject.h"

using namespace CozyElf;

ELfSectionHeaderObject::ELfSectionHeaderObject(Elf32_Shdr* obj)
    :m_obj(obj)
{

}

ELfSectionHeaderObject::~ELfSectionHeaderObject()
{

}
