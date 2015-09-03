#ifndef __COZY_DICT_HOOK_EXPORT__

#define COZYDICTEXPORT
#ifndef COZYDICTEXPORT
#define COZYDICTAPI _declspec(dllexport)
#else
#define COZYDICTAPI _declspec(dllexport) 
#endif


extern "C" COZYDICTAPI bool SetCBTHook();
extern "C" COZYDICTAPI bool UnSetCBTHook();



#define __COZY_DICT_HOOK_EXPORT__
#endif // __COZY_DICT_HOOK_EXPORT__
