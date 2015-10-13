using Cozy.Game.Manager;
using CozyAdventure.Engine.Module.Data;
using CozyAdventure.Game.Object;
using CozyAdventure.Model;
using CozyAdventure.Protocol;
using CozyAdventure.Protocol.Msg;
using CozyNetworkProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Manager
{
    public class NetworkMessageManager
    {
        private static NetworkMessageManager _Instance = new NetworkMessageManager();
        public static NetworkMessageManager Instance { get { return _Instance; } }

        public const string OkTag       = "Ok";
        public const string ErrorTag    = "Error";

        public void Init()
        {
            MessageManager.RegisterMessage("Client.Data", OnMessage);
        }

        private void OnMessage(object obj)
        {
            var msg = obj as MessageBase;
            switch(msg.Id)
            {
                case (uint)MessageId.Inner.LoginResultMessage:
                    OnLoginRspMessage((LoginResultMessage)msg);
                    break;
                case (uint)MessageId.User.PushMessage:
                    OnPushMessage((PushMessage)msg);
                    break;
                case (uint)MessageId.Mercenary.HireResultMessage:
                    OnHireResultMessage((HireResultMessage)msg);
                    break;
                case (uint)MessageId.Mercenary.FightResultMessage:
                    OnFightMessage((FightResultMessage)msg);
                    break;
                case (uint)MessageId.Farm.FarmIncomeMessage:
                    OnFarmIncommingMessage((FarmIncomeMessage)msg);
                    break;
                case (uint)MessageId.Farm.GotoResultMessage:
                    OnGotoResultMessage((GotoResultMessage)msg);
                    break;
                case (uint)MessageId.Inner.RegisterResultMessage:
                    OnRegisterResultMessage((RegisterResultMessage)msg);
                    break;
                default:
                    break;
            }
        }

        private void OnLoginRspMessage(LoginResultMessage msg)
        {
            if (msg.Result == OkTag)
            {
                PlayerObject.Instance.Self.PlayerId = msg.PlayerId;

                var sendMsg = new PullMessage()
                {
                    PlayerId = msg.PlayerId,
                };

                MessageManager.SendMessage("Client.Send", sendMsg);
            }
            else if (msg.Result == ErrorTag)
            {
                MessageManager.SendMessage("Message.Login.Failed");
            }
        }

        private void OnPushMessage(PushMessage msg)
        {
            var res     = FollowerPackageModule.GetFollowerPackages();
            var resId   = res.Followers.Select(x => x.Id);
            var result  = msg.FollowerList.Where(x => resId.Contains(x.Value)).Select(x => res.GetFollowerById(x.Value, x.Key));
            var ftres   = msg.FightFollowerList.Where(x => resId.Contains(x));

            SyncPlayerInfo(result.ToList(), ftres.ToList(), msg.Exp, msg.Money);

            foreach (var obj in PlayerObject.Instance.Self.AllFollower.Followers)
            {
                FollowerObjectManager.Instance.AddObj(obj.ObjectId, obj);
            }
            MessageManager.SendMessage("MessageManager.Login.Success");
        }

        private void SyncPlayerInfo(List<Follower> followers, List<int> fightId, long Exp, long Money)
        {
            if (followers != null)
            {
                PlayerObject.Instance.Self.AllFollower.Followers = followers;
                PlayerObject.Instance.Self.FightFollower.Followers.Clear();
                foreach (var obj in PlayerObject.Instance.Self.AllFollower.Followers)
                {
                    if (fightId.Contains(obj.Id))
                    {
                        obj.IsFighting = true;
                        PlayerObject.Instance.Self.FightFollower.Followers.Add(obj);
                    }
                }
            }

            PlayerObject.Instance.Self.Money    = Money;
            PlayerObject.Instance.Self.Exp      = Exp;
        }

        private void OnHireResultMessage(HireResultMessage msg)
        {
            var res = FollowerPackageModule.GetFollowerPackages();
            if (msg.Result == OkTag)
            {
                foreach (var obj in msg.Followers)
                {
                    var follower = res.GetFollowerById(obj.Value, obj.Key);
                    PlayerObject.Instance.Self.AllFollower.Followers.Add(follower);
                    FollowerObjectManager.Instance.AddObj(follower.ObjectId, follower);
                }
                MessageManager.SendMessage("Message.HireFollower.Success");
            }
            else
            {
                MessageManager.SendMessage("Message.HireFollower.Failed");
            }
        }

        private void OnFightMessage(FightResultMessage msg)
        {
            if (msg.Result == OkTag)
            {
                var follower = (Follower)FollowerObjectManager.Instance.GetObj(msg.ObjectId);
                if (msg.StatusNow == FightMessage.GoToFight)
                {
                    PlayerObject.Instance.Self.FightFollower.Followers.Add(follower);
                    follower.IsFighting = true;
                }
                else
                {
                    if (PlayerObject.Instance.Self.FightFollower.Followers.Contains(follower))
                    {
                        PlayerObject.Instance.Self.FightFollower.Followers.Remove(follower);
                        follower.IsFighting = false;
                    }
                }
                MessageManager.SendMessage("Message.FollowerFight.Success");
            }
            else
            {
                MessageManager.SendMessage("Message.FollowerFight.Failed");
            }
        }

        private void OnFarmIncommingMessage(FarmIncomeMessage msg)
        {
            SyncPlayerInfo(null, null, msg.Exp, msg.Money);
            MessageManager.SendMessage("Message.FarmIncoming.Data");
        }

        private void OnGotoResultMessage(GotoResultMessage msg)
        {
            if(msg.GoToType == GotoResultMessage.ToHome)
            {
                SyncPlayerInfo(null, null, msg.Exp, msg.Money);

                if (msg.UserData == "Leave")
                {
                    MessageManager.SendMessage("Message.GotoHome.Leave");
                }
                else if (msg.UserData == "Camp")
                {
                    MessageManager.SendMessage("Message.GotoHome.Camp");
                }
            }
            else if (msg.GoToType == GotoResultMessage.ToMap)
            {
                MessageManager.SendMessage("Message.GotoMap.Success", msg.Level);
            }
        }

        private void OnRegisterResultMessage(RegisterResultMessage msg)
        {
            if (msg.Result == OkTag)
            {
                MessageManager.SendMessage("Message.Register.Success");
            }
            else
            {
                MessageManager.SendMessage("Message.Register.Failed");
            }
        }
    }
}
