#include "PredicateObject.h"

PredicateObject::PredicateObject(LPCVOID lpData, DWORD dwSize, HANDLE hTarget)
:m_lpData(lpData), m_dwSize(dwSize), m_hTarget(hTarget)
{

}

bool PredicateObject::operator()(const ADDRESS_INFO& val)
{
    BOOL bRetVal = FALSE;
    if(val.addr != NULL && val.size == m_dwSize)
    {
        LPBYTE lpData = new BYTE[val.size];
        if(::ReadProcessMemory(m_hTarget, val.addr, lpData, val.size, NULL))
        {
            bRetVal = this->operator()(lpData, val.size);
        }
        delete[] lpData;
    }
    return bRetVal;
}

bool PredicateObject::operator() (const LPBYTE lpData, DWORD dwSize)
{
    if(dwSize == m_dwSize)
    {
        return !::memcmp(lpData, m_lpData, dwSize);
    }
    return FALSE;
}
