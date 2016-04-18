#ifndef __COZY_THUNDER__
#define __COZY_THUNDER__

#include "ICozyThunderTask.h"

namespace Cozy
{
    class COZY_API ICozyThunder
    {
    public:
        virtual bool Initialize() = 0;
        virtual void UnInitialize() = 0;

        virtual ICozyThunderTask* CreateTask(const wchar_t* sCfgPath) = 0;
        virtual ICozyThunderTask* LoadTask(const wchar_t* sCfgPath) = 0;
        virtual bool SaveTask(ICozyThunderTask* pTask) = 0;

        // 释放task
        virtual bool ReleaseTask(ICozyThunderTask* pTask) = 0;
        // 清理task相关的缓存文件、配置文件，释放task，但不会清除下载完成的文件
        virtual bool ClearTask(ICozyThunderTask* pTask) = 0;
    };
}

extern "C"
{
    COZY_API Cozy::ICozyThunder* createThunder();
    COZY_API void releaseThunder(Cozy::ICozyThunder* ptr);
}

#endif // __COZY_THUNDER__