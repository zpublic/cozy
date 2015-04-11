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
            Reading_and_Writing_to_Text_Files();
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

        public static void Reading_and_Writing_to_Text_Files()
        {
            //string fileName = @"D:\cozy.txt";

            //// 使用不同方法创建StreamReader
            //StreamReader reader1 = new StreamReader(fileName);
            //reader1.Close();

            //FileStream fs1 = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //StreamReader reader2 = new StreamReader(fs1);
            //reader2.Close();
            //fs1.Close();

            //FileInfo fi = new FileInfo(fileName);
            //StreamReader reader3 = fi.OpenText();
            //reader3.Close();

            //// 使用不同方法创建StreamWriter

            //StreamWriter writer1 = new StreamWriter(fileName);
            //writer1.Close();
            //StreamWriter writer2 = new StreamWriter(fileName, true, Encoding.ASCII);
            //writer2.Close();

            //FileStream fs2 = new FileStream(fileName, FileMode.Open, FileAccess.Write, FileShare.Read);
            //StreamWriter writer3 = new StreamWriter(fs2);
            //writer3.Close();
            //fs2.Close();

            Read_Text_File(fileName);
            Write_Text_File(fileName);
        }

        public static void Read_Text_File(string fileName)
        {
            // 用ASCII格式读取
            //StreamReader reader = new StreamReader(fileName, Encoding.ASCII);

            //// 读取一行
            //string nextLine = reader.ReadLine();
            //Console.WriteLine(nextLine);

            //// 读取一个字符
            //int nextChar = reader.Read();
            //Console.WriteLine(nextChar);

            //// 读取指定大小的数据
            //int nChars = 10;
            //char[] charArray = new char[nChars];
            //int nCharRead = reader.Read(charArray, 0, nChars);
            //Console.WriteLine(charArray);

            //// 读取到结尾
            //nextLine = reader.ReadToEnd();
            //Console.WriteLine(nextLine);

            //reader.Close();
        }

        public static void Write_Text_File(string fileName)
        {
            //StreamWriter writer = new StreamWriter(fileName);

            //// 输出字符串
            //string nextLine = "hello";
            //writer.Write(nextLine);

            //// 输出单个字符
            //char nextChar = 'a';
            //writer.Write(nextChar);

            //// 输出字符数组
            //char[] charArray = new char[10];
            //writer.Write(charArray);

            //writer.Close();
        }
    }
}
