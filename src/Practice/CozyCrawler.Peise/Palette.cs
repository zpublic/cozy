using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Peise
{
    public class Palette
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public List<string> RGB { get; set; } = new List<string>();
    }
}
