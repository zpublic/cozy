#ifndef __COZY_TESTER_12306TESTER__
#define __COZY_TESTER_12306TESTER__

#include "Base/CozyInterface.h"

NS_BEGIN

class Tester12306 : public ISeekerTester
{
public:
    Tester12306(int times);

    virtual void Test();

private:
    StrPtr MakeUrl();

    int m_times;
};

NS_END

#endif // __COZY_TESTER_12306TESTER__
