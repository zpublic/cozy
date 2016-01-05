#ifndef __COZY_KNIGHT_CORE__
#define __COZY_KNIGHT_CORE__

#include "stdafx.h"
#include "CozyDef.h"
#include "iknight.h"
#include <vector>

class COZY_API CozyKnightCore : public IKnight
{
public:
    CozyKnightCore();

    virtual void Release();

    virtual void Attach(HANDLE hProcess);
    virtual void Detach();

    virtual IKnightTask* CreateTask();
    virtual size_t GetTaskCount();
    virtual IKnightTask* GetTask(size_t index);
    virtual void DeleteTask(IKnightTask* pTask);
    virtual void ClearTask();

    virtual void SaveAddress(const ADDRESS_INFO& addr);
    virtual ADDRESS_LIST GetSavedAddress();
    virtual void DeleteSavedAddress(size_t index);
    virtual void ClearSavedAddress();

    virtual BOOL ModifyValue(const ADDRESS_INFO& addr, int value);

protected:
    ~CozyKnightCore();

private:
    std::vector<IKnightTask*>       m_vecTaskList;
    ADDRESS_LIST                    m_SavedAddrList;
    HANDLE                          m_hTarget;
};

EXTERN_C COZY_API IKnight* GetInstance()
{
    return new CozyKnightCore();
}

#endif // __COZY_KNIGHT_CORE__