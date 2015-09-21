using CozyAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public class FollowerLogic
    {
        static List<int> baseAttack = new List<int>() { 0, 300, 1000, 2500, 5000, 9000 };
        static List<int> growAttack = new List<int>() { 10, 20, 30, 50, 100, 150 };
        // 每个星级最多可以升级20级，对应满级攻击力为：200、700、1600、3500、7000、12000

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
