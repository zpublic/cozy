using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;

namespace Cozy.LearnFoundation.D
{
    class D4MappedMemoryFile
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            MappedMemoryFile_Sample();
        }

        public static void MappedMemoryFile_Sample()
        {
            // 自动释放资源
            using (var mmFile = 
                MemoryMappedFile.CreateFromFile(
                @"D:\cozy.txt", 
                System.IO.FileMode.OpenOrCreate, 
                "FileHandle", 
                1024 * 1 - 24))
            {
                string valueToWirte = "Written to mapped-memory file on " + DateTime.Now.ToString();
                var readOut = new byte[valueToWirte.Length];
                
                // 创建访问器对象
                var mAccessor = mmFile.CreateViewAccessor();

                // 读写操作
                mAccessor.WriteArray<byte>(0, Encoding.ASCII.GetBytes(valueToWirte), 0, valueToWirte.Length);
                mAccessor.ReadArray<byte>(0, readOut, 0, readOut.Length);

                var finalValue = Encoding.ASCII.GetString(readOut);
                Console.WriteLine(finalValue);
            }
        }
    }
}
