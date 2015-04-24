using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.LearnLinq.Details;

namespace Cozy.LearnLinq
{
    class Quantifiers
    {
        public static void Cozy()
        {
            Quantifiers self = new Quantifiers();

            self.Linq67();
            self.Linq69();
            self.Linq70();
        }

        public void Linq67()
        {
            string[] words = { "believe", "relief", "receipt", "field" };
            bool iAfterE = words.Any(w => w.Contains("ei"));

            Console.WriteLine("There is a word that contains in the list that contains 'ei': {0}", iAfterE);
        }

        public void Linq69()
        {
            List<Product> products = PublicData.instance.GetProductList();
            var productGroups =
                from p in products
                group p by p.Category into g
                where g.Any(p => p.UnitsInStock == 0)
                select new { Category = g.Key, Products = g };

            ObjectDumper.Write(productGroups, 1);
        }

        public void Linq70()
        {
            int[] numbers = { 1, 11, 3, 19, 41, 65, 19 };
            bool onlyOdd = numbers.All(n => n % 2 == 1);

            Console.WriteLine("The list contains only odd numbers: {0}", onlyOdd);
        }

        public void Linq72()
        {
            List<Product> products = PublicData.instance.GetProductList();

            var productGroups =
                from p in products
                group p by p.Category into g
                where g.All(p => p.UnitsInStock > 0)
                select new { Category = g.Key, Products = g };

            ObjectDumper.Write(productGroups, 1);
        }
    }
}
