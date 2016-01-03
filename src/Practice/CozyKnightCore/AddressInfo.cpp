#include "StdAfx.h"
#include "AddressInfo.h"

AddressInfo::AddressInfo(HANDLE hProcess, LPBYTE lpAddress)
    :m_hTarget(hProcess), m_lpAddress(lpAddress)
{

}

AddressInfo::~AddressInfo(void)
{

}

BOOL AddressInfo::Read(LPBYTE lpBuffer, DWORD dwSize)
{
    if(m_hTarget != NULL && m_lpAddress != NULL)
    {
        return ::ReadProcessMemory(m_hTarget, m_lpAddress, lpBuffer, dwSize, NULL);
    }
    return FALSE;
}

BOOL AddressInfo::Write(const LPBYTE lpBuffer, DWORD dwSize)
{
    if(m_hTarget != NULL && m_lpAddress != NULL)
    {
        return ::WriteProcessMemory(m_hTarget, m_lpAddress, lpBuffer, dwSize, NULL);
    }
    return FALSE;
}

LPBYTE AddressInfo::GetAddress() const
{
    return m_lpAddress;
}