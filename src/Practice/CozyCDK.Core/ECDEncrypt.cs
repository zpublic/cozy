using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CozyCDK.Core
{
    public class ECDEncrypt : IEncrypt, IDisposable
    {
        private byte[] key;
        private ECDsaCng dsa = new ECDsaCng();

        public string TestData { get; set; } = "CozyTestData";
        public byte[] TestDataBytes
        {
            get
            {
                return Encoding.UTF8.GetBytes(TestData);
            }
        }

        public ECDEncrypt()
        {
            dsa.HashAlgorithm   = CngAlgorithm.Sha256;
            key                 = dsa.Key.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        public bool Check(string source)
        {
            using (ECDsaCng ecsdKey = new ECDsaCng(CngKey.Import(key, CngKeyBlobFormat.EccPublicBlob)))
            {
                byte[] destData = new byte[source.Length / 2];
                for(int i = 0; i < source.Length / 2; ++i)
                {
                    destData[i] = Convert.ToByte(source.Substring(i * 2, 2), 16);
                }
                return ecsdKey.VerifyData(TestDataBytes, destData);
            }
        }

        public string Generate(string source)
        {
            byte[] destByte = dsa.SignData(TestDataBytes);

            var sb = new StringBuilder();
            for(int i = 0; i < destByte.Length; ++i)
            {
                sb.Append(destByte[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public void Dispose()
        {
            dsa.Dispose();
        }
    }
}
