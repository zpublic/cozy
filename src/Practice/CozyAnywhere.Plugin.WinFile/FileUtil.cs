using System;
using System.Runtime.InteropServices;

namespace CozyAnywhere.Plugin.WinFile
{
    public static class FileUtil
    {
        // bool (CALLBACK*)(LPTSTR str, bool IsFolder);
        public delegate bool FileEnumFunc(IntPtr ptr, bool IsFolder);

        // bool FileCopy(LPCTSTR lpSourcePath, LPCTSTR lpDestPath, bool b);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool FileCopy(string SourcePath, string DestPath, bool FailIfExists);

        // bool FileMove(LPCTSTR lpSourcePath, LPCTSTR lpDestPath);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool FileMove(string SourcePath, string DestPath);

        // bool FileDelete(LPCTSTR lpPath);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool FileDelete(string Path);

        // bool PathFileExist(LPCTSTR lpPath);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool PathFileExist(string Path);

        // bool IsDirectory(LPCTSTR lpPath);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool IsDirectory(string Path);

        // DWORD64 GetFileLength(LPCTSTR lpPath);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern ulong GetFileLength(string Path);

        // bool GetFileTimes(LPCTSTR lpPath, FILETIME* lpCreationTime, FILETIME* lpLastAccessTime, FILETIME* lpLastWriteTime);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern bool GetFileTimes(string path, ref ulong CreationTime, ref ulong LastAccessTime, ref ulong LastWriteTime);

        // void FileEnum(LPCTSTR lpPath, FILEENUMPROC lpEnumFunc);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern void FileEnum(string Path, FileEnumFunc func);
    }
}