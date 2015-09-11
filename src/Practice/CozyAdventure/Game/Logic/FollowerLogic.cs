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
        static List<int> baseAttack = new List<int>() { 0, 730, 2200, 8120, 17000 };
        static List<int> growAttack = new List<int>() { 20, 45, 180, 400, 1200 };

        static int GetGrowAttack(int star, int level)
        {
            return baseAttack[star] + growAttack[star] * level;
        }

        public static int GetAttack(Follower f)
        {
            // 基础战力 + (星级基础战力 + 星级战力成长 * 等级) * 成长细数
            int attack = f.BasicAttack;
            attack += (int)(GetGrowAttack(f.CurStar, f.CurLevel) * f.GrowRatio);
            return attack;
        }
    }
}
