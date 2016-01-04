#ifndef __COZY_KNIGHT_INTERFACE__
#define __COZY_KNIGHT_INTERFACE__

#include "stdafx.h"
#include "CozyDef.h"

class COZY_API IAddressInfo
{
public:
    virtual BOOL Read(LPBYTE lpBuffer, DWORD dwSize) const = 0;
    virtual BOOL Write(const LPBYTE lpBuffer, DWORD dwSize) = 0;
    virtual LPBYTE GetAddress() const = 0;
};

class COZY_API IMemoryTester
{
public:
    virtual BOOL Test(LPBYTE lpData, DWORD dwSize) const = 0;
};

class COZY_API IAddressTester : IMemoryTester
{
public:
    virtual BOOL Test(const IAddressInfo* info) const = 0;
};

class COZY_API ITask
{
public:
    virtual void AddAddress(IAddressInfo* addr) = 0;
    virtual void Clear() = 0;
    virtual void ApplyFilter(const IAddressTester& tester) = 0;
    virtual size_t GetLength() const = 0;
    virtual const IAddressInfo* GetData() const = 0;
};

class COZY_API ISearcher
{
public:
    virtual BOOL SearchFirst(const IMemoryTester& tester, DWORD dwSize, ITask& taskResult) = 0;
    virtual BOOL Search(ITask& TaskSource, const IAddressTester& tester) = 0;
};

#endif // __COZY_KNIGHT_INTERFACE__