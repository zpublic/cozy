using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.LearnLinq.Details;

namespace Cozy.LearnLinq
{
    class ElementOperators
    {
        public static void Cozy()
        {
            ElementOperators self = new ElementOperators();

            self.Linq58();
            self.Linq59();
            self.Linq61();
            self.Linq62();
        }

        public void Linq58()
        {
            List<Product> products = PublicData.instance.GetProductList();

            Product product12 = (
                from p in products
                where p.ProductID == 12
                select p)
                .First();

            ObjectDumper.Write(product12);
        }

        public void Linq59()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string startsWithO = strings.First(s => s[0] == 'o');

            Console.WriteLine("A string starting with 'o': {0}", startsWithO);
        }

        public void Linq61()
        {
            int[] numbers = { };
            int firstNumOrDefault = numbers.FirstOrDefault();

            Console.WriteLine(firstNumOrDefault);
        }

        public void Linq62()
        {
            List<Product> products = PublicData.instance.GetProductList();

            Product product789 = products.FirstOrDefault(p => p.ProductID == 789);

            Console.WriteLine("Product 789 exists: {0}", product789 != null);
        }
    }
}
