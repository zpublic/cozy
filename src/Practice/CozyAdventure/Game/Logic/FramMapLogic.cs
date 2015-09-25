using Cozy.Game.Manager;
using CozyAdventure.Engine;
using CozyAdventure.Protocol.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public class FramMapLogic
    {
        // 战斗力需求
        public static int Requirement(int level)
        {
            return (int)(double)ModuleManager.Instance.GetModule("FramMapModule").CallFunc("Requirement", level)[0];
        }

        // 每分钟经验
        public static int Exp(int level)
        {
            return (int)(double)ModuleManager.Instance.GetModule("FramMapModule").CallFunc("Exp", level)[0];
        }

        // 每分钟金币
        public static int Money(int level)
        {
            return (int)(double)ModuleManager.Instance.GetModule("FramMapModule").CallFunc("Money", level)[0];
        }

        // 进入地图
        public static void EnterMap(int level)
        {
            var msg = new GotoMapMessage()
            {
                Level = level,
            };

            MessageManager.SendMessage("Client.Send", msg);
        }

        // 离开地图
        public static void ExitMap(int level)
        {
            var msg = new GotoHomeMessage()
            {
                Level = level,
            };

            MessageManager.SendMessage("Client.Send", msg);
        }
    }
}
