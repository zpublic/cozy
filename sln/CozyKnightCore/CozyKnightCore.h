#ifndef __COZY_KNIGHT_CORE__
#define __COZY_KNIGHT_CORE__

#include "stdafx.h"
#include "CozyDef.h"
#include "AddressInfo.h"
#include <vector>

class MemoryTester
{
public:
    MemoryTester(LPBYTE lpData, DWORD dwSize)
        :m_lpData(lpData), m_dwSize(dwSize)
    {

    }

    BOOL operator()(const AddressInfo info) const
    {
        if(m_lpData != NULL && info.GetAddress() != NULL && m_dwSize > 0)
        {
            return !::memcmp(m_lpData, info.GetAddress(), m_dwSize);
        }
        return false;
    }
private:
    LPBYTE  m_lpData;
    DWORD   m_dwSize;
};

class COZY_API CozyKnightCore
{
public:
    CozyKnightCore();
    ~CozyKnightCore();

    BOOL SearchFirst(HANDLE hProcess, const MemoryTester lpTester, DWORD dwSize, std::vector<AddressInfo>& vecResult);

    BOOL Search(std::vector<AddressInfo>& vecSource, const MemoryTester lpTester);

    void Attch(HANDLE hProcess);
    void Detch();
private:
    HANDLE m_hTarget;
};

#endif // __COZY_KNIGHT_CORE__