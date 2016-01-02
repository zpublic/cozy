#ifndef __COZY_KNIGHT_CORE__
#define __COZY_KNIGHT_CORE__

#include "stdafx.h"
#include "CozyDef.h"

class COZY_API CozyKnightCore
{
public:
    typedef void (CALLBACK *EnumCallback)(LPBYTE lpData);
public:
    BOOL EnumMem(DWORD dwPid, EnumCallback lpfnCallback, DWORD dwSize);
};

#endif // __COZY_KNIGHT_CORE__