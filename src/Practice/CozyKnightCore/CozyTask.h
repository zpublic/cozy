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

    virtual void SearchDoubleWord(DWORD value);
    virtual void SearchWord(WORD value);
    virtual void SearchByte(BYTE value);
    virtual void SearchBytes(LPCVOID lpData, DWORD dwSize);

    virtual void SearchRange(int min, int max);
    virtual ADDRESS_LIST GetResultAddress();

private:
    void SearcFirst(LPCVOID lpData, DWORD dwSize);
    void SearchNext(LPCVOID lpData, DWORD dwSize);

private:
    class PredicateObject
    {
    public:
        PredicateObject(LPCVOID lpData, DWORD dwSize, HANDLE hTarget)
            :m_lpData(lpData), m_dwSize(dwSize), m_hTarget(hTarget)
        {

        }

        bool operator()(const ADDRESS_INFO& val)
        {
            BOOL bRetVal = FALSE;
            if(val.addr != NULL && val.size == m_dwSize)
            {
                LPBYTE lpData = new BYTE[val.size];
                if(::ReadProcessMemory(m_hTarget, val.addr, lpData, val.size, NULL))
                {
                    bRetVal = this->operator()(lpData, val.size);
                }
                delete[] lpData;
            }
            return bRetVal;
        }

        bool operator() (const LPBYTE lpData, DWORD dwSize)
        {
            if(dwSize == m_dwSize)
            {
                return !::memcmp(lpData, m_lpData, dwSize);
            }
            return FALSE;
        }

    private:
        LPCVOID     m_lpData;
        DWORD       m_dwSize;
        HANDLE      m_hTarget;
    };

private:
    ADDRESS_LIST    m_AddrList;
    HANDLE          m_hTarget;
};

#endif // __COZY_KNIGHT_TASK__