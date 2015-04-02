using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.M
{
    class M2FreeingUnmanagedResources
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Details.ResourceHolder res1 = new Details.ResourceHolder();
            res1.SomeMethod();
            res1.Dispose();
            try
            {
                res1.SomeMethod();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Details.ResourceHolder res2 = new Details.ResourceHolder();
            res2.SomeMethod();
        }
    }
}
