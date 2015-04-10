using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cozy.LearnFoundation.D
{
    class D3FileReadAndWrite
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Reading_a_File();
            Writing_to_a_File();
            Console.ReadKey();
        }

        public static void Reading_a_File()
        {
            // 调用File.ReadAllText方法
            //string fileName = @"D:\cozy.txt";
            //string text = File.ReadAllText(fileName, Encoding.ASCII);
            //Console.WriteLine(text);

            //// 调用File.ReadAllLines方法
            //string[] strline = File.ReadAllLines(fileName);
            //foreach(var elemt in strline)
            //{
            //    Console.WriteLine(elemt);
            //}
        }

        public static void Writing_to_a_File()
        {
            //string fileName = @"D:\cozy.txt";

            //// 调用File.WriteAllText
            //string text = "hello world";
            //File.WriteAllText(fileName, text);

            //// 调用File.WriteAllLines
            //string[] texts= 
            //{
            //    "zapline",
            //    "kingwl",
            //    "1900s",
            //    "Star",
            //    "AngryPowman",
            //    "Larrygood"
            //};
            //File.WriteAllLines(fileName, texts);
        }

        public static void Streams()
        {
            //string fileName = @"D:\cozy.txt";
            //FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);

            //// do something

            //fs.Close();

            //FileInfo fi = new FileInfo(fileName);
            //fs = fi.OpenWrite();

            //int dataSize = 255;
            //byte[] data = new byte[dataSize];

            //// read
            //int nextByte = fs.ReadByte();
            //int nByteRead = fs.Read(data, 0, dataSize);

            //// write
            //fs.WriteByte(100);
            //fs.Write(data, 0, dataSize);

            //fs.Close();
        }
    }
}
