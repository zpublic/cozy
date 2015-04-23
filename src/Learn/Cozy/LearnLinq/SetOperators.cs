using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.LearnLinq.Details;

namespace Cozy.LearnLinq
{
    class SetOperators
    {
        public static void Cozy()
        {
            SetOperators self = new SetOperators();

            self.Linq46();
            self.Linq47();
            self.Linq48();
            self.Linq49();
            self.Linq50();
            self.Linq51();
            self.Linq52();
            self.Linq53();
            Console.ReadKey();
        }

        public void Linq46()
        {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };
            var uniqueFactors = factorsOf300.Distinct();

            Console.WriteLine("Prime factors of 300:");
            foreach (var f in uniqueFactors)
            {
                Console.WriteLine(f);
            }
        }

        public void Linq47()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var categoryNames = (
                from p in products
                select p.Category)
                .Distinct();

            Console.WriteLine("Category names:");
            foreach (var n in categoryNames)
            {
                Console.WriteLine(n);
            }
        }
        public void Linq48()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var uniqueNumbers = numbersA.Union(numbersB);

            Console.WriteLine("Unique numbers from both arrays:");
            foreach (var n in uniqueNumbers)
            {
                Console.WriteLine(n);
            }
        }

        public void Linq49()
        {
            List<Product> products = PublicData.instance.GetProductList();
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var productFirstChars =
                from p in products
                select p.ProductName[0];
            var customerFirstChars =
                from c in customers
                select c.CompanyName[0];

            var uniqueFirstChars = productFirstChars.Union(customerFirstChars);

            Console.WriteLine("Unique first letters from Product names and Customer names:");
            foreach (var ch in uniqueFirstChars)
            {
                Console.WriteLine(ch);
            }
        }

        public void Linq50()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var commonNumbers = numbersA.Intersect(numbersB);

            Console.WriteLine("Common numbers shared by both arrays:");
            foreach (var n in commonNumbers)
            {
                Console.WriteLine(n);
            }
        }

        public void Linq51()
        {
            List<Product> products = PublicData.instance.GetProductList();
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var productFirstChars =
                from p in products
                select p.ProductName[0];
            var customerFirstChars =
                from c in customers
                select c.CompanyName[0];

            var commonFirstChars = productFirstChars.Intersect(customerFirstChars);

            Console.WriteLine("Common first letters from Product names and Customer names:");
            foreach (var ch in commonFirstChars)
            {
                Console.WriteLine(ch);
            }
        }

        public void Linq52()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            IEnumerable<int> aOnlyNumbers = numbersA.Except(numbersB);

            Console.WriteLine("Numbers in first array but not second array:");
            foreach (var n in aOnlyNumbers)
            {
                Console.WriteLine(n);
            }
        }

        public void Linq53()
        {
            List<Product> products = PublicData.instance.GetProductList();
            List<Customer> customers = PublicData.instance.GetCustomerList();

            var productFirstChars =
                from p in products
                select p.ProductName[0];
            var customerFirstChars =
                from c in customers
                select c.CompanyName[0];

            var productOnlyFirstChars = productFirstChars.Except(customerFirstChars);

            Console.WriteLine("First letters from Product names, but not from Customer names:");
            foreach (var ch in productOnlyFirstChars)
            {
                Console.WriteLine(ch);
            }
        }
    }
}
