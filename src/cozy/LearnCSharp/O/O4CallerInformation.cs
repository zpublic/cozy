using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.O
{
    class O4CallerInformation
    {
        private int someProperty;
        public int SomeProperty
        {
            get { return someProperty; }
            set
            {
                this.Log();
                someProperty = value;
            }
        }

        public void Log(
          [CallerLineNumber] int line = -1,
          [CallerFilePath] string path = null,
          [CallerMemberName] string name = null)
        {
            Console.WriteLine((line < 0) ? "No line" : "Line " + line);
            Console.WriteLine((path == null) ? "No file path" : path);
            Console.WriteLine((name == null) ? "No member name" : name);
            Console.WriteLine();
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            var p = new O4CallerInformation();
            p.Log();
            p.SomeProperty = 33;

            Action a1 = () => p.Log();
            a1();
        }
    }
}
