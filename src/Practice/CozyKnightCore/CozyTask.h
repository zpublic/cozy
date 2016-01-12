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
    ADDRESS_LIST    m_AddrList;
    HANDLE          m_hTarget;
};

#endif // __COZY_KNIGHT_TASK__