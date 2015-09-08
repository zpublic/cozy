using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCoderAlmanac.Core
{
    public class Special
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public Special(DateTime date, string type, string name, string desc)
        {
            Date = date;
            Type = type;
            Name = name;
            Desc = desc;
        }
    }
}
