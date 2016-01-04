#ifndef __COZY_KNIGHT_CORE__
#define __COZY_KNIGHT_CORE__

#include "stdafx.h"
#include "CozyInterface.h"
#include "AddressInfo.h"

class COZY_API CozyKnightCore : public ISearcher
{
public:
    CozyKnightCore();
    ~CozyKnightCore();

    void Attch(HANDLE hProcess);
    void Detch();

    // ISearcher
    virtual BOOL SearchFirst(const IMemoryTester& tester, DWORD dwSize, ITask& taskResult);
    virtual BOOL Search(ITask& taskSource, const IProcessMemoryTester& tester);

private:
    HANDLE m_hTarget;
};

#endif // __COZY_KNIGHT_CORE__