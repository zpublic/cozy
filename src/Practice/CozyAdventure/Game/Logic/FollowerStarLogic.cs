using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    class FollowerStarLogic
    {
        static List<int> baseAttack = new List<int>() { 0, 730, 2200, 8120, 17000 };
        static List<int> growAttack = new List<int>() { 20, 45, 180, 400, 1200 };

        public static int GetAttack(int star, int level)
        {
            return baseAttack[star] + growAttack[star] * level;
        }
    }
}
