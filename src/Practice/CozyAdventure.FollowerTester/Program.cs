using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Game.Model;
using CozyAdventure.Game.Logic;
using CozyAdventure.Game.Object;

namespace CozyAdventure.FollowerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerObject.Instance.Self.Money = 99999;
            PlayerObject.Instance.Self.Exp = 99999;

            Follower f1 = new Follower();
            Console.WriteLine(FollowerLogic.GetAttack(f1));

            var p = FollowerLogic.UpgradeLevelRequire(f1);
            if (p != null)
            {
                Console.WriteLine(p.Exp);
                Console.WriteLine(p.Money);
            }

            FollowerLogic.UpgradeLevel(f1);
            Console.WriteLine(FollowerLogic.GetAttack(f1));

            var p2 = FollowerLogic.UpgradeLevelRequire(f1);
            if (p2 != null)
            {
                Console.WriteLine(p2.Exp);
                Console.WriteLine(p2.Money);
            }
        }
    }
}
