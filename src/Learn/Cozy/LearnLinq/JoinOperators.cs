using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.LearnLinq.Details;

namespace Cozy.LearnLinq
{
    class JoinOperators
    {
        public static void Cozy()
        {
            JoinOperators self = new JoinOperators();

            self.Linq102();
            self.Linq103();
            self.Linq104();
            self.Linq105();
        }

        public void Linq102()
        {
            string[] categories = new string[]
            {  
                "Beverages",   
                "Condiments",   
                "Vegetables",   
                "Dairy Products",   
                "Seafood" 
            };

            List<Product> products = PublicData.instance.GetProductList();

            var q =
                from c in categories
                join p in products on c equals p.Category
                select new { Category = c, p.ProductName };

            foreach (var v in q)
            {
                Console.WriteLine(v.ProductName + ": " + v.Category);
            }
        }

        public void Linq103()
        {
            string[] categories = new string[]
            {  
                "Beverages",  
                "Condiments",  
                "Vegetables",  
                "Dairy Products",  
                "Seafood" 
            };

            List<Product> products = PublicData.instance.GetProductList();

            var q =
                from c in categories
                join p in products on c equals p.Category into ps
                select new { Category = c, Products = ps };

            foreach (var v in q)
            {
                Console.WriteLine(v.Category + ":");
                foreach (var p in v.Products)
                {
                    Console.WriteLine("   " + p.ProductName);
                }
            }
        }

        public void Linq104()
        {
            string[] categories = new string[]
            {   
                "Beverages",  
                "Condiments",  
                "Vegetables", 
                "Dairy Products",   
                "Seafood" 
            };

            List<Product> products = PublicData.instance.GetProductList();

            var q =
                from c in categories
                join p in products on c equals p.Category into ps
                from p in ps
                select new { Category = c, p.ProductName };

            foreach (var v in q)
            {
                Console.WriteLine(v.ProductName + ": " + v.Category);
            }
        }

        public void Linq105()
        {
            string[] categories = new string[]
            {   
                "Beverages",  
                "Condiments",   
                "Vegetables",   
                "Dairy Products",  
                "Seafood" 
            };

            List<Product> products = PublicData.instance.GetProductList();

            var q =
                from c in categories
                join p in products on c equals p.Category into ps
                from p in ps.DefaultIfEmpty()
                select new { Category = c, ProductName = p == null ? "(No products)" : p.ProductName };

            foreach (var v in q)
            {
                Console.WriteLine(v.ProductName + ": " + v.Category);
            }
        }
    }
}
