#ifndef __COZY_THUNDER_TASK_CALLBACK__
#define __COZY_THUNDER_TASK_CALLBACK__

#include "CozyThunderDef.h"

namespace Cozy
{
    class COZY_API ICozyThunderTaskCallback
    {
    public:
        // 开始下载
        virtual void OnStart() = 0;
        // 下载停止（reason = 0 下载完成，其他为各种错误码）
        virtual void OnStop(int reason) = 0;
        // 块下载状态（blockId为1到count；state = 0 下载开始 1 下载完成 2下载失败）
        virtual void OnBlockState(unsigned int blockId, int state) = 0;
    };
}

extern "C"
{
    COZY_API Cozy::ICozyThunderTaskCallback* createCallback();
    COZY_API void releaseCallback(Cozy::ICozyThunderTaskCallback* ptr);
}


#endif // __COZY_THUNDER_TASK_CALLBACK__