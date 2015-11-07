#include "ELfSectionHeaderObject.h"

using namespace CozyElf;

ELfSectionHeaderObject::ELfSectionHeaderObject(std::shared_ptr<Elf32_Shdr> obj, std::function<std::string(Elf32_Word)> GetNameCallback)
    :m_obj(obj),m_nameCallback(GetNameCallback)
{

}

ELfSectionHeaderObject::~ELfSectionHeaderObject()
{

}

std::string ELfSectionHeaderObject::GetName() const
{
    return m_nameCallback(m_obj->sh_name);
}