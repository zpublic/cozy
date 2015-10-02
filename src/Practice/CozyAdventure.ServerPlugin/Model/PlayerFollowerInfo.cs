using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.ServerPlugin.Model
{
    public class PlayerFollowerInfo
    {
        public int id { get; set; }

        public int PlayerId { get; set; }

        public List<int> FollowerList { get; set; } = new List<int>();

        public List<int> FightingFollowerList { get; set; } = new List<int>();
    }
}
