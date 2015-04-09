using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.E
{
    class E2MultidimensionalArray
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Declaration_and_Initialization_and_Access();
        }

        public static void Declaration_and_Initialization_and_Access()
        {
            int[,] array1 = {
                                {1,2,3},
                                {4,5,6},
                                {7,8,9}
                           };
            int[, ,] array2 = {
                                { {1, 2}, {3, 4} },
                                { {5, 6}, {7, 8} }
                             };

            Console.WriteLine("{0} {1}", array1[1, 2], array2[0, 1, 1]);
        }
    }
}
