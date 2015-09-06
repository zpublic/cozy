using CozyPoker.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Client.Game24Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            CozyPoker.Client.Core.Game24Calc c = new CozyPoker.Client.Core.Game24Calc();
            while (true)
            {
                List<Card> cs = c.Get();
                foreach (var i in cs)
                {
                    Console.Write(i.ToString());
                    Console.Write("  ");
                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }
    }
}
