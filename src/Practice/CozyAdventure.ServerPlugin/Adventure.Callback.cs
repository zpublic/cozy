using CozyAdventure.Protocol.Msg;
using CozyNetworkHelper;
using CozyNetworkProtocol;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.ServerPlugin.Model;

namespace CozyAdventure.ServerPlugin
{
    public partial class AdventurePlugin
    {
        [CallBack(typeof(RegisterMessage))]
        public void OnRegisterMessage(NetPeer server, NetBuffer buff, MessageBase msg)
        {
            var srv = server as NetServer;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                RegisterMessageImpl(srv, im, msg);
            }
        }

        private void RegisterMessageImpl(NetServer server, NetIncomingMessage im, MessageBase msg)
        {
            var registerMsg = msg as RegisterMessage;
            var r = new RegisterResultMessage();

            if (AdventurePluginDB.User.Get(registerMsg.Name, registerMsg.Pass) == null)
            {
                var user = new UserInfo
                {
                    Name = registerMsg.Name,
                    Pass = registerMsg.Pass,
                };
                AdventurePluginDB.User.Create(user);
                r.Result = "OK";
            }
            else
            {
                r.Result = "Error";
            }
            server.SendMessage(r, im.SenderConnection);
        }

        [CallBack(typeof(LoginMessage))]
        public void OnLoginMessage(NetPeer server, NetBuffer buff, MessageBase msg)
        {
            var srv = server as NetServer;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                LoginMessageImpl(srv, im, msg);
            }
        }

        private void LoginMessageImpl(NetServer server, NetIncomingMessage im, MessageBase msg)
        {
            var registerMsg = msg as LoginMessage;
            var r = new LoginResultMessage();

            var user = AdventurePluginDB.User.Get(registerMsg.Name, registerMsg.Pass);
            if (user != null)
            {
                r.Result = "OK";
                r.UserId = user.id;
            }
            else
            {
                r.Result = "Error";
            }
            server.SendMessage(r, im.SenderConnection);
        }
    }
}
