using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.E
{
    class CozyCollection
    {
        private string[] StrVar = 
        { 
            "zapline",
            "AngryPowman",
            "Star",
            "Kingwl",
            "1900s"
        };

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < StrVar.Length; ++i)
            {
                yield return StrVar[i];
            }
        }

        public IEnumerable<string> Reverse()
        {
            for (int i = StrVar.Length - 1; i >= 0; --i)
            {
                yield return StrVar[i];
            }
        }

        public IEnumerable<string> Subset(int first, int last)
        {
            for (int i = first; i < last; ++i)
            {
                yield return StrVar[i];
            }
        }
    }

    class E6Enumeraton
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            yield_Statement();
        }

        public static void yield_Statement()
        {
            CozyCollection collect = new CozyCollection();
            foreach(var elemt in collect)
            {
                Console.WriteLine(elemt);
            }

            foreach (var elemt in collect.Reverse())
            {
                Console.WriteLine(elemt);
            }

            foreach (var elemt in collect.Subset(2,4))
            {
                Console.WriteLine(elemt);
            }
        }
    }
}
