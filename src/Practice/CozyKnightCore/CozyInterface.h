#ifndef __COZY_KNIGHT_INTERFACE__
#define __COZY_KNIGHT_INTERFACE__

#include "stdafx.h"
#include "CozyDef.h"

class AddressInfo;

class COZY_API IMemoryTester
{
public:
    virtual BOOL TestMem(LPBYTE lpData) const = 0;
};

class COZY_API IProcessMemoryTester : public IMemoryTester
{
public:
    virtual BOOL TestProcMem(const AddressInfo& info) const = 0;
};

class COZY_API ITask
{
public:
    virtual void AddAddress(const AddressInfo& addr) = 0;
    virtual void Clear() = 0;
    virtual void ApplyFilter(const IProcessMemoryTester& tester) = 0;
    virtual size_t GetLength() const = 0;
    virtual const AddressInfo* GetData() const = 0;
};

class COZY_API ISearcher
{
public:
    virtual BOOL SearchFirst(const IMemoryTester& tester, DWORD dwSize, ITask& taskResult) = 0;
    virtual BOOL Search(ITask& taskSource, const IProcessMemoryTester& tester) = 0;
};

template<class T>
class CozyTesterWrapper
{
public:
    CozyTesterWrapper(const T& tester)
        :m_tester(tester)
    {

    }

    BOOL operator()(const AddressInfo& info) const
    {
        return m_tester.TestProcMem(info);
    }
private:
    const T& m_tester;
};

#endif // __COZY_KNIGHT_INTERFACE__