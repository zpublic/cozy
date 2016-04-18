#ifndef __COZY_TREADER_POOL__
#define __COZY_TREADER_POOL__

#include <functional>
#include <thread>
#include <atomic>
#include <deque>
#include <vector>
#include <condition_variable>

namespace Cozy
{
    class ThreadPool
    {
    public:
        using Task = std::function<void(void)>;
        using TaskQueue = std::deque<Task>;
        using ThreadList = std::vector<std::thread>;

    public:
        ThreadPool(int minWorker, int maxWorker);
        ~ThreadPool();

        void PostTask(Task task);

        void Start();
        void Stop();

    private:
        void __InitThread();
        void __AdjustThread();
        void __WorkerThreadProc();
        void __ReleaseAllTask();

    private:
        TaskQueue                   m_taskQueue;
        ThreadList                  m_threadList;
        std::condition_variable     m_cvTask;
        std::mutex                  m_taskMutex;
        std::mutex                  m_threadMutex;
        int                         m_minWorker;
        int                         m_maxWorker;
        std::atomic<bool>           m_cancleFlag;
    };
}

#endif // __COZY_TREADER_POOL__
