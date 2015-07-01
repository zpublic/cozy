// FileUtilCpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "FileUtilCpp.h"
#include "shlwapi.h"

#pragma comment(lib,"shlwapi.lib")

// This is an example of an exported variable
FILEUTILCPP_API CFileUtilCpp CFileUtilCppInstance;

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

DWORD64 CFileUtilCpp::GetFileLength(LPCTSTR lpPath)
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

void CFileUtilCpp::FileEnum(LPCTSTR lpPath, std::vector<LPCTSTR>* result)
{
    WIN32_FIND_DATA FindFileData;
    if (result != nullptr)
    {
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
                    result->push_back(FindFileData.cFileName);
                }
            }
        } while (FindNextFile(hFile, &FindFileData));
        FindClose(hFile);
    }
}

bool CFileUtilCpp::GetFileTimes(LPCTSTR lpPath, FILETIME* lpCreationTime, FILETIME* lpLastAccessTime, FILETIME* lpLastWriteTime)
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

FILEUTILCPP_API bool FileCopy(LPCTSTR lpSourcePath, LPCTSTR lpDestPath, bool bFailIfExists)
{
    return CFileUtilCppInstance.FileCopy(lpSourcePath, lpDestPath, bFailIfExists);
}

FILEUTILCPP_API bool FileMove(LPCTSTR lpSourcePath, LPCTSTR lpDestPath)
{
    return CFileUtilCppInstance.FileMove(lpSourcePath, lpDestPath);
}

FILEUTILCPP_API bool FileDelete(LPCTSTR lpPath)
{
    return CFileUtilCppInstance.FileDelete(lpPath);
}

FILEUTILCPP_API bool PathFileExist(LPCTSTR lpPath)
{
    return CFileUtilCppInstance.PathFileExist(lpPath);
}

FILEUTILCPP_API void FileEnum(LPCTSTR lpPath, std::vector<LPCTSTR>* result)
{
    return CFileUtilCppInstance.FileEnum(lpPath, result);
}

FILEUTILCPP_API bool IsDirectory(LPCTSTR lpPath)
{
    return CFileUtilCppInstance.IsDirectory(lpPath);
}

FILEUTILCPP_API DWORD64 GetFileLength(LPCTSTR lpPath)
{
    return CFileUtilCppInstance.GetFileLength(lpPath);
}

FILEUTILCPP_API bool GetFileTimes(LPCTSTR lpPath, FILETIME* lpCreationTime, FILETIME* lpLastAccessTime, FILETIME* lpLastWriteTime)
{
    return CFileUtilCppInstance.GetFileTimes(lpPath, lpCreationTime, lpLastAccessTime, lpLastWriteTime);
}

FILEUTILCPP_API bool FillFileData(LPCTSTR lpPath, WIN32_FIND_DATA* lpData)
{
    return CFileUtilCppInstance.FillFileData(lpPath, lpData);
}