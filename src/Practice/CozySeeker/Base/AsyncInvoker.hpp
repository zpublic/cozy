#ifndef _COZY_BASE_ASYNC_INVOKER__
#define _COZY_BASE_ASYNC_INVOKER__

#include "Base/CozySeekerDef.h"
#include "Base/Semaphore.h"
#include "Base/ConcurrentQueue.hpp"
#include <thread>
#include <vector>
#include <atomic>
#include <condition_variable>

NS_BEGIN

template<class T>
class AsyncInvoker
{
public:
    typedef std::function<void(const T&)> InvokerCallback;

public:
    AsyncInvoker()
        :AsyncInvoker(1)
    {

    }

    AsyncInvoker(int maxInvoker)
        :m_maxInvoker(maxInvoker), m_semaphore(0), m_cancelFlag(true)
    {

    }

    ~AsyncInvoker()
    {
        if (IsRunning())
        {
            Stop();
        }
    }

public:
    void Start()
    {
        m_isRunning     = true;
        m_cancelFlag    = false;
        InitInvoker();
    }

    void Stop()
    {
        Calcel();
        m_semaphore.Release(m_maxInvoker);
        WaitInvoker();
        m_msgQueue.Clear();
        m_isRunning = false;
    }

    void Wait()
    {

    }

    bool IsRunning() const
    {
        return m_isRunning;
    }

public:
    void Add(const T& value)
    {
        m_msgQueue.Push(value);
        m_semaphore.Release();
    }

    void SetCallback(InvokerCallback callback)
    {
        m_invokeCallback = callback;
    }

protected:
    void Calcel()
    {
        m_cancelFlag = true;
    }

    void TaskProc()
    {
        while (!m_cancelFlag)
        {
            m_semaphore.WaitOne();

            if (m_cancelFlag)
            {
                break;
            }

            T t = T();
            if (m_msgQueue.TryPop(t))
            {
                if (m_invokeCallback != nullptr)
                {
                    m_invokeCallback(t);
                }
            }
        }
    }

    void InitInvoker()
    {
        for (int i = 0; i < m_maxInvoker; ++i)
        {
            m_threadPool.push_back(std::thread(std::bind(&AsyncInvoker::TaskProc, this)));
        }
    }

    void WaitInvoker()
    {
        for (int i = 0; i < m_maxInvoker; ++i)
        {
            m_threadPool[i].join();
        }
    }

private:
    ConcurrentQueue<T>          m_msgQueue;
    std::vector<std::thread>    m_threadPool;
    Cozy::Semaphore             m_semaphore;
    InvokerCallback             m_invokeCallback;
    int                         m_maxInvoker;
    std::atomic<bool>           m_cancelFlag;
    std::atomic<bool>           m_isRunning;
};

NS_END

#endif // _COZY_BASE_ASYNC_INVOKER__
