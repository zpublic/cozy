using CozyAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Object
{
    public class BagObject
    {
        private BagObject() { }
        public static readonly BagObject Instance = new BagObject();
        
        public PropCollect Props { get; } = new PropCollect();
    }
}
