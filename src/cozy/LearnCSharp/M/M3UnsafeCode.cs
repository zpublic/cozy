using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.M
{
    class M3UnsafeCode
    {
        static unsafe void UnsafeCode1()
        {
            int x = 10;
            short y = -1;
            byte y2 = 4;
            double z = 1.5;
            int* pX = &x;
            short* pY = &y;
            double* pZ = &z;

            Console.WriteLine(
               "Address of x is 0x{0:X}, size is {1}, value is {2}",
               (uint)&x, sizeof(int), x);
            Console.WriteLine(
               "Address of y is 0x{0:X}, size is {1}, value is {2}",
               (uint)&y, sizeof(short), y);
            Console.WriteLine(
               "Address of y2 is 0x{0:X}, size is {1}, value is {2}",
               (uint)&y2, sizeof(byte), y2);
            Console.WriteLine(
               "Address of z is 0x{0:X}, size is {1}, value is {2}",
               (uint)&z, sizeof(double), z);
            Console.WriteLine(
               "Address of pX=&x is 0x{0:X}, size is {1}, value is 0x{2:X}",
               (uint)&pX, sizeof(int*), (uint)pX);
            Console.WriteLine(
               "Address of pY=&y is 0x{0:X}, size is {1}, value is 0x{2:X}",
               (uint)&pY, sizeof(short*), (uint)pY);
            Console.WriteLine(
               "Address of pZ=&z is 0x{0:X}, size is {1}, value is 0x{2:X}",
               (uint)&pZ, sizeof(double*), (uint)pZ);

            *pX = 20;
            Console.WriteLine("After setting *pX, x = {0}", x);
            Console.WriteLine("*pX = {0}", *pX);

            pZ = (double*)pX;
            Console.WriteLine("x treated as a double = {0}", *pZ);
        }

        internal struct CurrencyStruct
        {
            public long Dollars;
            public byte Cents;

            public override string ToString()
            {
                return "$" + Dollars + "." + Cents;
            }
        }

        internal class CurrencyClass
        {
            public long Dollars;
            public byte Cents;

            public override string ToString()
            {
                return "$" + Dollars + "." + Cents;
            }
        }

        static unsafe void UnsafeCode2()
        {
            Console.WriteLine("Size of Currency struct is " + sizeof(CurrencyStruct));
            CurrencyStruct amount1, amount2;
            CurrencyStruct* pAmount = &amount1;
            long* pDollars = &(pAmount->Dollars);
            byte* pCents = &(pAmount->Cents);

            Console.WriteLine("Address of amount1 is 0x{0:X}", (uint)&amount1);
            Console.WriteLine("Address of amount2 is 0x{0:X}", (uint)&amount2);
            Console.WriteLine("Address of pAmt is 0x{0:X}", (uint)&pAmount);
            Console.WriteLine("Address of pDollars is 0x{0:X}", (uint)&pDollars);
            Console.WriteLine("Address of pCents is 0x{0:X}", (uint)&pCents);
            pAmount->Dollars = 20;
            *pCents = 50;
            Console.WriteLine("amount1 contains " + amount1);
            --pAmount;   // this should get it to point to amount2
            Console.WriteLine("amount2 has address 0x{0:X} and contains {1}",
               (uint)pAmount, *pAmount);
            // do some clever casting to get pCents to point to cents
            // inside amount2
            CurrencyStruct* pTempCurrency = (CurrencyStruct*)pCents;
            pCents = (byte*)(--pTempCurrency);
            Console.WriteLine("Address of pCents is now 0x{0:X}", (uint)&pCents);
            Console.WriteLine("\nNow with classes");
            // now try it out with classes
            CurrencyClass amount3 = new CurrencyClass();

            fixed (long* pDollars2 = &(amount3.Dollars))
            fixed (byte* pCents2 = &(amount3.Cents))
            {
                Console.WriteLine("amount3.Dollars has address 0x{0:X}", (uint)pDollars2);
                Console.WriteLine("amount3.Cents has address 0x{0:X}", (uint)pCents2);
                *pDollars2 = -100;
                Console.WriteLine("amount3 contains " + amount3);
            }
        }

        static unsafe void QuickArray()
        {
            uint size = 15;
            long* pArray = stackalloc long[(int)size];
            for (int i = 0; i < size; i++)
            {
                pArray[i] = i * i;
            }

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Element {0} = {1}", i, *(pArray + i));
            }
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            UnsafeCode1();
            UnsafeCode2();
            QuickArray();
        }
    }
}
