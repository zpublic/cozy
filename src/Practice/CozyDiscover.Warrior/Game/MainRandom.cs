using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDiscover.Warrior.Game
{
    public static class MainRandom
    {
        static Random random = new Random();

        public static int Gen()
        {
            return random.Next();
        }

        public static int Gen(int max)
        {
            return random.Next(max);
        }

        public static int Gen(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
