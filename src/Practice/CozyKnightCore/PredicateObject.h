#pragma once

#include "stdafx.h"
#include "knightdef.h"

class PredicateObject
{
public:
    PredicateObject(LPCVOID lpData, DWORD dwSize, HANDLE hTarget);
    bool operator()(const ADDRESS_INFO& val);
    bool operator() (const LPBYTE lpData, DWORD dwSize);

private:
    LPCVOID     m_lpData;
    DWORD       m_dwSize;
    HANDLE      m_hTarget;
};