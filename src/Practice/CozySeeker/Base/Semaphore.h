#ifndef __COZY_BASE_SEMAPHORE__
#define __COZY_BASE_SEMAPHORE__

#include "Base/CozySeekerDef.h"
#include <thread>
#include <mutex>
#include <condition_variable>

NS_BEGIN

class Semaphore
{
public:
    Semaphore(int value)
        :m_count(value)
    {

    }

    void WaitOne()
    {
        std::unique_lock<std::mutex> lock(m_mutex);
        if (--m_count < 0)
        {
            m_condition.wait(lock);
        }
    }

    void Release()
    {
        Release(1);
    }

    void Release(int n)
    {
        std::lock_guard<std::mutex> lock(m_mutex);
        m_count += n;
        if (m_count <= 0)
        {
            m_condition.notify_one();
        }
    }

private:
    int                         m_count;
    std::mutex                  m_mutex;
    std::condition_variable     m_condition;
};

NS_END

#endif // __COZY_BASE_SEMAPHORE__