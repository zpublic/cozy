using CozyAdventure.Protocol.Msg;
using CozyAdventure.ServerPlugin.Model;
using CozyNetworkHelper;
using CozyNetworkProtocol;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.ServerPlugin
{
    public partial class AdventurePlugin
    {
        private void RegisterMessageImpl(NetIncomingMessage im, MessageBase msg)
        {
            var registerMsg = msg as RegisterMessage;
            var r           = new RegisterResultMessage();

            if (AdventurePluginDB.User.Get(registerMsg.Name, registerMsg.Pass) == null)
            {
                var user = new UserInfo
                {
                    Name = registerMsg.Name,
                    Pass = registerMsg.Pass,
                };
                var id = AdventurePluginDB.User.Create(user);

                AdventurePluginDB.Customer.Create(new CustomerInfo() { PlayerId = id, });
                AdventurePluginDB.PlayerFollower.Create(new PlayerFollowerInfo() { PlayerId = id, });

                r.Result = "OK";
            }
            else
            {
                r.Result = "Error";
            }
            SharedServer.SendMessage(r, im.SenderConnection);
        }

        private void LoginMessageImpl(NetIncomingMessage im, MessageBase msg)
        {
            var loginMsg    = msg as LoginMessage;
            var r           = new LoginResultMessage();

            var user = AdventurePluginDB.User.Get(loginMsg.Name, loginMsg.Pass);
            if (user != null)
            {
                r.Result = "OK";
                r.PlayerId = user.id;
            }
            else
            {
                r.Result = "Error";
            }
            SharedServer.SendMessage(r, im.SenderConnection);
        }

        private void PullMessageImpl(NetIncomingMessage im, MessageBase msg)
        {
            var pullMsg     = msg as PullMessage;
            var r           = new PushMessage();

            if (AdventurePluginDB.PlayerFollower.IsPlayerExist(pullMsg.PlayerId))
            {
                var follower = AdventurePluginDB.PlayerFollower.GetPlayerFollower(pullMsg.PlayerId);
                r.FollowerList.AddRange(follower.FollowerList);
            }

            SharedServer.SendMessage(r, im.SenderConnection);
        }

        private void GotoMapMessageImpl(NetIncomingMessage im, MessageBase msg)
        {
            var mapMsg      = msg as GotoMapMessage;
            var r           = new GotoResultMessage();

            if(AddFarmObj(im.SenderConnection, mapMsg.Money, mapMsg.Exp))
            {
                r.Result = "Ok";
            }
            else
            {
                r.Result = "Error";
            }

            SharedServer.SendMessage(r, im.SenderConnection);
        }

        private void GotoHomeMessageImpl(NetIncomingMessage im, MessageBase msg)
        {
            var homeMsg     = msg as GotoHomeMessage;
            var r           = new GotoResultMessage();

            if (RemoveFarmObj(im.SenderConnection))
            {
                r.Result = "Ok";
            }
            else
            {
                r.Result = "Error";
            }

            SharedServer.SendMessage(r, im.SenderConnection);
        }

        private void HireFollowerMessageImpl(NetIncomingMessage im, MessageBase msg)
        {
            var hireMsg     = msg as HireFollowerMessage;
            var r           = new HireResultMessage();

            if(AdventurePluginDB.User.Get(hireMsg.PlayerId) != null)
            {
                r.Result = "Ok";

                var follower = AdventurePluginDB.PlayerFollower.GetPlayerFollower(hireMsg.PlayerId);
                follower.FollowerList.AddRange(hireMsg.FollowerId);
                AdventurePluginDB.PlayerFollower.Update(follower);
            }
            else
            {
                r.Result = "Error";
            }
            SharedServer.SendMessage(r, im.SenderConnection);
        }
    }
}
