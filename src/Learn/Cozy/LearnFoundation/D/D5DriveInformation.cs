using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cozy.LearnFoundation.D
{
    class D5DriveInformation
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Reading_Drive_Info();
        }

        public static void Reading_Drive_Info()
        {

            // 遍历驱动器并获取信息
            DriveInfo[] dis = DriveInfo.GetDrives();

            foreach (var elemt in dis)
            {
                Console.WriteLine();
                Console.WriteLine("Name : " + elemt.Name);
                Console.WriteLine("VolumeLabel : " + elemt.VolumeLabel);
                Console.WriteLine("DriveFormat : " + elemt.DriveFormat);
                Console.WriteLine("DriveType : " + elemt.DriveType);
                Console.WriteLine("RootDirectory : " + elemt.RootDirectory);
                Console.WriteLine("IsReady : " + elemt.IsReady);
                Console.WriteLine("AvailableFreeSpace : " + elemt.AvailableFreeSpace);
                Console.WriteLine("TotalFreeSpace : " + elemt.TotalFreeSpace);
                Console.WriteLine("TotalSize : " + elemt.TotalSize);
            }
        }
    }
}
