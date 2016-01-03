#ifndef __COZY_KNIGHT_TASK__
#define __COZY_KNIGHT_TASK__

#include "stdafx.h"
#include "CozyDef.h"
#include "AddressInfo.h"
#include "MemoryTester.h"

class COZY_API CozyTask
{
public:
    CozyTask(void);
    ~CozyTask(void);

    void AddAddress(const AddressInfo& addr);
    void Clear();
    void ApplyFilter(const MemoryTester& tester);
    size_t GetLength() const;
    const AddressInfo* GetData() const;

private:
    std::vector<AddressInfo> m_vecAddrList;
};

#endif // __COZY_KNIGHT_TASK__