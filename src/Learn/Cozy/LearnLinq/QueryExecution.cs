using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnLinq
{
    class QueryExecution
    {
        public static void Cozy()
        {
            QueryExecution self = new QueryExecution();

            self.Linq99();
            self.Linq100();
        }

        public void Linq99()
        {
            // Sequence operators form first-class queries that 
            // are not executed until you enumerate over them. 

            int[] numbers = new int[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var q =
                from n in numbers
                select ++i;

            // Note, the local variable 'i' is not incremented 
            // until each element is evaluated (as a side-effect): 
            foreach (var v in q)
            {
                Console.WriteLine("v = {0}, i = {1}", v, i);
            }
        }

        public void Linq100()
        {
            // Methods like ToList() cause the query to be 
            // executed immediately, caching the results. 

            int[] numbers = new int[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var q = (
                from n in numbers
                select ++i)
                .ToList();

            // The local variable i has already been fully 
            // incremented before we iterate the results: 
            foreach (var v in q)
            {
                Console.WriteLine("v = {0}, i = {1}", v, i);
            }
        } 
    }
}
