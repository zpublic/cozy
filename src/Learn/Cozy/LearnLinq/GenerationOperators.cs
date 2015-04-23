using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnLinq
{
    class GenerationOperators
    {
        public static void Cozy()
        {
            GenerationOperators self = new GenerationOperators();

            self.Linq65();
            self.Linq66();
        }

        public void Linq65()
        {
            var numbers =
                from n in Enumerable.Range(100, 50)

                select new { Number = n, OddEven = n % 2 == 1 ? "odd" : "even" };

            foreach (var n in numbers)
            {
                Console.WriteLine("The number {0} is {1}.", n.Number, n.OddEven);
            }
        }

        public void Linq66()
        {
            var numbers = Enumerable.Repeat(7, 10);

            foreach (var n in numbers)
            {
                Console.WriteLine(n);
            }
        }
    }
}
