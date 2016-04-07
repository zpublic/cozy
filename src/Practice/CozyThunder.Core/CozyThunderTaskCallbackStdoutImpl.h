#ifndef __COZY_THUNDER_TASK_CALLBACK_STDOUT_IMPL__
#define __COZY_THUNDER_TASK_CALLBACK_STDOUT_IMPL__

#include "ICozyThunderTaskCallback.h"
#include <mutex>

namespace Cozy
{
    class CozyThunderTaskCallbackStdoutImpl : public ICozyThunderTaskCallback
    {
    public:
        // 开始下载
        virtual void OnStart();
        // 下载停止（reason = 0 下载完成，其他为各种错误码）
        virtual void OnStop(int reason);
        // 块下载状态（blockId为1到count；state = 0 下载开始 1 下载完成 2下载失败）
        virtual void OnBlockState(unsigned int blockId, int state);
    private:
        std::mutex m_outMutex;
    };
}

#endif // __COZY_THUNDER_TASK_CALLBACK_STDOUT_IMPL__
