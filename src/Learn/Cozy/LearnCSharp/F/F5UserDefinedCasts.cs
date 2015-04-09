using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.F
{

    class TwoInt
    {
        public int value1;
        public int value2;

        // 定义构造函数
        public TwoInt()
        {

        }

        public TwoInt(int value1, int value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        // 重载ToString 方便输出
        public override string ToString()
        {
            return "value 1 : " + value1.ToString() + " value 2 : " + value2.ToString();
        }

        // TwoInt强制转换到int
        public static implicit operator int(TwoInt lhs)
        {
            return lhs.value1 + lhs.value2;
        }

        // int强制转换到TwoInt
        public static implicit operator TwoInt(int lhs)
        {
            TwoInt obj = new TwoInt(lhs, lhs);
            return obj;
        }

        // long强制转换到TwoInt
        public static implicit operator TwoInt(long lhs)
        {
            checked     // 可能抛出异常
            {
                TwoInt obj = new TwoInt();
                obj.value1 = obj.value2 = (int)lhs;
                return obj;
            }
        }
    }

    // 定义基类
    class CastBaseClass
    {
        public int BaseValue;
        public CastBaseClass(int value)
        {
            BaseValue = value;
        }

        public override string ToString()
        {
            return "BaseValue : " + BaseValue.ToString();
        }
    }

    // 定义派生类1
    class CastDeriveClass1 : CastBaseClass
    {
        public int DeriveValueA;

        public CastDeriveClass1(int b, int d)
            :base(b)
        {
                DeriveValueA = d;
        }

        public override string ToString()
        {
            return base.ToString() + " DeriveValueA : " + DeriveValueA.ToString();
        }
    }

    // 定义派生类2
    class CastDeriveClass2 : CastBaseClass
    {
        public int DeriveValueB;

        public CastDeriveClass2(int b, int d)
            :base(b)
        {
            DeriveValueB = d;
        }

        public override string ToString()
        {
            return base.ToString() + " DeriveValueB : " + DeriveValueB.ToString();
        }

        // 定义类型转换
        public static explicit operator CastDeriveClass1(CastDeriveClass2 lhs)
        {
            CastDeriveClass1 obj = new CastDeriveClass1(lhs.BaseValue, lhs.DeriveValueB);
            return obj;
        }

        public static explicit operator CastDeriveClass2(CastDeriveClass1 lhs)
        {
            CastDeriveClass2 obj = new CastDeriveClass2(lhs.BaseValue, lhs.DeriveValueA);
            return obj;
        }
    }

    class F5UserDefinedCasts
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Implementing_User_Defined_Casts();
            Casts_Between_Classes();
        }

        public static void Implementing_User_Defined_Casts()
        {
            // int和TwoInt互相转换
            TwoInt t1 = 1;
            Console.WriteLine(t1.ToString());
            int i1 = t1;
            Console.WriteLine(i1);
            
            // long和TwoInt互相转换
            long l1 = 2147483648L;
            try
            {
                TwoInt t = l1;      // 这里会抛出一个异常
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
        }

        public static void Casts_Between_Classes()
        {
            CastDeriveClass1 cdc1 = new CastDeriveClass1(1, 2);
            CastDeriveClass2 cdc2 = new CastDeriveClass2(3, 4);
            CastDeriveClass1 cdc3 = (CastDeriveClass1)cdc2;

            Console.WriteLine(cdc1.ToString());
            Console.WriteLine(cdc2.ToString());
            Console.WriteLine(cdc3.ToString());
        }
    }
}
