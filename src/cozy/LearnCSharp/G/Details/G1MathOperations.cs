using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.G.Details
{
    class G1MathOperations
    {
        public static double MultiplyByTwo(double value)
        {
            return value * 2;
        }

        public static double Square(double value)
        {
            return value * value;
        }

        public static void ActionInt1(int n)
        {
            Console.WriteLine("ActionInt1 {0}", n);
        }

        public static void ActionInt2(int n)
        {
            Console.WriteLine("ActionInt2 {0}", n);
        }

        public static void DoubleInt(int a, int b)
        {
            Console.WriteLine("DoubleInt {0} {1}", a, b);
        }

        public static int AddAll(int a, int b, int c, int d)
        {
            return a + b + c + d;
        }

        public static bool less_do(int a, int b)
        {
            return a < b;
        }

        public static void BubbleSorter(int[] array, Compare comp)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                for (int j = i + 1; j < array.Length; ++j)
                {
                    if (comp(array[j], array[i]))
                    {
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
    }
}
