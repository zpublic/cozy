using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCDK.Core;

namespace CozyCDK.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            IEncrypt enc1 = new MD5Encrypt();
            IEncrypt enc2 = new ECDEncrypt()
            {
                TestData = "Cozy最屌",
            };
            IEncrypt enc3 = new MD5InfoEncrypt()
            {
                Info = "Cozy最屌",
            };

            string str1 = enc1.Generate("Cozy");
            string str2 = enc2.Generate(null);
            string str3 = enc3.Generate(null);

            Console.WriteLine(str1);
            Console.WriteLine(str2);
            Console.WriteLine(str3);
            Console.WriteLine(enc2.Check(str2));
            Console.WriteLine(enc3.Check(str3));

            Console.ReadKey();
        }
    }
}
