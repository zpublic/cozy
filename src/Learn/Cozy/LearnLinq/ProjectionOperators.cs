using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.LearnLinq.Details;

namespace Cozy.LearnLinq
{
    class ProjectionOperators
    {
        public static void Cozy()
        {
            ProjectionOperators self = new ProjectionOperators();

            self.Linq6();
            self.Linq7();
            self.Linq8();
            self.Linq9();
            self.Linq10();
            self.Linq11();
            self.Linq12();
            self.Linq13();
            self.Linq14();
            self.Linq15();
            self.Linq16();
            self.Linq17();
            self.Linq18();
            self.Linq19();
        }

        public void Linq6()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var numsPlusOne =
                from n in numbers
                select n + 1;

            Console.WriteLine("Numbers + 1:");
            foreach (var i in numsPlusOne)
            {
                Console.WriteLine(i);
            }
        }

        public void Linq7()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var productNames =
                from p in products
                select p.ProductName;

            Console.WriteLine("Product Names:");
            foreach (var productName in productNames)
            {
                Console.WriteLine(productName);
            }
        }

        public void Linq8()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var textNums =
                from n in numbers
                select strings[n];

            Console.WriteLine("Number strings:");
            foreach (var s in textNums)
            {
                Console.WriteLine(s);
            }
        }

        public void Linq9()
        {
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };

            var upperLowerWords =
                from w in words
                select new { Upper = w.ToUpper(), Lower = w.ToLower() };

            foreach (var ul in upperLowerWords)
            {
                Console.WriteLine("Uppercase: {0}, Lowercase: {1}", ul.Upper, ul.Lower);
            }
        }

        public void Linq10()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var digitOddEvens =
                from n in numbers
                select new { Digit = strings[n], Even = (n % 2 == 0) };

            foreach (var d in digitOddEvens)
            {
                Console.WriteLine("The digit {0} is {1}.", d.Digit, d.Even ? "even" : "odd");
            }
        }

        public void Linq11()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var productInfos =
                from p in products
                select new { p.ProductName, p.Category, Price = p.UnitPrice };

            Console.WriteLine("Product Info:");
            foreach (var productInfo in productInfos)
            {
                Console.WriteLine("{0} is in the category {1} and costs {2} per unit.", productInfo.ProductName, productInfo.Category, productInfo.Price);
            }
        }

        public void Linq12()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var numsInPlace = numbers.Select((num, index) => new { Num = num, InPlace = (num == index) });

            Console.WriteLine("Number: In-place?");
            foreach (var n in numsInPlace)
            {
                Console.WriteLine("{0}: {1}", n.Num, n.InPlace);
            }
        }

        public void Linq13()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var lowNums =
                from n in numbers
                where n < 5
                select digits[n];

            Console.WriteLine("Numbers < 5:");
            foreach (var num in lowNums)
            {
                Console.WriteLine(num);
            }
        }

        public void Linq14()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var pairs =
                from a in numbersA
                from b in numbersB
                where a < b
                select new { a, b };

            Console.WriteLine("Pairs where a < b:");
            foreach (var pair in pairs)
            {
                Console.WriteLine("{0} is less than {1}", pair.a, pair.b);
            }
        }

        public void Linq15()
        {
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var orders =
                from c in customers
                from o in c.Orders
                where o.Total < 500.00M
                select new { c.CustomerID, o.OrderID, o.Total };

            ObjectDumper.Write(orders);
        }

        public void Linq16()
        {
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var orders =
                from c in customers
                from o in c.Orders
                where o.OrderDate >= new DateTime(1998, 1, 1)
                select new { c.CustomerID, o.OrderID, o.OrderDate };

            ObjectDumper.Write(orders);
        }

        public void Linq17()
        {
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var orders =
                from c in customers
                from o in c.Orders
                where o.Total >= 2000.0M
                select new { c.CustomerID, o.OrderID, o.Total };

            ObjectDumper.Write(orders);
        }

        public void Linq18()
        {
            List<Customer> customers = PublicData.instance.GetCustomerList();

            DateTime cutoffDate = new DateTime(1997, 1, 1);

            var orders =
                from c in customers
                where c.Region == "WA"
                from o in c.Orders
                where o.OrderDate >= cutoffDate
                select new { c.CustomerID, o.OrderID };

            ObjectDumper.Write(orders);
        }

        public void Linq19()
        {
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var customerOrders =
                customers.SelectMany(
                    (cust, custIndex) =>
                    cust.Orders.Select(o => "Customer #" + (custIndex + 1) +
                                            " has an order with OrderID " + o.OrderID));

            ObjectDumper.Write(customerOrders);
        }
    }
}
