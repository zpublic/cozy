// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the FILEUTILCPP_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// FILEUTILCPP_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef FILEUTILCPP_EXPORTS
#define FILEUTILCPP_API __declspec(dllexport)
#else
#define FILEUTILCPP_API __declspec(dllimport)
#endif

// This class is exported from the FileUtilCpp.dll
class FILEUTILCPP_API CFileUtilCpp {
public:
	CFileUtilCpp(void);
	// TODO: add your methods here.
};

extern FILEUTILCPP_API int nFileUtilCpp;

FILEUTILCPP_API int fnFileUtilCpp(void);
