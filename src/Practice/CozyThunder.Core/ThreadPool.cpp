#include "ThreadPool.h"

using namespace Cozy;

ThreadPool::ThreadPool(int minWorker, int maxWorker)
    :m_minWorker(minWorker), 
    m_maxWorker(maxWorker), 
    m_cancleFlag(true)
{

}

ThreadPool::~ThreadPool()
{
    m_cancleFlag = true;
    __ReleaseAllTask();
}

void ThreadPool::PostTask(Task task)
{
    std::lock_guard<std::mutex> lock(m_taskMutex);

    m_taskQueue.push_back(task);
    __AdjustThread();

    m_cvTask.notify_one();
}

void ThreadPool::Start()
{
    m_cancleFlag = false;
    __InitThread();
}

void ThreadPool::Stop()
{
    m_cancleFlag = true;
    __ReleaseAllTask();
}

void ThreadPool::__InitThread()
{
    std::lock_guard<std::mutex> lock(m_threadMutex);

    for (auto i = m_threadList.size(); i < m_minWorker; ++i)
    {
        m_threadList.push_back(std::thread(std::bind(&ThreadPool::__WorkerThreadProc, this)));
    }
}

void ThreadPool::__AdjustThread()
{
    std::lock_guard<std::mutex> lock(m_threadMutex);

    auto size = m_taskQueue.size();
    if (size > m_threadList.size() && size < m_maxWorker)
    {
        m_threadList.push_back(std::thread(std::bind(&ThreadPool::__WorkerThreadProc, this)));
    }
}

void ThreadPool::__WorkerThreadProc()
{
    while (!m_cancleFlag)
    {
        Task t = nullptr;

        {
            std::unique_lock<std::mutex> lock(m_taskMutex);

            m_cvTask.wait(lock, [this]()
            {
                return !m_taskQueue.empty();
            });

            if (m_cancleFlag)
            {
                break;
            }

            if (!m_taskQueue.empty())
            {
                t = m_taskQueue.front();
                m_taskQueue.pop_front();
            }
        }

        if (t != nullptr)
        {
            t();
        }
    }
}

void ThreadPool::__ReleaseAllTask()
{
    {
        std::lock_guard<std::mutex> lock(m_taskMutex);

        m_taskQueue.assign(m_maxWorker, nullptr);
        m_cvTask.notify_all();
    }

    for (auto& thread : m_threadList)
    {
        thread.join();
    }
    m_threadList.clear();
    m_taskQueue.clear();
}