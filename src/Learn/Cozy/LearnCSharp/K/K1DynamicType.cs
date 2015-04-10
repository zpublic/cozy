using System;

namespace Cozy.LearnCSharp.K
{
    class K1DynamicType
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //dynamic对象可以在运行时改变其类型

            dynamic dyn;
            dyn = 100;
            Console.WriteLine(dyn.GetType());
            Console.WriteLine(dyn);

            dyn = "This is a string";
            Console.WriteLine(dyn.GetType());
            Console.WriteLine(dyn);

            dyn = new Person() { FirstName = "Bugs", LastName = "Bunny" };
            Console.WriteLine(dyn.GetType());
            Console.WriteLine("{0} {1}", dyn.FirstName, dyn.LastName);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
