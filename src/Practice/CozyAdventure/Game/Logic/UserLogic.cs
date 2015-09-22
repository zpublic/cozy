using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Protocol.Msg;
using Cozy.Game.Manager;

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
            MessageManager.SendMessage("Client.Send", msg);
        }

        public static void Regist(string user, string pass, string nickname)
        {
        }
    }
}
