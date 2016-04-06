#ifndef __COZY_THUNDER_IMPL__
#define __COZY_THUNDER_IMPL__

#include "ICozyThunder.h"
#include <vector>
#include "ThreadPool.h"

namespace Cozy
{
    class CozyThunderImpl : public ICozyThunder
    {
    public:
        virtual bool Initialize();
        virtual void UnInitialize();

        virtual ICozyThunderTask* CreateTask(const wchar_t* sCfgPath);
        virtual ICozyThunderTask* LoadTask(const wchar_t* sCfgPath);
        virtual bool SaveTask(ICozyThunderTask* pTask);

        // 释放task
        virtual bool ReleaseTask(ICozyThunderTask* pTask);
        // 清理task相关的缓存文件、配置文件，释放task，但不会清除下载完成的文件
        virtual bool ClearTask(ICozyThunderTask* pTask);

    private:
        std::vector<ICozyThunderTask*>  m_ThunderTaskList;
    };
}

#endif // __COZY_THUNDER_IMPL__