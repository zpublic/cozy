#ifndef __COZY_BASE_CONCURRENT_SET__
#define __COZY_BASE_CONCURRENT_SET__

#include "Base/CozySeekerDef.h"
#include <unordered_set>
#include <thread>
#include <mutex>

NS_BEGIN

template<class T>
class ConcurrentSet
{
public:
    void Add(const T& o)
    {
        std::lock_guard<std::mutex> lock(m_mutex);
        m_set.insert(o);
    }

    void Remove(const T& o)
    {
        std::lock_guard<std::mutex> lock(m_mutex);
        auto iter = m_set.find(o);
        if (iter != m_set.end())
        {
            m_set.erase(iter);
        }
    }

    void Contains(const T& o)
    {
        std::lock_guard<std::mutex> lock(m_mutex);
        return  m_set.find(o) != m_set.end();
    }

private:
    std::unordered_set<T>   m_set;
    std::mutex              m_mutex;
}

NS_END

#endif // __COZY_BASE_CONCURRENT_SET__
