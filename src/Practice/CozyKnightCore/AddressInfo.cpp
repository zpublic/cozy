#include "StdAfx.h"
#include "AddressInfo.h"

AddressInfo::AddressInfo(HANDLE hTarget, LPBYTE lpAddress)
    :m_hTarget(hTarget), m_lpAddress(lpAddress)
{

}

AddressInfo::~AddressInfo(void)
{

}

LPBYTE AddressInfo::GetAddress() const
{
    return m_lpAddress;
}

HANDLE AddressInfo::GetTarget() const
{
    return m_hTarget;
}