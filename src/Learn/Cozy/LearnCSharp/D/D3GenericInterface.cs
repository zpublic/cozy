using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.D
{

    // 定义泛型接口
    interface GenericInterface<T>
    {
        bool SameAs(T t);
    }

    // 继承泛型接口
    class GenericInterfaceClass<T> : GenericInterface<T>
    {
        T value = default(T);

        // 实现泛型接口的方法
        public bool SameAs(T t)
        {
            return value.Equals(t);
        }
    }

    interface BaseInterface
    {

    }

    class DeriveClass : BaseInterface
    {

    }

    interface CovarianceInterface<out T>
    {
        T DoSomething();
        // void DoSomething(T t);   协变的泛型参数只能作为方法的返回值
    }

    interface ContravarianceInterface<in T>
    {
        void DoSomething(T t);
        // T DoSomething();         逆变的泛型参数只能作为方法的参数
    }

    class D3GenericInterface
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            GenericInterface();
            Covariance_and_Contravariance();
            Covariance_with_Generic_Interfaces();
        }

        public static void GenericInterface()
        {
            // 用int实例化GenericInterfaceClass泛型类
            GenericInterface<int> gi1 = new GenericInterfaceClass<int>();
            Console.WriteLine(gi1.SameAs(0).ToString());
        }

        public static void Covariance(BaseInterface b)
        {
            // do something
        }

        public static DeriveClass Contravariance()
        {
            return new DeriveClass();
        }

        public static void Covariance_and_Contravariance()
        {
            DeriveClass dc = new DeriveClass();
            // 协变
            Covariance(dc);

            // 抗变
            BaseInterface bc = Contravariance();
        }

        public static void Covariance_with_Generic_Interfaces()
        {
            // 协变
            CovarianceInterface<BaseInterface> cib1 = null;
            CovarianceInterface<DeriveClass> cid1 = null;
            cib1 = cid1;

            // 逆变
            ContravarianceInterface<BaseInterface> conib1 = null;
            ContravarianceInterface<DeriveClass> conid1 = null;
            conid1 = conib1;
        }
    }
}
