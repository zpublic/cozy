using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("===============================================");
            Console.WriteLine("hello cozy!");
            LearnCSharp.A.A.Cozy();
        }
    }
}
