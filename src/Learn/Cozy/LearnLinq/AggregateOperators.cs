using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.LearnLinq.Details;

namespace Cozy.LearnLinq
{
    class AggregateOperators
    {
        public static void Cozy()
        {
            AggregateOperators self = new AggregateOperators();

            self.Linq73();
            self.Linq74();
            self.Linq76();
            self.Linq77();
            self.Linq78();
            self.Linq79();
            self.Linq80();
            self.Linq81();
            self.Linq82();
            self.Linq83();
            self.Linq84();
            self.Linq85();
            self.Linq86();
            self.Linq87();
            self.Linq88();
            self.Linq89();
            self.Linq90();
            self.Linq91();
            self.Linq92();
            self.Linq93();
        }

        public void Linq73()
        {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };
            int uniqueFactors = factorsOf300.Distinct().Count();

            Console.WriteLine("There are {0} unique factors of 300.", uniqueFactors);
        }

        public void Linq74()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);

            Console.WriteLine("There are {0} odd numbers in the list.", oddNumbers);
        }

        public void Linq76()
        {
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var orderCounts =
                from c in customers
                select new { c.CustomerID, OrderCount = c.Orders.Count() };

            ObjectDumper.Write(orderCounts);
        }

        public void Linq77() 
        {
            List<Product> products = PublicData.instance.GetProductList(); 
  
            var categoryCounts = 
                from p in products 
                group p by p.Category into g 
                select new { Category = g.Key, ProductCount = g.Count() };

            ObjectDumper.Write(categoryCounts); 
        }

        public void Linq78()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            double numSum = numbers.Sum();

            Console.WriteLine("The sum of the numbers is {0}.", numSum);
        }

        public void Linq79()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            double totalChars = words.Sum(w => w.Length);

            Console.WriteLine("There are a total of {0} characters in these words.", totalChars);
        }

        public void Linq80()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var categories =
                from p in products
                group p by p.Category into g
                select new { Category = g.Key, TotalUnitsInStock = g.Sum(p => p.UnitsInStock) };

            ObjectDumper.Write(categories);
        }

        public void Linq81()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int minNum = numbers.Min();

            Console.WriteLine("The minimum number is {0}.", minNum);
        }

        public void Linq82()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            int shortestWord = words.Min(w => w.Length);

            Console.WriteLine("The shortest word is {0} characters long.", shortestWord);
        }

        public void Linq83()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var categories =
                from p in products
                group p by p.Category into g
                select new { Category = g.Key, CheapestPrice = g.Min(p => p.UnitPrice) };

            ObjectDumper.Write(categories);
        }

        public void Linq84()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var categories =
                from p in products
                group p by p.Category into g
                let minPrice = g.Min(p => p.UnitPrice)
                select new { Category = g.Key, CheapestProducts = g.Where(p => p.UnitPrice == minPrice) };

            ObjectDumper.Write(categories, 1);
        }

        public void Linq85()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int maxNum = numbers.Max();

            Console.WriteLine("The maximum number is {0}.", maxNum);
        }

        public void Linq86()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            int longestLength = words.Max(w => w.Length);

            Console.WriteLine("The longest word is {0} characters long.", longestLength);
        }

        public void Linq87()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var categories =
                from p in products
                group p by p.Category into g
                select new { Category = g.Key, MostExpensivePrice = g.Max(p => p.UnitPrice) };

            ObjectDumper.Write(categories);
        }

        public void Linq88()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var categories =
                from p in products
                group p by p.Category into g
                let maxPrice = g.Max(p => p.UnitPrice)
                select new { Category = g.Key, MostExpensiveProducts = g.Where(p => p.UnitPrice == maxPrice) };

            ObjectDumper.Write(categories, 1);
        }

        public void Linq89()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            double averageNum = numbers.Average();

            Console.WriteLine("The average number is {0}.", averageNum);
        }

        public void Linq90()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            double averageLength = words.Average(w => w.Length);

            Console.WriteLine("The average word length is {0} characters.", averageLength);
        }

        public void Linq91()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var categories =
                from p in products
                group p by p.Category into g
                select new { Category = g.Key, AveragePrice = g.Average(p => p.UnitPrice) };

            ObjectDumper.Write(categories);
        }

        public void Linq92()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);

            Console.WriteLine("Total product of all numbers: {0}", product);
        }

        public void Linq93()
        {
            double startBalance = 100.0;
            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };
            double endBalance =
                attemptedWithdrawals.Aggregate(startBalance,
                    (balance, nextWithdrawal) =>
                        ((nextWithdrawal <= balance) ? (balance - nextWithdrawal) : balance));

            Console.WriteLine("Ending balance: {0}", endBalance);
        }
    }
}
