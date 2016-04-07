#ifndef __COZY_THUNDER_DEF__
#define __COZY_THUNDER_DEF__

#ifdef COZY_API_EXPORT 
#define COZY_API __declspec(dllexport)
#else
#define COZY_API __declspec(dllimport)
#endif // COZY_API_EXPORT

namespace Cozy
{
    static const int BlockStatusStart   = 0;
    static const int BlockStatusFinish  = 1;
    static const int BlockStatusFailed  = 2;
}

#endif // __COZY_THUNDER_DEF__
