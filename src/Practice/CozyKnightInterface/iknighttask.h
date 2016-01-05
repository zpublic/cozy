#pragma once
#include "knightdef.h"

class IKnightTask
{
public:
    virtual void Search(int value)                  = 0;
    virtual void SearchRange(int min, int max)      = 0;
    virtual ADDRESS_LIST GetResultAddress()         = 0;
};
