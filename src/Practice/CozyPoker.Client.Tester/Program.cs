using CozyPoker.Client.Core;
using CozyPoker.Engine.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Client.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestGameBullfight();
            //PatternAequitas();
            PatternFirehawk();
        }

        private static void PatternFirehawk()
        {
            PatternFirehawk p = new PatternFirehawk();
            if (p.Init("firehawk_bullfight"))
            {
                p.Shuffle();
                var ccA = p.Deal();
                foreach (var i in ccA.Cards)
                {
                    Console.Write(i.ToString());
                    Console.Write("  ");
                }
                Console.WriteLine();
                var ccB = p.Deal();
                foreach (var i in ccB.Cards)
                {
                    Console.Write(i.ToString());
                    Console.Write("  ");
                }
                Console.WriteLine();
                p.Compare(ccA, ccB);
            }
        }

        private static void PatternAequitas()
        {
            PatternAequitas p = new PatternAequitas();
            if (p.Init("aequitas_24calc"))
            {
                var cc = p.Run();
                foreach (var i in cc.Cards)
                {
                    Console.Write(i.ToString());
                    Console.Write("  ");
                }
                Console.WriteLine();
                cc = p.Run();
                foreach (var i in cc.Cards)
                {
                    Console.Write(i.ToString());
                    Console.Write("  ");
                }
                Console.WriteLine();
                cc = p.Run();
                foreach (var i in cc.Cards)
                {
                    Console.Write(i.ToString());
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        private static void TestGameBullfight()
        {
            GameBullfight bf = new GameBullfight();
            bf.Shuffle();
            var a = bf.Get();
            var b = bf.Get();

            Console.Write("A的牌： ");
            foreach (var i in a)
            {
                Console.Write(i.ToString());
                Console.Write("  ");
            }
            Console.WriteLine();
            Console.Write("B的牌： ");
            foreach (var i in b)
            {
                Console.Write(i.ToString());
                Console.Write("  ");
            }
            Console.WriteLine();
            if (bf.Compare(a, b))
            {
                Console.WriteLine("A胜");
            }
            else
            {
                Console.WriteLine("B胜");
            }
        }
    }
}
