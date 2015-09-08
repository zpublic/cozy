using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCoderAlmanac.Core
{
    class Activity
    {
        public Activity(string name, string good, string bad, bool weekend = false)
        {
            Name = name;
            Good = good;
            Bad = bad;
            Weekend = weekend;
        }

        public string Name { get; set; }
        public string Good { get; set; }
        public string Bad { get; set; }
        public bool Weekend { get; set; } = false;
    }
}
