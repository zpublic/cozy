using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Cozy.LearnFoundation.D
{
    class D6FileSecurity
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Reading_ACLs_from_a_File();
            Reading_ACLs_from_a_Directory();
            Adding_and_Removing_ACLs_from_a_File();
        }

        public static void Reading_ACLs_from_a_File()
        {
            string fileName = @"D:\cozy1.txt";
            Console.WriteLine(fileName);
            //try
            //{
            //    using (FileStream mFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            //    {
            //        // 获取ACL信息
            //        FileSecurity fileSec = mFile.GetAccessControl();

            //        // 遍历所有权限
            //        foreach (FileSystemAccessRule fileRule in fileSec.GetAccessRules(true, true, typeof(NTAccount)))
            //        {
            //            Console.WriteLine(fileName + " " + Print_AccessRul(fileRule));
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("file path error " + e.Message);
            //}
        }

        public static void Reading_ACLs_from_a_Directory()
        {
            string direName = @"D:\cozy_dire";
            Console.WriteLine(direName);

            //try
            //{
            //    DirectoryInfo di = new DirectoryInfo(direName);
            //    if (di.Exists)
            //    {
            //        // 获取ACL信息
            //        DirectorySecurity mDireSec = di.GetAccessControl();

            //        // 遍历所有权限
            //        foreach (FileSystemAccessRule fileRule in mDireSec.GetAccessRules(true, true, typeof(NTAccount)))
            //        {
            //            Console.WriteLine(direName + " " + Print_AccessRul(fileRule));
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("file path error " + e.Message);
            //}
        }

        public static string Print_AccessRul(FileSystemAccessRule rule)
        {
            string result = String.Format("{0} {1} access for {2}",
                rule.AccessControlType == AccessControlType.Allow ? "provides" : "denies",
                rule.FileSystemRights,
                rule.IdentityReference);
            return result;
        }

        public static void Adding_and_Removing_ACLs_from_a_File()
        {
            string fileName = @"D:\cozy1.txt";
            Console.WriteLine(fileName);

            //try
            //{
            //    using (FileStream mFile = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            //    {
            //        // 获取原有AlC信息
            //        FileSecurity fileSec = mFile.GetAccessControl();

            //        Console.WriteLine("\nBefore Change");
            //        foreach (FileSystemAccessRule fileRule in fileSec.GetAccessRules(true, true, typeof(NTAccount)))
            //        {
            //            Console.WriteLine(fileName + " " + Print_AccessRul(fileRule));
            //        }

            //        // 为Every用户赋予FullControl权限
            //        FileSystemAccessRule newRule = new FileSystemAccessRule(
            //            new System.Security.Principal.NTAccount("Everyone"),
            //            FileSystemRights.FullControl,
            //            AccessControlType.Allow);

            //        // 设置文件ALC信息
            //        fileSec.AddAccessRule(newRule);
            //        File.SetAccessControl(fileName, fileSec);

            //        // 输出修改后的ALC信息
            //        Console.WriteLine("\nAfter Change");
            //        foreach (FileSystemAccessRule fileRule in fileSec.GetAccessRules(true, true, typeof(NTAccount)))
            //        {
            //            Console.WriteLine(fileName + " " + Print_AccessRul(fileRule));
            //        }

            //        // 删除刚设置的ALC信息
            //        fileSec.RemoveAccessRule(newRule);
            //        File.SetAccessControl(fileName, fileSec);

            //        Console.WriteLine("\nAfter Change");
            //        foreach (FileSystemAccessRule fileRule in fileSec.GetAccessRules(true, true, typeof(NTAccount)))
            //        {
            //            Console.WriteLine(fileName + " " + Print_AccessRul(fileRule));
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("file path error " + e.Message);
            //}
        }
    }
}
