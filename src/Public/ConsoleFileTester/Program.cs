using CozyAnywhere.Plugin.WinFile;
using System;

namespace ConsoleFileTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pathName    = @"E:\";
            var WinFileList = FileUtil.DefFileEnum(pathName);

            Console.WriteLine("---------------------------------Folder:---------------------------------");
            foreach (var obj in WinFileList)
            {
                if (obj.IsFolder)
                {
                    Console.WriteLine("Name: {0}\tCreationTime: {1}\tLastAccessTime: {2}\tLastWriteTime: {3}",
                    obj.Name, obj.Times.CreationTime, obj.Times.LastAccessTime, obj.Times.LastWriteTime);
                }
            }

            Console.WriteLine("---------------------------------File:---------------------------------");
            foreach (var obj in WinFileList)
            {
                if (!obj.IsFolder)
                {
                    Console.WriteLine("Name: {0}\tSize: {1}\tCreationTime: {2}\tLastAccessTime: {3}\tLastWriteTime: {4}",
                    obj.Name, obj.Size, obj.Times.CreationTime, obj.Times.LastAccessTime, obj.Times.LastWriteTime);
                }
            }
            Console.WriteLine("---------------------------------End:---------------------------------");
            Console.ReadKey();
        }
    }
}