using System;
using System.Runtime.InteropServices;

namespace ClientTester.Ext
{
    public class FileUtil
    {
        public delegate bool FileEnumFunc(IntPtr ptr, bool IsFolder);

        [DllImport(@"../../FileUtilCpp/Debug/FileUtilCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool FileEnum(string Path, FileEnumFunc func);

        [DllImport(@"../../FileUtilCpp/Debug/FileUtilCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool FileDelete(string Path);
    }
}