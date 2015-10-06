using CozyAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public class FollowerStarLogic
    {
        public static bool CanUpgrade(Follower f)
        {
            return true;
        }

        public static Package UpgradeRequire(Follower f)
        {
            return null;
        }

        public static bool Upgrade(Follower f)
        {
            return true;
        }
    }
}
