using CozyAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public class FollowerCollectLogic
    {
        public static int GetAttack(FollowerCollect fc)
        {
            int attack = 0;
            foreach (var i in fc.Followers)
            {
                attack += FollowerLogic.GetAttack(i);
            }
            return attack;
        }

        // 血量等于攻击力
        public static int GetHp(FollowerCollect fc)
        {
            return GetAttack(fc);
        }
    }
}
