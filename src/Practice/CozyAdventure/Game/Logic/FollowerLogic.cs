using CozyAdventure.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public class FollowerLogic
    {
        public static int GetAttack(Follower f)
        {
            return 1;
        }

        public static bool CanUpgradeLevel(Follower f)
        {
            return true;
        }

        public static Package UpgradeLevelRequire(Follower f)
        {
            return null;
        }

        public static bool UpgradeLevel(Follower f)
        {
            return true;
        }

        public static bool CanUpgradeStar(Follower f)
        {
            return true;
        }

        public static Package UpgradeStarRequire(Follower f)
        {
            return null;
        }

        public static bool UpgradeStarLevel(Follower f)
        {
            return true;
        }
    }
}
