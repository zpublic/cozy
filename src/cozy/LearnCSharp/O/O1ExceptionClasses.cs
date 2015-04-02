using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.O
{
    class O1ExceptionClasses
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            System.Exception e1 = new Exception();
            System.ApplicationException e2 = new ApplicationException();
            e2 = null;
            System.Reflection.TargetInvocationException e3 = new TargetInvocationException(e1);
            System.SystemException e4 = new SystemException();
            e4 = null;
            System.StackOverflowException e5 = new StackOverflowException();
            e5 = null;
        }
    }
}
