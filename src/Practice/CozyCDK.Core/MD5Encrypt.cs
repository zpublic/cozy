using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CozyCDK.Core
{
    public class MD5Encrypt : IEncrypt
    {
        private static MD5 _Md5 = new MD5CryptoServiceProvider();

        public bool Check(string source)
        {
            throw new NotImplementedException();
        }

        public string Generate(string source)
        {
            var srcByte = Encoding.UTF8.GetBytes(source);
            var destByte = _Md5.ComputeHash(srcByte);

            var sb = new StringBuilder();
            for(int i = 0; i < destByte.Length; ++i)
            {
                sb.Append(destByte[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
