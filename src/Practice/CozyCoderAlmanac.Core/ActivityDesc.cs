using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCoderAlmanac.Core
{
    public class ActivityDesc
    {
        public ActivityDesc(string name, string desc)
        {
            Name = name;
            Desc = desc;
        }

        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
