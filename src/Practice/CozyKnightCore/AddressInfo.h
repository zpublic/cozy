#ifndef __COZY_KNIGHT_ADDRESS_INFO__
#define __COZY_KNIGHT_ADDRESS_INFO__

#include "stdafx.h"
#include "CozyDef.h"

class COZY_API AddressInfo
{
public:
    AddressInfo(HANDLE hProcess, LPBYTE lpAddress);
    ~AddressInfo(void);

    BOOL Read(LPBYTE lpBuffer, DWORD dwSize) const;
    BOOL Write(const LPBYTE lpBuffer, DWORD dwSize);

    LPBYTE GetAddress() const;
private:
    LPBYTE  m_lpAddress;
    HANDLE  m_hTarget;
};

#endif // __COZY_KNIGHT_ADDRESS_INFO__