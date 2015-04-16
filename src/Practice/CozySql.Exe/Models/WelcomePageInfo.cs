using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Exe.Models
{
    public class WelcomePageInfo
    {
        public List<WelcomePageBlockInfo> Elemts { get; set; }

        public WelcomePageInfo()
        {
            Elemts = new List<WelcomePageBlockInfo>();
        }
    }
}
