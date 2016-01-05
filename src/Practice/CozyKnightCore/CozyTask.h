#ifndef __COZY_KNIGHT_TASK__
#define __COZY_KNIGHT_TASK__

#include "stdafx.h"
#include "CozyDef.h"
#include "iknighttask.h"

class COZY_API CozyTask : public IKnightTask
{
public:
    CozyTask(HANDLE hTarget);
    ~CozyTask(void);

    virtual void Search(int value);
    virtual void SearchRange(int min, int max);
    virtual ADDRESS_LIST GetResultAddress();

private:
    void SearcFirst(int value);
    void SearchNext(int value);

private:
    template<class T>
    class PredicateObject
    {
    public:
        PredicateObject(const T& val, HANDLE hTarget)
            :m_val(val), m_hTarget(hTarget)
        {

        }

        bool operator()(const ADDRESS_INFO& val)
        {
            T buffer = T();
            ::ReadProcessMemory(m_hTarget, val.addr, &buffer, val.size, NULL);
            return m_val == buffer;
        }

    private:
        const T&    m_val;
        HANDLE      m_hTarget;
    };

private:
    ADDRESS_LIST    m_AddrList;
    HANDLE          m_hTarget;
};

#endif // __COZY_KNIGHT_TASK__