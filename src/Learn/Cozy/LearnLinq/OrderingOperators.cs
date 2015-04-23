using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.LearnLinq.Details;
using Cozy.LearnLinq.Details.Compare;

namespace Cozy.LearnLinq
{
    class OrderingOperators
    {
        public static void Cozy()
        {
            OrderingOperators self = new OrderingOperators();

            self.Linq28();
            self.Linq29();
            self.Linq30();
            self.Linq31();
            self.Linq32();
            self.Linq33();
            self.Linq34();
            self.Linq35();
            self.Linq36();
            self.Linq37();
            self.Linq38();
            self.Linq39();
        }

        public void Linq28()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            var sortedWords =
                from w in words
                orderby w
                select w;

            Console.WriteLine("The sorted list of words:");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }
        }

        public void Linq29()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            var sortedWords =
                from w in words
                orderby w.Length
                select w;

            Console.WriteLine("The sorted list of words (by length):");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }
        }

        public void Linq30()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var sortedProducts =
                from p in products
                orderby p.ProductName
                select p;

            ObjectDumper.Write(sortedProducts);
        }

        public void Linq31()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());

            ObjectDumper.Write(sortedWords);
        }

        public void Linq32()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            var sortedDoubles =
                from d in doubles
                orderby d descending
                select d;

            Console.WriteLine("The doubles from highest to lowest:");
            foreach (var d in sortedDoubles)
            {
                Console.WriteLine(d);
            }
        }

        public void Linq33()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var sortedProducts =
                from p in products
                orderby p.UnitsInStock descending
                select p;

            ObjectDumper.Write(sortedProducts);
        }

        public void Linq34()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());

            ObjectDumper.Write(sortedWords);
        }

        public void Linq35()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits =
                from d in digits
                orderby d.Length, d
                select d;

            Console.WriteLine("Sorted digits:");
            foreach (var d in sortedDigits)
            {
                Console.WriteLine(d);
            }
        }

        public void Linq36()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords =
                words.OrderBy(a => a.Length)
                     .ThenBy(a => a, new CaseInsensitiveComparer());

            ObjectDumper.Write(sortedWords);
        }

        public void Linq37()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var sortedProducts =
                from p in products
                orderby p.Category, p.UnitPrice descending
                select p;

            ObjectDumper.Write(sortedProducts);
        }

        public void Linq38()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords =
                words.OrderBy(a => a.Length)
                     .ThenByDescending(a => a, new CaseInsensitiveComparer());

            ObjectDumper.Write(sortedWords);
        }

        public void Linq39()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var reversedIDigits = (
                from d in digits
                where d[1] == 'i'
                select d)
                .Reverse();

            Console.WriteLine("A backwards list of the digits with a second character of 'i':");
            foreach (var d in reversedIDigits)
            {
                Console.WriteLine(d);
            }
        }
    }
}
