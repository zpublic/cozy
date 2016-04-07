#ifndef __COZY_CURRENT_QUEUE__
#define __COZY_CURRENT_QUEUE__

#include <queue>
#include <thread>
#include <mutex>

namespace Cozy
{
    template<class T>
    class CurrentQueue
    {
    public:
        void Push(const T& value)
        {
            std::lock_guard<std::mutex> lock(m_mutex);
            m_queue.push(value);
        }

        bool TryPop(T& value)
        {
            std::lock_guard<std::mutex> lock(m_mutex);
            if (!m_queue.empty())
            {
                value = m_queue.front();
                m_queue.pop();
                return true;
            }
            return false;
        }

        void Clear()
        {
            std::lock_guard<std::mutex> lock(m_mutex);
            while (!m_queue.empty())
            {
                m_queue.pop();
            }
        }

        std::size_t Count()
        {
            std::lock_guard<std::mutex> lock(m_mutex);
            return m_queue.size();
        }

    private:
        std::queue<T>   m_queue;
        std::mutex      m_mutex;
    };
}


#endif // __COZY_CURRENT_QUEUE__