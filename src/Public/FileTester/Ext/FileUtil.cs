using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace FileTester.Ext
{
    public class FileUtil
    {
        public delegate void FileEnumFunc(IntPtr ptr, bool IsFolder);
        [DllImport(@"../../FileUtilCpp/Debug/FileUtilCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FileEnum(string Path, FileEnumFunc func);

        [DllImport(@"../../FileUtilCpp/Debug/FileUtilCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool FileDelete(string Path);
    }
}
