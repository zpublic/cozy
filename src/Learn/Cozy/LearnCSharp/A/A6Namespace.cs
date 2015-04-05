using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.A
{
    // using using_namespace命名空间
    using using_namespace;

    // 为using_namespace指定别名 Aliases
    using Aliases = using_namespace;


    // 定义命名空间
    namespace  namespaceA
    {
        // 同一命名空间定义多个类
        class ClassA
        {
            public static int value = 1;
        }

        class ClassB
        {
            public static int value = 2;
        }
    }

    // 定义同一个命名空间
    namespace namespaceA
    {
        class ClassC
        {
            public static int value = 3;
        }

        // 嵌套定义命名空间
        namespace inside_namespace
        {
            class ClassD
            {
                public static int value = 4;
            }
        }
    }

    namespace namespaceB
    {
        // 定义多个命名空间
        namespace inside_namespaceA
        {
            class inside_classA
            {
                public static int value = 5;
            }
        }

        namespace inside_namespaceB
        {
            // 不同命名空间定义相同名称的类
            class inside_classA
            {
                public static int value = 6;
            }
            class inside_classB
            {
                public static int value = 7;
            }
        }
    }

    // 定义被用于using语句的命名空间
    namespace using_namespace
    {
        class using_namespace_class
        {
            public static int value = 8;
        }
    }
    
    // 定义与别名冲突的命名空间
    namespace Aliases
    {

    }

    class A6Namespace
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            NamespaceDefined();
            Using_Directive();

        }

        public static void NamespaceDefined()
        {
            // 获取各命名空间中定义的值
            int nsAcAvalue = namespaceA.ClassA.value;
            int nsAcBvalue = namespaceA.ClassB.value;
            int nsAcDvalue = namespaceA.ClassC.value;
            int nsA_in_cDvalue = namespaceA.inside_namespace.ClassD.value;
            int nsB_inA_icAvalue = namespaceB.inside_namespaceA.inside_classA.value;
            int nsB_inB_icAvalue = namespaceB.inside_namespaceB.inside_classA.value;
            int nsB_inB_icBvalue = namespaceB.inside_namespaceB.inside_classB.value;

            Console.WriteLine(nsAcAvalue);
            Console.WriteLine(nsAcBvalue);
            Console.WriteLine(nsAcDvalue);
            Console.WriteLine(nsA_in_cDvalue);
            Console.WriteLine(nsB_inA_icAvalue);
            Console.WriteLine(nsB_inB_icAvalue);
            Console.WriteLine(nsB_inB_icBvalue);

        }

        public static void Using_Directive()
        {
            // 以下调用方式相同
            int usnscvalue = using_namespace_class.value;
            int nscvalue = using_namespace.using_namespace_class.value;
            // int nsAcvalue_e = Aliases.using_namespace_class.value;   // 别名Aliases与命名空间 Aliases冲突
            int nsAcvalue = Aliases::using_namespace_class.value;       // 若命名空间冲突 使用 :: 则采用指定的别名

            Console.WriteLine(usnscvalue);
            Console.WriteLine(nscvalue);
            Console.WriteLine(nsAcvalue);

        }
    }
}
