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
                r.FollowerList.AddRange(follower.FollowerList.Select(x =>
                    {
                        return new KeyValuePair<int, int>(x, AdventurePluginDB.Follower.GetWithObjectId(x));
                    }));
                r.FightFollowerList.AddRange(follower.FightingFollowerList);

                var customer = AdventurePluginDB.Customer.GetPlayerCustomer(pullMsg.PlayerId);
                if(customer != null)
                {
                    r.Exp   = customer.Exp;
                    r.Money = customer.Money;
                }
            }

            SharedServer.SendMessage(r, im.SenderConnection);
        }

        private void GotoMapMessageImpl(NetIncomingMessage im, MessageBase msg)
        {
            var mapMsg      = msg as GotoMapMessage;
            var r           = new GotoResultMessage();

            if(AddFarmObj(im.SenderConnection, mapMsg.PlayerId, mapMsg.Money, mapMsg.Exp))
            {
                r.Result    = "Ok";
                r.GoToType  = GotoResultMessage.ToMap;
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
                r.Result        = "Ok";
                r.GoToType      = GotoResultMessage.ToHome;
                r.UserData      = homeMsg.UserData;
                var customer    = AdventurePluginDB.Customer.GetPlayerCustomer(homeMsg.PlayerId);
                if(customer != null)
                {
                    r.Exp   = customer.Exp;
                    r.Money = customer.Money;
                }
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
                r.Result        = "Ok";
                var follower    = AdventurePluginDB.PlayerFollower.GetPlayerFollower(hireMsg.PlayerId);

                var ObjectIdList = new List<int>();
                foreach (var id in hireMsg.FollowerId)
                {
                    var objId   = ObjectId;
                    r.Followers.Add(new KeyValuePair<int, int>(objId, id));
                    ObjectIdList.Add(objId);

                    var info = new FollowerInfo()
                    {
                        FollowerID  = id,
                        ObjectID    = objId,
                    };
                    AdventurePluginDB.Follower.Create(info);
                }

                follower.FollowerList.AddRange(ObjectIdList);
                AdventurePluginDB.PlayerFollower.Update(follower);
            }
            else
            {
                r.Result = "Error";
            }
            SharedServer.SendMessage(r, im.SenderConnection);
        }

        private void FightMessageImpl(NetIncomingMessage im, MessageBase msg)
        {
            var fightMsg    = msg as FightMessage;
            var r           = new FightResultMessage();

            if (AdventurePluginDB.User.Get(fightMsg.PlayerId) != null)
            {
                r.Result        = "Ok";
                r.ObjectId      = fightMsg.ObjectId;
                var follower    = AdventurePluginDB.PlayerFollower.GetPlayerFollower(fightMsg.PlayerId);

                if(follower.FollowerList.Contains(fightMsg.ObjectId))
                {
                    if(fightMsg.FightType == FightMessage.GoToFight)
                    {
                        follower.FightingFollowerList.Add(fightMsg.ObjectId);
                        r.StatusNow = FightMessage.GoToFight;
                    }
                    else
                    {
                        if(follower.FightingFollowerList.Contains(fightMsg.ObjectId))
                        {
                            follower.FightingFollowerList.Remove(fightMsg.ObjectId);
                            r.StatusNow = FightMessage.GoToRest;
                        }
                    }
                }
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
