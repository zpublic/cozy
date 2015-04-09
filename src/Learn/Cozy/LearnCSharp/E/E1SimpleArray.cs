using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.E
{
    class ArrayObject
    {
        public int value1;
        public int value2;

        public override string ToString()
        {
            return value1.ToString() + " " + value2.ToString();
        }
    }

    class E1SimpleArray
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Array_Declaration_and_Initialization();
            Accessing_Array_Elements();
            Using_Reference_Types();
        }

        public static void Array_Declaration_and_Initialization()
        {
            int[] array1 = new int[4];
            int[] array2 = new int[4] { 0, 1, 2, 3 };
            int[] array3 = new int[] { 0, 1, 2, 3 };
            int[] array4 = { 0, 1, 2, 3 };
        }

        public static void Accessing_Array_Elements()
        {
            int[] array = { 1, 2, 3, 4 };
            int e1 = array[0];
            int e2 = array[1];
            Console.WriteLine("{0} {1}", e1, e2);

            for(int i = 0; i< array.Length; ++i)
            {
                Console.Write("{0}", array[i]);
            }
            Console.WriteLine();

            foreach(var elemt in array)
            {
                Console.Write("{0}",elemt);
            }
            Console.WriteLine();
        }

        public static void Using_Reference_Types()
        {
            ArrayObject[] array1 = new ArrayObject[2];
            array1[0] = new ArrayObject { value1 = 0, value2 = 1 };
            array1[1] = new ArrayObject { value1 = 2, value2 = 3 };

            ArrayObject[] array2 = 
            {
                new ArrayObject { value1 = 0, value2 = 1 },
                new ArrayObject { value1 = 2, value2 = 3 }    
            };

            Console.WriteLine(array1[0].ToString() + " " + array1[1].ToString());
            Console.WriteLine(array2[0].ToString() + " " + array2[1].ToString());
        }
    }
}
