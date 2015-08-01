using System;
using System.Text;
using System.Collections.Generic;
using CozyAnywhere.Plugin.WinFile.Model;
using CozyAnywhere.Plugin.WinFile.Ext;
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

        // DWORD CurrentDirectoryGet(DWORD dwLength, LPTSTR lpResult);
        [DllImport(@"FileUtilCpp.dll",
            CharSet             = CharSet.Auto,
            CallingConvention   = CallingConvention.Cdecl)]
        public static extern uint CurrentDirectoryGet(uint length, StringBuilder result);

        #region DefaultMethod

        public static string FileGetCurrentDirectory()
        {
            uint length = CurrentDirectoryGet(0, null);
            if(length != 0)
            {
                StringBuilder result = new StringBuilder((int)length);
                if(CurrentDirectoryGet(length, result) != 0)
                {
                    return result.ToString();
                }
                return null;
            }
            return null;
        }


        public static WinFileTimeModel DefGetFileTimes(string path)
        {
            ulong creationTime      = 0;
            ulong lastAccessTime    = 0;
            ulong lastWriteTime     = 0;

            GetFileTimes(path, ref creationTime, ref lastAccessTime, ref lastWriteTime);

            var times = new WinFileTimeModel()
            {
                CreationTime    = creationTime.ToDateTime(),
                LastAccessTime  = lastAccessTime.ToDateTime(),
                LastWriteTime   = lastWriteTime.ToDateTime(),
            };
            return times;
        }

        public static List<WinFileModel> DefFileEnum(string path, bool EnumSize = true, bool EnumTime = true)
        {
            List<WinFileModel> Result   = new List<WinFileModel>();
            string enumPath             = path + '*';

            FileEnum(enumPath, (x, b) => 
            {
                string name     = path + Marshal.PtrToStringAuto(x);
                bool isFolder   = b;
                var file = new WinFileModel()
                {
                    Name        = name,
                    IsFolder    = isFolder,
                };

                if(EnumSize)
                {
                    file.Size   = GetFileLength(enumPath + name);
                }

                if(EnumTime)
                {
                    file.Times  = DefGetFileTimes(enumPath + name);
                }

                Result.Add(file);
                return false; 
            });

            return Result;
        }
        #endregion
    }
}