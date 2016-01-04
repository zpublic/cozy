#ifndef __COZY_MEMORY_TESTER__
#define __COZY_MEMORY_TESTER__

#include "stdafx.h"
#include "CozyInterface.h"

class COZY_API MemoryTester : public IProcessMemoryTester
{
public:
    MemoryTester(LPBYTE lpData, DWORD dwSize);

    // IProcessMemoryTester
    virtual BOOL TestMem(LPBYTE lpData) const;
    virtual BOOL TestProcMem(const AddressInfo& info) const;

private:
    LPBYTE  m_lpData;
    DWORD   m_dwSize;
};

#endif // __COZY_MEMORY_TESTER__