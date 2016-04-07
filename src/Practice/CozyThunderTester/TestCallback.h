#ifndef __COZY_TEST_CALLBACK__
#define __COZY_TEST_CALLBACK__

#include "../CozyThunder.Core/ICozyThunderTaskCallback.h"
#include <mutex>
#include <iostream>

class TestCallback : public Cozy::ICozyThunderTaskCallback
{
public:
    virtual void OnStart();

    // 下载停止（reason = 0 下载完成，其他为各种错误码）
    virtual void OnStop(int reason);

    // 块下载状态（blockId为1到count；state = 0 下载开始 1 下载完成 2下载失败）
    virtual void OnBlockState(unsigned int blockId, int state);

    void Wait();

private:
    std::condition_variable m_cvOK;
    std::mutex m_finishMutex;
    std::mutex m_outMutex;
};

#endif // __COZY_TEST_CALLBACK__
