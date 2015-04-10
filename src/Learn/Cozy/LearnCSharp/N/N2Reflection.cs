using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Cozy.LearnCSharp.N
{
    // 定义用于反射的类
    class CozyReflectionClass
    {
        public int Member1 = 0;
        public string Member2 = "hello";
        private double Member3 = 0.0;

        CozyReflectionClass(double d)
        {
            Member3 = d;
        }

        public void Method1()
        {

        }

        public virtual void Method2(int n)
        {

        }

        public int Method3()
        {
            return 0;
        }
    }

    class N2Reflection
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            The_System_Type_Class();
            Members();
            Assemblys();
        }

        public static void The_System_Type_Class()
        {
            int var1 = 0;
            Type intType = typeof(int);
            Type classType = typeof(CozyReflectionClass);
            Type t1 = var1.GetType();
            Type t2 = Type.GetType("System.Double");

            PrintTypeProperties(intType);
            PrintTypeProperties(classType);
            PrintTypeProperties(t2);
        }

        // 输出所有成员
        public static void Members()
        {
            Type t = typeof(CozyReflectionClass);
            PrintTypeMembers(t);
        }

        // 输出类型属性
        public static void PrintTypeProperties(Type type)
        {
            Console.WriteLine("\nbegin");
            Console.WriteLine(type.Name);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.Namespace);
            Console.WriteLine(type.BaseType);
            Console.WriteLine("IsAbstract {0}", type.IsAbstract);
            Console.WriteLine("IsClass {0}", type.IsClass);
            Console.WriteLine("IsEnum {0}", type.IsEnum);
            Console.WriteLine("IsPrimitive {0}", type.IsPrimitive);
            Console.WriteLine("IsValueType {0}", type.IsValueType);
            Console.WriteLine("IsClass {0}", type.IsClass);
            Console.WriteLine("end\n");
        }

        public static void PrintTypeMembers(Type type)
        {
            MemberInfo[] info = type.GetMembers();
            foreach (var elemt in info)
            {
                Console.WriteLine(elemt.DeclaringType + " " + elemt.MemberType + " " + elemt.Name);
            }
        }

        public static void Assemblys()
        {
            List<Tuple<string, CozyTestAttribute>> LAttribs = new List<Tuple<string, CozyTestAttribute>>();

            // 载入Cozy程序集 获取定义的全部类
            Assembly assembly1 = Assembly.Load("Cozy");
            Type[] types = assembly1.GetTypes();

            foreach (var elemt in types)
            {
                if (!(elemt.IsClass))
                {
                    continue;
                }

                // 遍历类 获取全部方法
                MethodInfo[] mi = elemt.GetMethods();
                foreach (var mielemt in mi)
                {
                    // 遍历方法 获取有CozyTest属性的方法
                    object objs = mielemt.GetCustomAttribute(typeof(CozyTestAttribute), false);
                    if (objs != null)
                    {
                        LAttribs.Add(Tuple.Create<string, CozyTestAttribute>(elemt.Name, objs as CozyTestAttribute));
                    }
                }
            }
            Console.WriteLine("Count : {0}", LAttribs.Count);
            foreach (var elemt in LAttribs)
            {
                Console.WriteLine("{0} {1} {2}", elemt.Item1, elemt.Item2.Id, elemt.Item2.Name);
            }
        }
    }
}
