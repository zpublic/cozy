using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cozy.LearnLinq.Details;

namespace Cozy.LearnLinq
{
    class RestrictionOperators
    {
        public static void Cozy()
        {
            RestrictionOperators self = new RestrictionOperators();

            // Where - Simple 1
            self.Linq1();
            self.Linq2();
            self.Linq3();
            self.Linq5();
        }

        public void Linq1()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from n in numbers
                where n < 5
                select n;

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }

            // result 4 1 3 2 0
        }

        public void Linq2()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var soldOutProducts =
                from p in products
                where p.UnitsInStock == 0
                select p;

            Console.WriteLine("Sold out products:");
            foreach (var product in soldOutProducts)
            {
                Console.WriteLine("{0} is sold out!", product.ProductName);
            }
        }

        public void Linq3()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var expensiveInStockProducts =
                from prod in products
                where prod.UnitsInStock > 0 && prod.UnitPrice > 3.00M
                select prod;

            Console.WriteLine("In-stock products that cost more than 3.00:");
            foreach (var product in expensiveInStockProducts)
            {
                Console.WriteLine("{0} is in stock and costs more than 3.00.", product.ProductName);
            }
        }

        public void Linq5()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var shortDigits = digits.Where((digit, index) => digit.Length < index);

            Console.WriteLine("Short digits:");
            foreach (var d in shortDigits)
            {
                Console.WriteLine("The word {0} is shorter than its value.", d);
            }
        }
    }
}
