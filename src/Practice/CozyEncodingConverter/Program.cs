using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEncodingConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileList = Directory.GetFiles(Path.GetDirectoryName(@"D:\xxxxxxxxxxxxxxx\kui\"), "*", SearchOption.AllDirectories)
               .Where(x => x.EndsWith(".h") || x.EndsWith(".cc") || x.EndsWith(".cpp"));
            foreach (var f in fileList)
            {
                convert(f);
            }
        }

        static void convert(string f)
        {
            var en = GetFileEncode(f);
            if (en == Encoding.Default)
            {
                WriteTxt(f, ReadTxt(f), Encoding.UTF8);
            }
        }

        static byte[] ReadTxt(string filepath)
        {
            FileStream fileStream = File.Open(filepath, FileMode.Open, FileAccess.ReadWrite);
            var buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            fileStream.Close();
            fileStream.Dispose();
            return buffer;
        }

        static public Encoding GetFileEncode(string path)
        {
            FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
            var buffer = new byte[3];
            var len = fileStream.Length;
            fileStream.Read(buffer, 0, 3);
            fileStream.Close();
            fileStream.Dispose();
            if (len <= 0 || buffer[0] < 239)
                return Encoding.Default;
            if (buffer[0] == 239 && buffer[1] == 187 && buffer[2] == 191)
                return Encoding.UTF8;
            if (buffer[0] == 254 && buffer[1] == byte.MaxValue)
                return Encoding.BigEndianUnicode;
            if (buffer[0] == byte.MaxValue && buffer[1] == 254)
                return Encoding.Unicode;
            return Encoding.Default;
        }

        static private void WriteTxt(string filepath, byte[] body, Encoding encoding)
        {
            if (File.Exists(filepath))
                File.Delete(filepath);
            FileStream fileStream = File.Open(filepath, FileMode.CreateNew, FileAccess.Write);
            if (Equals(encoding, Encoding.UTF8))
            {
                fileStream.WriteByte(239);
                fileStream.WriteByte(187);
                fileStream.WriteByte(191);
            }
            else if (Equals(encoding, Encoding.BigEndianUnicode))
            {
                fileStream.WriteByte(254);
                fileStream.WriteByte(byte.MaxValue);
            }
            else if (Equals(encoding, Encoding.Unicode))
            {
                fileStream.WriteByte(byte.MaxValue);
                fileStream.WriteByte(254);
            }
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            var data = Encoding.Convert(gb2312, encoding, body);
            fileStream.Write(data, 0, data.Length);
            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
        }
    }
}
