using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.E
{
    class E5ArrayParameter
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            SimpleArrayParameter();
            Array_Covariance();
            ArraySegment();
        }

        public static void SimpleArrayParameter()
        {
            int[] array = SimpleArrayOut(10);
            SimpleArrayIn(array);
        }

        // 将数组作为返回值
        public static int[] SimpleArrayOut(int n)
        {
            int[] array = new int[n];
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = i;
            }
            return array;
        }

        // 将数组作为参数
        public static void SimpleArrayIn(int[] array)
        {
            for(int i = 0; i< array.Length; ++i)
            {
                Console.Write(array[i]);
            }
            Console.WriteLine();
        }

        // 数组协变
        public static void Array_Covariance()
        {
            string[] strArray = 
            {
                "hello",
                "hi",
                "hehe"
            };
            Array_Covariance_In(strArray);
        }

        // 只能用于引用类型 不能用于值类型
        public static void Array_Covariance_In(object[] objs)
        {
            string[] strArray = objs as string[];
            foreach(var elemt in strArray)
            {
                Console.WriteLine(elemt);
            }
        }

        public static void ArraySegment()
        {
            int[] intArray = { 0, 1, 2, 3, 4, 5, 6, 7 };
            ArraySegment<int> segment = new ArraySegment<int>(intArray, 1, 6);
            int sum = SumOfSegments(segment);
            Console.WriteLine(sum);
        }

        public static int SumOfSegments(ArraySegment<int> segment)
        {
            int sum = 0;
            for(int i = segment.Offset; i < segment.Offset + segment.Count; ++i)
            {
                sum += segment.Array[i];
            }
            return sum;
        }
    }
}
