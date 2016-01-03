#ifndef __COZY_KNIGHT_DEF__
#define __COZY_KNIGHT_DEF__

#pragma warning( disable: 4251 )

#ifndef API_EXPORT
#define COZY_API __declspec(dllimport)
#else
#define COZY_API __declspec(dllexport)
#endif

#endif // __COZY_KNIGHT_DEF__