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

#include "windows.h"
#include <vector>

// This class is exported from the FileUtilCpp.dll
class FILEUTILCPP_API CFileUtilCpp {
public:
	CFileUtilCpp(void);
	// TODO: add your methods here.

    bool FileCopy(LPCTSTR lpSourcePath, LPCTSTR lpDestPath, bool bFailIfExists);

    bool FileMove(LPCTSTR lpSourcePath, LPCTSTR lpDestPath);

    bool FileDelete(LPCTSTR lpPath);

    bool PathFileExist(LPCTSTR lpPath);

    void FileEnum(LPCTSTR lpPath, std::vector<LPCTSTR>& result);

    bool IsDirectory(LPCTSTR lpPath);

    DWORD64 GetFileSize(LPCTSTR lpPath);

    bool GetFileTime(LPCTSTR lpPath, FILETIME* lpCreationTime, FILETIME* lpLastAccessTime, FILETIME* lpLastWriteTime);

    bool FillFileData(LPCTSTR lpPath, WIN32_FIND_DATA* lpData);
};

extern FILEUTILCPP_API int nFileUtilCpp;

FILEUTILCPP_API int fnFileUtilCpp(void);
