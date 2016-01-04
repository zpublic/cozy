#include "MemoryTester.h"
#include <vector>
#include "AddressInfo.h"

MemoryTester::MemoryTester(LPBYTE lpData, DWORD dwSize)
:m_lpData(lpData), m_dwSize(dwSize)
{

}

BOOL MemoryTester::TestMem(LPBYTE lpData) const
{
    if(m_lpData != NULL && lpData != NULL && m_dwSize > 0)
    {
        return !::memcmp(m_lpData, lpData, m_dwSize);
    }
    return FALSE;
}

BOOL MemoryTester::TestProcMem(const AddressInfo& info) const
{
    if(m_lpData != NULL && info.GetAddress() != NULL && m_dwSize > 0)
    {
        std::vector<BYTE> vecBuffer;
        vecBuffer.resize(m_dwSize);
        ::ReadProcessMemory(info.GetTarget(), info.GetAddress(), &vecBuffer[0], m_dwSize, NULL);

        return this->TestMem(&vecBuffer[0]);
    }
    return FALSE;
}