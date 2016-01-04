#ifndef __COZY_KNIGHT_ADDRESS_INFO__
#define __COZY_KNIGHT_ADDRESS_INFO__

#include "stdafx.h"
#include "CozyDef.h"

class COZY_API AddressInfo
{
public:
    AddressInfo(HANDLE hTarget, LPBYTE lpAddress);
    ~AddressInfo(void);

    LPBYTE GetAddress() const;
    HANDLE GetTarget() const;

private:
    LPBYTE  m_lpAddress;
    HANDLE  m_hTarget;
};

#endif // __COZY_KNIGHT_ADDRESS_INFO__