using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.E
{
    class E3JaggedArray
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
            int[][] jagged = new int[3][];
            jagged[0] = new int[3] {1, 2, 3};
            jagged[1] = new int[2] {1, 2};
            jagged[2] = new int[4] {1, 2, 3, 4};

            for(int i = 0; i < jagged.Length; ++i)
            {
                for(int j = 0; j < jagged[i].Length; ++j)
                {
                    Console.WriteLine("row : {0} col : {1} value : {2}", i, j, jagged[i][j]);
                }
            }
        }
    }
}
