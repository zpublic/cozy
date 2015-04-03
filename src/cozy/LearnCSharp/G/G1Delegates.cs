using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.G
{
    delegate double DoubleOp(double x);
    delegate bool Compare(int a, int b);

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
            Action_and_Func();
            Multicast_Delegates();
            Anonymous_Methods();
        }

        public static void Action_and_Func()
        {
            // Action<in, in, in....in>
            Action<int> ActInt = Details.G1MathOperations.ActionInt1;
            Action<int, int> ActDouInt = Details.G1MathOperations.DoubleInt;
            ActInt(1);
            ActDouInt(1, 2);

            // Func<in, in, in....in, out>
            Func<double,double> FuncDD = Details.G1MathOperations.Square;
            Func<int, int, int, int, int> Funciiiii = Details.G1MathOperations.AddAll;
            Console.WriteLine(FuncDD(3.0));
            Console.WriteLine(Funciiiii(1,2,3,4));

            int[] arr = new int[]{4,3,2,6,9,8,7,1,0,5};
            Details.G1MathOperations.BubbleSorter(arr, Details.G1MathOperations.less_do);
            for (int i = 0; i < arr.Length; ++i)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();

        }

        public static void Multicast_Delegates()
        {
            // 多播委托
            Action<int> action1 = Details.G1MathOperations.ActionInt1;
            Action<int> action2 = Details.G1MathOperations.ActionInt2;

            Action<int> DoubleAction = action1;
            DoubleAction += action2;
            DoubleAction(42);

            Action<int> ResultAction = action1 + action2;
            ResultAction(233);
            ResultAction -= action1;
            ResultAction(233);
        }

        public static void Anonymous_Methods()
        {
            // 匿名方法
            Func<int, int> DoubleFunc = delegate(int n)
            {
                return n * 2;
            };
            Console.WriteLine(DoubleFunc(42));
        }
    }
}
