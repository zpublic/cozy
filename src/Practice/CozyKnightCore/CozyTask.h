#ifndef __COZY_KNIGHT_TASK__
#define __COZY_KNIGHT_TASK__

#include "stdafx.h"
#include "CozyInterface.h"
#include <vector>

class AddressInfo;

class COZY_API CozyTask : public ITask
{
public:
    CozyTask(void);
    ~CozyTask(void);

    virtual void AddAddress(const AddressInfo& addr);
    virtual void Clear();
    virtual void ApplyFilter(const IProcessMemoryTester& tester);
    virtual size_t GetLength() const;
    virtual const AddressInfo* GetData() const;

private:
    std::vector<AddressInfo> m_vecAddrList;
};

#endif // __COZY_KNIGHT_TASK__