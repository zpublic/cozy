using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.ServerPlugin.Model
{
    public class CustomerInfo
    {
        public int id { get; set; }

        public int PlayerId { get; set; }

        public int Exp { get; set; }

        public int Money { get; set; }
    }
}
