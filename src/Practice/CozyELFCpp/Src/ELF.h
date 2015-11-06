#ifndef __COZY_ELF__
#define __COZY_ELF__

#include <string>
#include <array>
#include "ELFDef.h"

//////////////////////////////////////////////////////////////////////////
// ELF Base
//////////////////////////////////////////////////////////////////////////
class COZY_API ELFBase
{
    int m_ELFType;

public:
    typedef ELFDefBase::ELF_HALF ELF_HALF;
    typedef ELFDefBase::ELF_WORD ELF_WORD;
    typedef ELFDefBase::ELF_SWORD ELF_SWORD;

public:
    static unsigned char DefaultMagicnumber[4];

public:
    ELFBase(int ELFtype)
    {
        m_ELFType = ELFtype;
    }

public:
    GETTER(ELFType);
};

//////////////////////////////////////////////////////////////////////////
// ELF
//////////////////////////////////////////////////////////////////////////
template<ELFDefBase::ELF_HALF N>
class COZY_API ELF : public ELFBase
{
public:
    typedef ELFBase::ELF_HALF ELF_HALF;
    typedef ELFBase::ELF_WORD ELF_WORD;
    typedef ELFBase::ELF_SWORD ELF_SWORD;
    typedef typename ELFDef<N>::ELF_OFF ELF_OFF;
    typedef typename ELFDef<N>::ELF_ADDR ELF_ADDR;

private:
    ELF_HALF m_Type;
    ELF_HALF m_Machine;
    ELF_WORD m_Version;
    ELF_ADDR m_Entry;
    ELF_OFF m_Phoff;
    ELF_OFF m_Shoff;
    ELF_WORD m_Flags;
    ELF_HALF m_EhSize;
    ELF_HALF m_PhentSize;
    ELF_HALF m_PhNum;
    ELF_HALF m_ShentSize;
    ELF_HALF m_ShNum;
    ELF_HALF m_ShStrndx;

private:
    ELF(const std::string& filename) :ELFBase(N) {}

public:
    static ELF<N>* Create(const std::string& filename)
    {
        return nullptr;
    }

public: // Getter and Setter

    GETTER(Type);
    GETTER(Machine);
    GETTER(Version);
    GETTER(Entry);
    GETTER(Phoff);
    GETTER(Shoff);
    GETTER(Flags);
    GETTER(EhSize);
    GETTER(PhentSize);
    GETTER(PhNum);
    GETTER(ShentSize);
    GETTER(ShNum);
    GETTER(ShStrndx);

    SETTER(Type);
    SETTER(Machine);
    SETTER(Version);
    SETTER(Entry);
    SETTER(Phoff);
    SETTER(Shoff);
    SETTER(Flags);
    SETTER(EhSize);
    SETTER(PhentSize);
    SETTER(PhNum);
    SETTER(ShentSize);
    SETTER(ShNum);
    SETTER(ShStrndx);
};

#endif // __COZY_ELF__