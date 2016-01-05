#pragma once
#include "knightdef.h"
#include "iknighttask.h"
#include "windows.h"

class IKnight
{
public:
    virtual void Release()                                  = 0;

    virtual void Attach(HANDLE hProcess)                    = 0;
    virtual void Detach()                                   = 0;

    virtual IKnightTask* CreateTask()                       = 0;
    virtual size_t GetTaskCount()                           = 0;
    virtual IKnightTask* GetTask(size_t index)              = 0;
    virtual void DeleteTask(IKnightTask* pTask)             = 0;
    virtual void ClearTask()                                = 0;

    virtual void SaveAddress(const ADDRESS_INFO& addr)      = 0;
    virtual ADDRESS_LIST GetSavedAddress()                  = 0;
    virtual void DeleteSavedAddress(size_t index)           = 0;
    virtual void ClearSavedAddress()                        = 0;

    virtual BOOL ModifyValue(const ADDRESS_INFO& addr, int value)  = 0;
};
