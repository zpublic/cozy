using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using cozy.LearnFoundation.B.Details;

namespace Cozy.LearnFoundation.B
{
    class B2Encryption
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Signature();
            ExchangeAndTransfer.Cozy();
        }

        internal static CngKey aliceKeySignature;
        internal static byte[] alicePubKeyBlob;

        public static void Signature()
        {
            // 创建新的密钥对
            CreateKeys();

            byte[] aliceData = Encoding.UTF8.GetBytes("Alice");
            byte[] aliceSignature = CreateSignature(aliceData, aliceKeySignature);
            Console.WriteLine("Alice create signature {0}", Convert.ToBase64String(alicePubKeyBlob));
            if (VerifySignature(aliceData, aliceSignature, alicePubKeyBlob))
            {
                Console.WriteLine("Alice signature verify success");
            }
        }

        public static void CreateKeys()
        {
            // 根据算法创建密钥对
            aliceKeySignature = CngKey.Create(CngAlgorithm.ECDsaP256);
            // 导出密钥对中的公钥
            alicePubKeyBlob = aliceKeySignature.Export(CngKeyBlobFormat.GenericPublicBlob);
        }

        public static byte[] CreateSignature(byte[] data, CngKey key)
        {
            // 创建签名
            byte[] signature;
            using(var signingAlg = new ECDsaCng(key))
            {
                signature = signingAlg.SignData(data);
                signingAlg.Clear();
            }
            return signature;
        }

        public static bool VerifySignature(byte[] data, byte[] signature, byte[] pubKey)
        {
            // 验证签名
            bool result = false;
            using (CngKey key = CngKey.Import(pubKey, CngKeyBlobFormat.GenericPublicBlob))
            {
                using(var signingAlg = new ECDsaCng(key))
                {
                    result = signingAlg.VerifyData(data, signature);
                    signingAlg.Clear();
                }
            }
            return result;
        }
    }
}
