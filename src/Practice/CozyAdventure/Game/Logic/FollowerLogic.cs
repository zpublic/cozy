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
            // 基础战力 + (觉醒基础战力 + 觉醒战力成长 * 等级) * 成长细数
            int attack = f.BasicAttack;
            attack += (int)(FollowerStarLogic.GetAttack(f.CurStar, f.CurLevel) * f.GrowRatio);
            return attack;
        }

        public static bool CanUpgradeLevel(Follower f)
        {
            return true;
        }

        public static Package UpgradeLevelRequire(Follower f)
        {
            var p = new Package();
            p.Exp = 100;
            return p;
        }

        public static bool UpgradeLevel(Follower f)
        {
            f.CurLevel++;
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
