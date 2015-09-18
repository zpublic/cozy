using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Engine;
using CozyAdventure.Game.Model;

namespace CozyAdventure.EngineTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ModuleManager.Instance.Init();

            var FollowerLevelModule = ModuleManager.Instance.GetModule("FollowerLevelModule");
            var FollowerModule = ModuleManager.Instance.GetModule("FollowerModule");
            var FollowerStarModule = ModuleManager.Instance.GetModule("FollowerStarModule");

            var follower = new Follower()
            {
                CurStar = 1,
                CurLevel = 2,
            };

            //-------------------------FollowerLevelModule-------------------------------
            Console.WriteLine((FollowerLevelModule.CallFunc("CanUpgrade", follower)[0].ToString()));
            Console.WriteLine(((Package)FollowerLevelModule.CallFunc("UpgradeRequire", follower)[0]).Exp);
            Console.WriteLine(FollowerLevelModule.CallFunc("Upgrade", follower)[0].ToString());
            Console.WriteLine(((Package)FollowerLevelModule.CallFunc("UpgradeRequire", follower)[0]).Exp);

            //-------------------------FollowerModule------------------------------------
            Console.WriteLine(FollowerModule.CallFunc("GetAttack", follower)[0].ToString());

            //-------------------------FollowerModule------------------------------------
            Console.WriteLine(follower.CurStar + " " + follower.CurLevel);
            Console.WriteLine(FollowerStarModule.CallFunc("CanUpgrade", follower)[0].ToString());
            follower.CurLevel = 31;
            Console.WriteLine(FollowerStarModule.CallFunc("CanUpgrade", follower)[0].ToString());
            Console.WriteLine(((Package)FollowerStarModule.CallFunc("UpgradeRequire", follower)[0]).Exp);
            Console.WriteLine(((Package)FollowerStarModule.CallFunc("UpgradeRequire", follower)[0]).Money);
            Console.WriteLine(FollowerStarModule.CallFunc("Upgrade", follower)[0].ToString());

            Console.WriteLine(follower.CurStar + " " + follower.CurLevel);

            Console.ReadKey();
        }
    }
}
