using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Protocol.Msg;

namespace CozyAdventure.Game.Logic
{
    public class UserLogic
    {
        public static void Login(string user, string pass)
        {
            var msg = new LoginMessage()
            {
                Name = user,
                Pass = pass,
            };
            AppDelegate.SharedClient.SendMessage(msg);
        }

        public static void Regist(string user, string pass, string nickname)
        {
        }
    }
}
