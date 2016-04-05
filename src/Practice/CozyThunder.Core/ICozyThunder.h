#pragma once
#include "ICozyThunderTask.h"

class ICozyThunder
{
    virtual bool Initialize()                                       = 0;
    virtual void UnInitialize()                                     = 0;

    virtual ICozyThunderTask CreateTask(const wchar_t* sCfgPath)    = 0;
    virtual ICozyThunderTask LoadTask(const wchar_t* sCfgPath)      = 0;
    virtual bool SaveTask(ICozyThunderTask* pTask)                  = 0;

    // 释放task
    virtual bool ReleaseTask(ICozyThunderTask* pTask)               = 0;
    // 清理task相关的缓存文件、配置文件，释放task，但不会清除下载完成的文件
    virtual bool ClearTask(ICozyThunderTask* pTask)                 = 0;
};
