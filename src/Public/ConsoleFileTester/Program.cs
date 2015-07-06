using CozyAnywhere.Plugin.WinFile;
using CozyAnywhere.Plugin.WinFile.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ConsoleFileTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var WinFileList = new List<WinFile>();
            var pathName    = @"E:\";
            FileUtil.FileEnum(pathName + '*', (x, b) =>
            {
                if (x != IntPtr.Zero)
                {
                    var path            = Marshal.PtrToStringAuto(x);
                    ulong size          = FileUtil.GetFileLength(pathName + path);
                    ulong createTime    = 0;
                    ulong accessTime    = 0;
                    ulong writeTime     = 0;
                    FileUtil.GetFileTimes(pathName + path, ref createTime, ref accessTime, ref writeTime);
                    var time = new WinFileTime()
                    {
                        CreationTime    = createTime,
                        LastAccessTime  = accessTime,
                        LastWriteTime   = writeTime,
                    };

                    var file = new WinFile()
                    {
                        Name        = path,
                        IsFolder    = b,
                        Size        = size,
                        Times       = time,
                    };
                    WinFileList.Add(file);
                }
                return false;
            });

            Console.WriteLine("---------------------------------Folder:---------------------------------");
            foreach (var obj in WinFileList)
            {
                if (obj.IsFolder)
                {
                    Console.WriteLine("Name: {0} CreationTime: {1} LastAccessTime: {2} LastWriteTime: {3}",
                    obj.Name, obj.Times.CreationTime, obj.Times.LastAccessTime, obj.Times.LastWriteTime);
                }
            }

            Console.WriteLine("---------------------------------File:---------------------------------");
            foreach (var obj in WinFileList)
            {
                if (!obj.IsFolder)
                {
                    Console.WriteLine("Name: {0} Size: {1} CreationTime: {2} LastAccessTime: {3} LastWriteTime: {4}",
                    obj.Name, obj.Size, obj.Times.CreationTime, obj.Times.LastAccessTime, obj.Times.LastWriteTime);
                }
            }
            Console.WriteLine("---------------------------------End:---------------------------------");
            Console.ReadKey();
        }
    }
}