using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreCozy.Extensions;

namespace StoreCozy.Model
{
    public class MenuItem : BindableBase
    {
        private string text;
        public string Text
        { 
            get 
            { 
                return text; 
            } 
            set 
            { 
                SetProperty(ref text, value);
            } 
        }

        public string ImagePath { get; set; }

        private double price;
        public double Price { 
            get 
            { 
                return price; 
            } 
            set 
            { 
                SetProperty(ref price, value); 
            } 
        }

        public MenuCard MenuCard { get; set; }
    }
}
