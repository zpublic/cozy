#include "stdafx.h"
#include "CozyKnightCore.h"
#include "CozyTask.h"
#include <algorithm>

CozyKnightCore::CozyKnightCore()
    :m_hTarget(NULL)
{

}

CozyKnightCore::~CozyKnightCore()
{

}

void CozyKnightCore::Release()
{
    delete this;
}

void CozyKnightCore::Attach(HANDLE hProcess)
{
    Detach();

    m_hTarget = hProcess;
}

void CozyKnightCore::Detach()
{
    if(m_hTarget != NULL)
    {
        ::CloseHandle(m_hTarget);
        m_hTarget = NULL;
    }
}

IKnightTask* CozyKnightCore::CreateTask()
{
    IKnightTask* retVal = NULL;
    if(m_hTarget != NULL)
    {
        retVal = new CozyTask(m_hTarget);
        m_vecTaskList.push_back(retVal);
    }
    return retVal;
}

size_t CozyKnightCore::GetTaskCount()
{
    return m_vecTaskList.size();
}

IKnightTask* CozyKnightCore::GetTask(size_t index)
{
    if(index < m_vecTaskList.size())
    {
        return m_vecTaskList[index];
    }
    return NULL;
}

void CozyKnightCore::DeleteTask(IKnightTask* pTask)
{
    typedef std::vector<IKnightTask*>::iterator TaskIter;
    TaskIter iter = std::find(m_vecTaskList.begin(), m_vecTaskList.end(), pTask);

    if(iter != m_vecTaskList.end())
    {
        delete *iter;
        m_vecTaskList.erase(iter);
    }
}

void CozyKnightCore::ClearTask()
{
    typedef std::vector<IKnightTask*>::iterator TaskIter;
    for(TaskIter iter = m_vecTaskList.begin(); iter != m_vecTaskList.end(); ++iter)
    {
        delete *iter;
    }
    m_vecTaskList.clear();
}

void CozyKnightCore::SaveAddress(const ADDRESS_INFO& addr)
{
    m_SavedAddrList.push_back(addr);
}

ADDRESS_LIST CozyKnightCore::GetSavedAddress()
{
    return m_SavedAddrList;
}

void CozyKnightCore::DeleteSavedAddress(size_t index)
{
    if(index < m_SavedAddrList.size())
    {
        m_SavedAddrList.erase(m_SavedAddrList.begin() + index);
    }
}

void CozyKnightCore::ClearSavedAddress()
{
    m_SavedAddrList.clear();
}

BOOL CozyKnightCore::ModifyValue(const ADDRESS_INFO& addr, int value)
{
    if(m_hTarget != NULL)
    {
        return ::WriteProcessMemory(m_hTarget, addr.addr, &value, addr.size, NULL);
    }
    return FALSE;
}

BOOL CozyKnightCore::ReadValue(const ADDRESS_INFO& addr, int& nValue)
{
    if(m_hTarget != NULL)
    {
        return ::ReadProcessMemory(m_hTarget, addr.addr, &nValue, addr.size, NULL);
    }
    return FALSE;
}