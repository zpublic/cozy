using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cozy.LearnFoundation.D
{
    class D1FileSystem
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Classes_That_Represent_Files_and_Folders();
            The_Path_Class();
        }

        public static void Classes_That_Represent_Files_and_Folders()
        {
            // 打开文件和文件夹
            FileInfo file = new FileInfo(@"D:\cozy.txt");
            DirectoryInfo dire = new DirectoryInfo(@"C:\Windows");

            //显示信息
            Print_File_Info(file);
            Print_Directory_Info(dire);
            Print_FileSystemInfos(dire);
        }

        // 输出FileInfo的属性
        public static void Print_File_Info(FileSystemInfo f)
        {
            var file = f as FileInfo;
            if (file != null && file.Exists)
            {
                Print_Public_Info(f);
                Console.WriteLine("DirectoryFullName : " + file.Directory.FullName);
                Console.WriteLine("Length : " + file.Length);
            }
            else
            {
                Console.WriteLine("Open Failed");
            }
        }

        // 输出DirectoryInfo的属性
        public static void Print_Directory_Info(FileSystemInfo f)
        {
            var dire = f as DirectoryInfo;
            if (dire != null && dire.Exists)
            {
                Print_Public_Info(f);
                Console.WriteLine("Parent : " + dire.Parent);
                Console.WriteLine("Root : " + dire.Root);
            }
            else
            {
                Console.WriteLine("Open Failed");
            }
        }

        // 输出FileSystemInfo的属性
        public static void Print_Public_Info(FileSystemInfo f)
        {
            Console.WriteLine("Name : " + f.Name);
            Console.WriteLine("Extension : " + f.Extension);
            Console.WriteLine("FullName : " + f.FullName);
            Console.WriteLine("CreationTime : " + f.CreationTime);
            Console.WriteLine("LastAccessTime : " + f.LastAccessTime);
            Console.WriteLine("LastWriteTime : " + f.LastWriteTime);
        }

        public static void Print_FileSystemInfos(DirectoryInfo f)
        {
            // 输出F目录下所有文件和文件夹的名字
            FileSystemInfo[] fsi = f.GetFileSystemInfos();
            foreach(var elemt in fsi)
            {
                Console.WriteLine("FullName : " + elemt.FullName);
            }
        }

        // 链接
        public static void The_Path_Class()
        {
            string filename1 = Path.Combine(@"C:\Windows", @"cozy.txt");
            string filename2 = Path.Combine(@"C:\Windows\", @"cozy.txt");
            
            // 输出相同
            Console.WriteLine(filename1);
            Console.WriteLine(filename2);
        }

        
    }
}
