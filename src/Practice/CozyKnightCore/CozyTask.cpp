#include "StdAfx.h"
#include "CozyTask.h"
#include "AddressInfo.h"
#include "MemoryTester.h"
#include <algorithm>

CozyTask::CozyTask(void)
{

}

CozyTask::~CozyTask(void)
{

}

void CozyTask::AddAddress(const AddressInfo& addr)
{
    m_vecAddrList.push_back(addr);
}

void CozyTask::Clear()
{
    m_vecAddrList.clear();
}

void CozyTask::ApplyFilter(const IProcessMemoryTester& tester)
{
    CozyTesterWrapper<IProcessMemoryTester> wrapper(tester);
    m_vecAddrList.erase(std::remove_if(m_vecAddrList.begin(), m_vecAddrList.end(), wrapper), m_vecAddrList.end());
}

size_t CozyTask::GetLength() const
{
    return m_vecAddrList.size();
}

const AddressInfo* CozyTask::GetData() const
{
    return &m_vecAddrList[0];
}