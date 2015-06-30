// FileUtilCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "FileUtilCpp.h"
#include "shlwapi.h"

#pragma comment(lib,"shlwapi.lib")

// This is an example of an exported variable
FILEUTILCPP_API int nFileUtilCpp=0;

// This is an example of an exported function.
FILEUTILCPP_API int fnFileUtilCpp(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see FileUtilCpp.h for the class definition
CFileUtilCpp::CFileUtilCpp()
{
	return;
}

bool CFileUtilCpp::PathFileExist(LPCTSTR lpPath)
{
    return ::PathFileExists(lpPath) == TRUE;
}

bool CFileUtilCpp::FileDelete(LPCTSTR lpPath)
{
    return ::DeleteFile(lpPath) == TRUE;
}

bool CFileUtilCpp::FileCopy(LPCTSTR lpSourcePath, LPCTSTR lpDestPath, bool bFailIfExists)
{
    return ::CopyFile(lpSourcePath, lpDestPath, bFailIfExists) == TRUE;
}

bool CFileUtilCpp::FillFileData(LPCTSTR lpPath, WIN32_FIND_DATA* lpData)
{
    HANDLE hFile = ::FindFirstFile(lpPath, lpData);
    if (hFile == INVALID_HANDLE_VALUE)
    {
        CloseHandle(hFile);
        return false;
    }
    ::FindClose(hFile);
    return true;
}

bool CFileUtilCpp::IsDirectory(LPCTSTR lpPath)
{
    WIN32_FIND_DATA FindFileData;
    if (!FillFileData(lpPath, &FindFileData))
    {
        return false;
    }

    return (FindFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) == TRUE;
}

bool CFileUtilCpp::FileMove(LPCTSTR lpSourcePath, LPCTSTR lpDestPath)
{
    return ::MoveFile(lpSourcePath, lpDestPath) == TRUE;
}

DWORD64 CFileUtilCpp::GetFileSize(LPCTSTR lpPath)
{
    WIN32_FIND_DATA FindFileData;
    if (!FillFileData(lpPath, &FindFileData))
    {
        return 0;
    }

    DWORD64 result  = 0;
    result          = FindFileData.nFileSizeHigh;
    result          <<= 32;
    result          &= FindFileData.nFileSizeLow;
    return result;
}

void CFileUtilCpp::FileEnum(LPCTSTR lpPath, std::vector<LPCTSTR>& result)
{
    WIN32_FIND_DATA FindFileData;
    HANDLE hFile = ::FindFirstFile(lpPath, &FindFileData);
    if (hFile == INVALID_HANDLE_VALUE)
    {
        CloseHandle(hFile);
        return;
    }

    do
    {
        if (!(FindFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
        {
            if (FindFileData.cFileName[0] != '.')
            {
                result.push_back(FindFileData.cFileName);
            }
        }
    } while (FindNextFile(hFile, &FindFileData));
    FindClose(hFile);
}

bool CFileUtilCpp::GetFileTime(LPCTSTR lpPath, FILETIME* lpCreationTime, FILETIME* lpLastAccessTime, FILETIME* lpLastWriteTime)
{
    if (lpCreationTime == nullptr && lpLastAccessTime == nullptr && lpLastWriteTime == nullptr) return false;

    WIN32_FIND_DATA FindFileData;
    if (!FillFileData(lpPath, &FindFileData))
    {
        return false;
    }

    if (lpCreationTime != nullptr)
    {
        *lpCreationTime = FindFileData.ftCreationTime;
    }

    if (lpLastAccessTime != nullptr)
    {
        *lpLastAccessTime = FindFileData.ftLastAccessTime;
    }

    if (lpLastWriteTime != nullptr)
    {
        *lpLastWriteTime = FindFileData.ftLastWriteTime;
    }
    return true;
}
