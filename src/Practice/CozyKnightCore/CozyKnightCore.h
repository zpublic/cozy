#ifndef __COZY_KNIGHT_CORE__
#define __COZY_KNIGHT_CORE__

#include "stdafx.h"
#include "CozyDef.h"
#include "AddressInfo.h"
#include "MemoryTester.h"
#include "CozyTask.h"


class COZY_API CozyKnightCore
{
public:
    CozyKnightCore();
    ~CozyKnightCore();

    BOOL SearchFirst(const MemoryTester& tester, DWORD dwSize, CozyTask& taskResult);

    BOOL Search(CozyTask& TaskSource, const MemoryTester& tester);

    void Attch(HANDLE hProcess);
    void Detch();

private:
    HANDLE m_hTarget;
};

#endif // __COZY_KNIGHT_CORE__