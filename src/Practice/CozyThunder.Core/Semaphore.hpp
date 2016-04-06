#ifndef __COZY_SEMAPHORE__
#define __COZY_SEMAPHORE__

#include <mutex>
#include <condition_variable>
#include <chrono>

namespace Cozy
{
    class Semaphore
    {
    public:
        Semaphore(int value)
            :m_count(value)
        {

        }

        void Wait()
        {
            std::unique_lock<std::mutex> lock(m_mutex);
            if (--m_count < 0)
            {
                m_condition.wait(lock);
            }
        }

        std::cv_status Wait(int time)
        {
            std::unique_lock<std::mutex> lock(m_mutex);
            if (--m_count < 0)
            {
                return m_condition.wait_for(lock, std::chrono::milliseconds(time));
            }
            return std::cv_status::no_timeout;
        }

        void ReleaseAll()
        {
            std::unique_lock<std::mutex> lock(m_mutex);
            m_count = 0;
            m_condition.notify_all();
        }

        void Release(int n)
        {
            std::unique_lock<std::mutex> lock(m_mutex);
            if (++m_count <= 0)
            {
                m_condition.notify_one();
            }
        }

    private:
        std::mutex m_mutex;
        std::condition_variable m_condition;
        int m_count;
    };
}

#endif // __COZY_SEMAPHORE__
