#ifndef __COZY_SEEKER_DEF__
#define __COZY_SEEKER_DEF__

#ifndef COZY_EXPORT
#define COZY_API _declspec(dllimport)
#else
#define COZY_API _declspec(dllexport)
#endif

#define NS_BEGIN namespace Cozy {

#define NS_END }

#endif // __COZY_SEEKER_DEF__
