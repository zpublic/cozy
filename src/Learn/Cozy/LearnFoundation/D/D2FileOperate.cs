using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cozy.LearnFoundation.D
{
    class D2FileOperate
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Create_Moving_Copying_and_Deleting_Files();
        }

        public static void Create_Moving_Copying_and_Deleting_Files()
        {
            FileInfo file = new FileInfo(@"D:\cozy.txt");

            // 创建 移动 复制 删除文件 修改文件最后修改时间
            //(file.Create()).Close();                      // 返回一个FileStream对象 因为要移动File 所以关闭
            //file.MoveTo(@"D:\cozy1.txt");
            //FileInfo new_file = file.CopyTo(@"D:\cozy.txt");
            //file.Delete();
            //file.LastWriteTime = new DateTime(2015, 4, 10, 22, 00, 00);

            // 与上方FileInfo实现的功能相同 调用File中的静态方法实现
            //(File.Create(@"D:\cozy.txt")).Close();
            //File.Move(@"D:\cozy.txt", @"D:\cozy1.txt");
            //File.Copy(@"D:\cozy1.txt", @"D:\cozy.txt");
            //File.Delete(@"D:\cozy1.txt");
            //File.SetLastWriteTime(@"D:\cozy.txt", new DateTime(2015, 4, 10, 22, 00, 00));
        }
    }
}
