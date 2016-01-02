#ifndef __COZY_KNIGHT_ADDRESS_INFO__
#define __COZY_KNIGHT_ADDRESS_INFO__

#include "stdafx.h"
#include "CozyDef.h"

class COZY_API AddressInfo
{
public:
    AddressInfo(LPBYTE lpAddress, );
    ~AddressInfo(void);

    BOOL Read(LPBYTE lpBuffer, DWORD dwSize);
    BOOL Write(const LPBYTE lpBuffer, DWORD dwSize);
private:
    LPBYTE  m_lpAddress;
};

#endif // __COZY_KNIGHT_ADDRESS_INFO__