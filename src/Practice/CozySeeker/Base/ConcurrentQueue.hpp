#ifndef __COZY_BASE_CONCURRENT_QUEUE__
#define __COZY_BASE_CONCURRENT_QUEUE__

#include "Base/CozySeekerDef.h"
#include <queue>
#include <thread>
#include <mutex>

NS_BEGIN

template<class T>
class ConcurrentQueue
{
public:
    void Push(const T& o)
    {
        std::lock_guard<std::mutex> lock(m_mutex);
        m_queue.push(o);
    }

    bool TryPop(T& out)
    {
        std::lock_guard<std::mutex> lock(m_mutex);
        if (!m_queue.empty())
        {
            out = m_queue.front();
            m_queue.pop();
            return true;
        }
        return false;
    }

    bool TryPeek(T& out)
    {
        std::lock_guard<std::mutex> lock(m_mutex);
        if (!m_queue.empty())
        {
            out = m_queue.top();
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

private:
    std::queue<T>   m_queue;
    std::mutex      m_mutex;
};

NS_END

#endif // __COZY_BASE_CONCURRENT_QUEUE__
