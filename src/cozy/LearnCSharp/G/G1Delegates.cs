using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.G
{
    delegate double DoubleOp(double x);

    class G1Delegates
    {
        static void ProcessAndDisplayNumber(DoubleOp action, double value)
        {
            double result = action(value);
            Console.WriteLine("Value is {0}, result of operation is {1}", value, result);
        }

        static void SimpleDelegate()
        {
            DoubleOp[] operations =
            {
                Details.G1MathOperations.MultiplyByTwo,
                Details.G1MathOperations.Square
            };

            for (int i = 0; i < operations.Length; ++i)
            {
                ProcessAndDisplayNumber(operations[i], 2.0);
                ProcessAndDisplayNumber(operations[i], 7.94);
                ProcessAndDisplayNumber(operations[i], 1.414);
            }
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");
            SimpleDelegate();
        }
    }
}
