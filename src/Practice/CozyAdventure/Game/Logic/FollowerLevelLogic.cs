using CozyAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public class FollowerLevelLogic
    {
        public static bool CanUpgrade(Follower f)
        {
            return true;
        }

        public static Package UpgradeRequire(Follower f)
        {
            var p = new Package();
            p.Exp = 100;
            return p;
        }

        public static bool Upgrade(Follower f)
        {
            f.CurLevel++;
            return true;
        }
    }
}
