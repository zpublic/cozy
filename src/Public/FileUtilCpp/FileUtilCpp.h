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

using FILEENUMPROC = bool (CALLBACK*)(LPTSTR str, bool IsFolder);

// This class is exported from the FileUtilCpp.dll
class CFileUtilCpp {
public:
    CFileUtilCpp(void);
    // TODO: add your methods here.

    bool FileCopy(LPCTSTR lpSourcePath, LPCTSTR lpDestPath, bool b);

    bool FileMove(LPCTSTR lpSourcePath, LPCTSTR lpDestPath);

    bool FileDelete(LPCTSTR lpPath);

    bool PathFileExist(LPCTSTR lpPath);

    void FileEnum(LPCTSTR lpPath, FILEENUMPROC lpEnumFunc);

    bool IsDirectory(LPCTSTR lpPath);

    DWORD64 GetFileLength(LPCTSTR lpPath);

    bool GetFileTimes(LPCTSTR lpPath, FILETIME* lpCreationTime, FILETIME* lpLastAccessTime, FILETIME* lpLastWriteTime);

    bool FillFileData(LPCTSTR lpPath, WIN32_FIND_DATA* lpData);

    DWORD CurrentDirectoryGet(DWORD dwLength, LPTSTR lpResult);
};

extern FILEUTILCPP_API CFileUtilCpp CFileUtilCppInstance;

extern "C" FILEUTILCPP_API bool FileCopy(LPCTSTR lpSourcePath, LPCTSTR lpDestPath, bool bFailIfExists);

extern "C" FILEUTILCPP_API bool FileMove(LPCTSTR lpSourcePath, LPCTSTR lpDestPath);

extern "C" FILEUTILCPP_API bool FileDelete(LPCTSTR lpPath);

extern "C" FILEUTILCPP_API bool PathFileExist(LPCTSTR lpPath);

extern "C" FILEUTILCPP_API bool IsDirectory(LPCTSTR lpPath);

extern "C" FILEUTILCPP_API DWORD64 GetFileLength(LPCTSTR lpPath);

extern "C" FILEUTILCPP_API bool GetFileTimes(LPCTSTR lpPath, FILETIME* lpCreationTime, FILETIME* lpLastAccessTime, FILETIME* lpLastWriteTime);

extern "C" FILEUTILCPP_API bool FillFileData(LPCTSTR lpPath, WIN32_FIND_DATA* lpData);

extern "C" FILEUTILCPP_API void FileEnum(LPCTSTR lpPath, FILEENUMPROC lpEnumFunc);

extern "C" FILEUTILCPP_API  DWORD CurrentDirectoryGet(DWORD dwLength, LPTSTR lpResult);