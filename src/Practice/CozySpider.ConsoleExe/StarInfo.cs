using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.ConsoleExe
{
    public class StarInfo
    {
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Age { get; set; }
        public string Cup { get; set; }
        public string Chest { get; set; }
        public string Waist { get; set; }
        public string Hip { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if(Name != null)
            {
                builder.Append(" Name : " + Name);
            }
            if(Age != null)
            {
                builder.Append(" Age : " + Age);
            }
            if(Cup != null)
            {
                builder.Append(" Cup : " + Cup);
            }
            return builder.ToString();
        }
    }
}
