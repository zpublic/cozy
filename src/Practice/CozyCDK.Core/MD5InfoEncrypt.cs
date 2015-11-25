using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CozyCDK.Core
{
    public class MD5InfoEncrypt : ICheckedEncrypt
    {
        public const string DefaultMap = @"BCDFGHJKMPQRTVWXY2346789";

        private string map = DefaultMap;
        public string Map
        {
            get { return this.map; }
            set
            {
                if (value == null || value.Length != 24)
                {
                    throw new ArgumentException("map必须是长度24的字符串");
                }
                map = value;
            }
        }

        private string info = "Cozy";
        public string Info
        {
            get { return info; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                info = value;
            }
        }

        private MD5 _Md5    = new MD5CryptoServiceProvider();
        private Random rd   = new Random();

        public bool Check(string source)
        {
            var infoByte = Encoding.Default.GetBytes(Info);
            var infoStr = GetString(infoByte);

            for(int i = 0; i < infoStr.Length; ++i)
            {
                if(source[i] != infoStr[i])
                {
                    return false;
                }
            }
            return true;
        }

        public string Generate(string source)
        {
            var srcByte     = Encoding.Default.GetBytes(rd.Next().ToString());
            var md5Byte    = _Md5.ComputeHash(srcByte);
            var infoByte    = Encoding.Default.GetBytes(Info);

            return GetString(infoByte) + GetString(md5Byte);
        }

        public string GetString(byte[] bytes)
        {
            string text = string.Empty;
            for (int i = bytes.Length - 1; i >= 0; i -= 4)
            {
                uint data = 0;
                for (int j = i < 3 ? i : 3; j >= 0; j--)
                {
                    data = (data << 8) + bytes[i - j];
                }

                string t = string.Empty;
                while (data > 0)
                {
                    int d = (int)(data % 24);
                    t = map[d] + t;
                    data = data / 24;
                }
                text = t.PadLeft(7, map[0]) + text;
            }
            return text;
        }

        public byte[] GetBytes(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException();
            }
            int len = text.Length / 7 + (text.Length % 7 == 0 ? 0 : 1);
            byte[] bytes = new byte[len * 4];
            int pos = bytes.Length - 1;
            for (int i = text.Length - 1; i >= 0; i -= 7, pos -= 4)
            {
                uint data = 0;
                for (int j = i < 6 ? i : 6; j >= 0; j--)
                {
                    int d = map.IndexOf(text[i - j]);
                    if (d == -1)
                    {
                        throw new FormatException();
                    }
                    try
                    {
                        data = checked(data * 24 + (uint)d);
                    }
                    catch (OverflowException)
                    {
                        throw new FormatException();
                    }
                }

                byte[] t = BitConverter.GetBytes(data);
                for (int j = 0; j < 4; j++)
                {
                    bytes[pos - j] = t[j];
                }
            }
            return bytes;
        }
    }
}
