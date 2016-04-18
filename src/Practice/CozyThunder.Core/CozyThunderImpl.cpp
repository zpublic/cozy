#include "stdafx.h"
#include "CozyThunderImpl.h"

#include "CozyThunderTaskImpl.h"
#include <algorithm>
#include <fstream>
#include <string>

using namespace Cozy;

bool CozyThunderImpl::Initialize()
{
    return true;
}

void CozyThunderImpl::UnInitialize()
{
    std::for_each(std::begin(m_ThunderTaskList), std::end(m_ThunderTaskList), [](auto ptr) { delete ptr; });
    m_ThunderTaskList.clear();
}

ICozyThunderTask* CozyThunderImpl::CreateTask(const wchar_t* sCfgPath)
{
    auto ptr = new CozyThunderTaskImpl();
    ptr->SetCfgPath(sCfgPath);

    std::wofstream fs;
    fs.open(sCfgPath, std::ios::out | std::ios::binary);
    if (!fs.is_open()) return nullptr;

    fs.close();

    m_ThunderTaskList.push_back(ptr);
    return ptr;
}

ICozyThunderTask* CozyThunderImpl::LoadTask(const wchar_t* sCfgPath)
{
    auto pTask = new CozyThunderTaskImpl();
    pTask->SetCfgPath(sCfgPath);

    std::wifstream fs;
    fs.open(sCfgPath, std::ios::in);
    if (fs.is_open())
    {
        std::wstring buffer;
        fs >> buffer;
        pTask->SetRemotePath(buffer.c_str());

        fs >> buffer;
        pTask->SetLocalPath(buffer.c_str());

        unsigned long value = 0;
        fs >> value;
        pTask->SetFileSize(value);
      
        int v = 0;
        fs >> v;
        pTask->SetTaskState(value);

        unsigned int n = 0;
        fs >> n;
        pTask->SetBlockCount(n);

        fs >> buffer;
        pTask->SetBlockInfo(buffer);
    }

    fs.close();

    m_ThunderTaskList.push_back(pTask);
    return pTask;
}

bool CozyThunderImpl::SaveTask(ICozyThunderTask* pTask)
{
    std::wofstream fs;
    fs.open(pTask->GetCfgPath(), std::ios::out);
    if (!fs.is_open()) return nullptr;

    fs << pTask->GetRemotePath() << std::endl;
    fs << pTask->GetLocalPath() << std::endl;
    fs << pTask->GetFileSize() << std::endl;
    fs << pTask->GetTaskState() << std::endl;
    fs << pTask->GetBlockCount() << std::endl;
    for (int i = 0; i < pTask->GetBlockCount(); ++i)
    {
        fs << pTask->GetBlockState(i);
    }
    fs << std::endl;

    fs.close();

    return true;
}

// 释放task
bool CozyThunderImpl::ReleaseTask(ICozyThunderTask* pTask)
{
    auto iter = std::find(std::begin(m_ThunderTaskList), std::end(m_ThunderTaskList), pTask);
    if (iter != m_ThunderTaskList.end())
    {
        m_ThunderTaskList.erase(iter);
        delete pTask;
        return true;
    }
    return false;
}

// 清理task相关的缓存文件、配置文件，释放task，但不会清除下载完成的文件
bool CozyThunderImpl::ClearTask(ICozyThunderTask* pTask)
{
    if (pTask->GetTaskState() != 3)
    {
        ::DeleteFile(pTask->GetLocalPath());
    }
    ::DeleteFile(pTask->GetCfgPath());

    ReleaseTask(pTask);
    return true;
}

Cozy::ICozyThunder* createThunder()
{
    return new CozyThunderImpl();
}

void releaseThunder(Cozy::ICozyThunder* ptr)
{
    delete ptr;
}