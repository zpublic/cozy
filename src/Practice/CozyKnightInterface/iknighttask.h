#pragma once
#include "knightdef.h"
#include "windows.h"

class IKnightTask
{
public:
    virtual void SearchDoubleWord(DWORD value)                        = 0;
    virtual void SearchWord(WORD value)                         = 0;
    virtual void SearchByte(BYTE value)                         = 0;
    virtual void SearchBytes(LPCVOID lpData, DWORD dwSize)   = 0;

    virtual void SearchRange(int min, int max)              = 0;
    virtual ADDRESS_LIST GetResultAddress()                 = 0;
};
