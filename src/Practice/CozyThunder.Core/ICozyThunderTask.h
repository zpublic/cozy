#ifndef __COZY_THUNDER_TASK__
#define __COZY_THUNDER_TASK__

#include "ICozyThunderTaskCallback.h"

namespace Cozy
{
    class COZY_API ICozyThunderTask
    {
    public:
        // state = 0 新任务 1 正在下载 2 下载暂停（旧任务加载） 3 下载完成
        virtual int GetTaskState() = 0;
        virtual const wchar_t* GetCfgPath() = 0;
        virtual const wchar_t* GetRemotePath() = 0;
        virtual const wchar_t* GetLocalPath() = 0;
        virtual unsigned long GetFileSize() = 0;
        virtual unsigned int GetBlockCount() = 0;
        // state = 0 下载开始 1 下载完成 2下载失败
        virtual int GetBlockState(unsigned int blockId) = 0;

        virtual void SetCfgPath(const wchar_t* path) = 0;
        virtual void SetRemotePath(const wchar_t* path) = 0;
        virtual void SetLocalPath(const wchar_t* path) = 0;

        virtual void SetTaskCallback(ICozyThunderTaskCallback* pCallback) = 0;
        // state 0/2 -> 1
        virtual bool Start() = 0;
        // state 1 -> 2
        virtual bool Stop() = 0;
    };
}

#endif // __COZY_THUNDER_TASK__