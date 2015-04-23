using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.LearnLinq.Details;
using Cozy.LearnLinq.Details.Compare;

namespace Cozy.LearnLinq
{
    class GroupingOperators
    {
        public static void Cozy()
        {
            GroupingOperators self = new GroupingOperators();

            self.Linq40();
            self.Linq41();
            self.Linq42();
            self.Linq43();
            self.Linq44();
            self.Linq45();
        }

        public void Linq40()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var numberGroups =
                from n in numbers
                group n by n % 5 into g
                select new { Remainder = g.Key, Numbers = g };

            foreach (var g in numberGroups)
            {
                Console.WriteLine("Numbers with a remainder of {0} when divided by 5:", g.Remainder);
                foreach (var n in g.Numbers)
                {
                    Console.WriteLine(n);
                }
            }
        }

        public void Linq41()
        {
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };

            var wordGroups =
                from w in words
                group w by w[0] into g
                select new { FirstLetter = g.Key, Words = g };

            foreach (var g in wordGroups)
            {
                Console.WriteLine("Words that start with the letter '{0}':", g.FirstLetter);
                foreach (var w in g.Words)
                {
                    Console.WriteLine(w);
                }
            }
        }

        public void Linq42()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var orderGroups =
                from p in products
                group p by p.Category into g
                select new { Category = g.Key, Products = g };

            ObjectDumper.Write(orderGroups, 1);
        }

        public void Linq43()
        {
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var customerOrderGroups =
                from c in customers
                select
                    new
                    {
                        c.CompanyName,
                        YearGroups =
                            from o in c.Orders
                            group o by o.OrderDate.Year into yg
                            select
                                new
                                {
                                    Year = yg.Key,
                                    MonthGroups =
                                        from o in yg
                                        group o by o.OrderDate.Month into mg
                                        select new { Month = mg.Key, Orders = mg }
                                }
                    };

            ObjectDumper.Write(customerOrderGroups, 3);
        }

        public void Linq44()
        {
            string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            var orderGroups = anagrams.GroupBy(w => w.Trim(), new AnagramEqualityComparer());

            ObjectDumper.Write(orderGroups, 1);
        }

        public void Linq45()
        {
            string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            var orderGroups = anagrams.GroupBy(
                        w => w.Trim(),
                        a => a.ToUpper(),
                        new AnagramEqualityComparer());

            ObjectDumper.Write(orderGroups, 1);
        } 
    }
}
