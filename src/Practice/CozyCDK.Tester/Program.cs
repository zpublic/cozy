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
            IEncrypt enc = new MD5Encrypt();
            Console.WriteLine(enc.Generate("Cozy"));
            Console.ReadKey();
        }
    }
}
