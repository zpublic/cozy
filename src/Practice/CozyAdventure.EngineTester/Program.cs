using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Engine;

namespace CozyAdventure.EngineTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ModuleManager.Instance.Init();

            var module = ModuleManager.Instance.GetModule("FollowerLevelModule");

            var resule = module.CallFunc("CanUpgrade")[0];

            Console.WriteLine(resule.ToString());
            Console.ReadKey();
        }
    }
}
