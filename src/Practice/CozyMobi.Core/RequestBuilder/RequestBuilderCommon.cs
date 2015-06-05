using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CozyMobi.Core.RequestBuilder
{
    class RequestBuilderCommon
    {
        public static string Host = "https://coding.net";
        public static string ApiUrl = Host + "/api";

        public static string Account = ApiUrl + "/account";
        public static string AccountLogin = Account + "/login";
        public static string AccountLogout = Account + "/logout";

        public static string Social = ApiUrl + "/social";
        public static string SocialMaopao = Social + "/tweet";

        public static string ToHexString(byte[] bytes)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("x2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        public static string EncryptToSHA1(string str)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = sha1.ComputeHash(str1);
            sha1.Clear();
            (sha1 as IDisposable).Dispose();
            return ToHexString(str2);
        }
    }
}
