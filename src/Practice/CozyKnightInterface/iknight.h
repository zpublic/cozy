#pragma once
#include "knightdef.h"
#include "iknighttask.h"

interface IKnight
{
    virtual void Release()                                  = 0;

    virtual void Attach(HANDLE hProcess)                    = 0;
    virtual void Detach()                                   = 0;

    virtual IKnightTask CreateTask()                        = 0;
    virtual int GetTaskCount()                              = 0;
    virtual IKnightTask GetTask(int index)                  = 0;
    virtual void DeleteTask(IKnightTask* pTask)             = 0;
    virtual void ClearTask()                                = 0;

    virtual void SaveAddress(ADDRESS_INFO addr)             = 0;
    virtual ADDRESS_LIST GetSavedAddress()                  = 0;
    virtual void DeleteSavedAddress(int index)              = 0;
    virtual void ClearSavedAddress()                        = 0;

    virtual BOOL ModifyValue(ADDRESS_INFO addr, int value)  = 0;
};
