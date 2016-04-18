#include "TestCallback.h"

void TestCallback::OnStart()
{
    std::lock_guard<std::mutex> lock(m_outMutex);
    std::cout << "task start" << std::endl;
}

// 下载停止（reason = 0 下载完成，其他为各种错误码）
void TestCallback::OnStop(int reason)
{
    std::lock_guard<std::mutex> lock(m_outMutex);
    std::cout << "task end reason :" << reason << std::endl;
    m_cvOK.notify_one();
}
// 块下载状态（blockId为1到count；state = 0 下载开始 1 下载完成 2下载失败）
void TestCallback::OnBlockState(unsigned int blockId, int state)
{
    std::unique_lock<std::mutex> lock(m_outMutex);
    std::cout << "block : " << blockId << " status : ";
    if (state == 0)
    {
        std::cout << "begin";
    }
    else if (state == 1)
    {
        std::cout << "finish";
    }
    else if (state == 2)
    {
        std::cout << "failed";
    }
    else
    {
        std::cout << "undefined error";
    }
    std::cout << std::endl;
}

void TestCallback::Wait()
{
    std::unique_lock<std::mutex> lock(m_finishMutex);
    m_cvOK.wait(lock);
}
