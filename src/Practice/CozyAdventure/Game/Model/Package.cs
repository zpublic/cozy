using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Model
{
    public class Package
    {
        public int Money { get; set; } = 0;
        public int Exp { get; set; } = 0;
        public PropCollect Props { get; set; }
    }
}
