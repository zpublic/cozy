using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace cozy.LearnFoundation.B.Details
{
    public class ExchangeAndTransfer
    {
        static CngKey aliceKey;
        static CngKey bobKey;
        static byte[] alicePubKeyBlod;
        static byte[] bobPubKeyBlob;

        public static void Cozy()
        {
            Run();
        }

        private async static void Run()
        {
            try
            {
                CreateKeys();
                byte[] encrytpedData = await AliceSendsData("secret message");
                BobReceivesData(encrytpedData);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CreateKeys()
        {
            aliceKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            alicePubKeyBlod = aliceKey.Export(CngKeyBlobFormat.GenericPublicBlob);
            bobKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            bobPubKeyBlob = bobKey.Export(CngKeyBlobFormat.GenericPublicBlob);
        }

        private async static Task<byte[]> AliceSendsData(string message)
        {
            Console.WriteLine("Alice send message {0}", message);
            byte[] rawData = Encoding.UTF8.GetBytes(message);
            byte[] encryptedData = null;

            using(var aliceAlgo = new ECDiffieHellmanCng(aliceKey))
            {
                using(CngKey bobPubKey = CngKey.Import(bobPubKeyBlob, CngKeyBlobFormat.GenericPublicBlob))
                {
                    byte[] symmKey = aliceAlgo.DeriveKeyMaterial(bobPubKey);
                    Console.WriteLine("Alice create this symm key with Bobs public key information : {0}", Convert.ToBase64String(symmKey));
                    using(var aes = new AesCryptoServiceProvider())
                    {
                        aes.Key = symmKey;
                        aes.GenerateIV();
                        using(ICryptoTransform encryptor = aes.CreateEncryptor())
                        {
                            using(MemoryStream ms = new MemoryStream())
                            {
                                var cs = new CryptoStream(ms, encryptor,CryptoStreamMode.Write);
                                await ms.WriteAsync(aes.IV, 0, aes.IV.Length);
                                cs.Write(rawData, 0, rawData.Length);
                                cs.Close();
                                encryptedData = ms.ToArray();
                            }
                        }
                        aes.Clear();
                    }
                }
            }
            Console.WriteLine("Alice message is encrypted : {0}", Convert.ToBase64String(encryptedData));
            return encryptedData;
        }

        private static void BobReceivesData(byte[] encryptedData)
        {
            Console.WriteLine("Bob receives encrypted data");
            byte[] rawData = null;
            var aes = new AesCryptoServiceProvider();

            int nBytes = aes.BlockSize >> 3;
            byte[] iv = new byte[nBytes];
            for(int i = 0; i < iv.Length; ++i)
            {
                iv[i] = encryptedData[i];
            }

            using(var bobAlgro = new ECDiffieHellmanCng(bobKey))
            {
                using(CngKey alicePubKey = CngKey.Import(alicePubKeyBlod, CngKeyBlobFormat.GenericPublicBlob))
                {
                    byte[] symmKey = bobAlgro.DeriveKeyMaterial(alicePubKey);
                    Console.WriteLine("Bob creates this symmetric key with ALices public key information : {0}", Convert.ToBase64String(symmKey));

                    aes.Key = symmKey;
                    aes.IV = iv;

                    using(ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        using(MemoryStream ms = new MemoryStream())
                        {
                            var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
                            cs.Write(encryptedData, nBytes, encryptedData.Length - nBytes);
                            cs.Close();

                            rawData = ms.ToArray();
                            Console.WriteLine("Bob decrypts message to {0}", Encoding.UTF8.GetString(rawData));
                        }
                    }
                }
            }
            aes.Clear();
        }
    }
}
