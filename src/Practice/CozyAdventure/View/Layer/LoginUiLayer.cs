using CocosSharp;
using CocosSharpExt;
using Cozy.Game.Manager;
using CozyAdventure.Engine.Module.Data;
using CozyAdventure.Game.Logic;
using CozyAdventure.Game.Manager;
using CozyAdventure.Game.Object;
using CozyAdventure.Protocol;
using CozyAdventure.Protocol.Msg;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Scene;
using CozyNetworkProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
    public class LoginUiLayer : CCLayer
    {
        public LoginUiLayer()
        {
            var title = new CCLabel("冒险与编程", "微软雅黑", 72)
            {
                Position = new CCPoint(400, 320),
                Color = CCColor3B.Yellow
            };
            AddChild(title, 100);

            var begin = new CozySampleButton(200, 100, 200, 80)
            {
                // 测试代码
                // Text        = "开始游戏",
                Text        = StringManager.GetText("str3"),
                FontSize    = 24,
                OnClick     = new Action(OnBeginButtonDown),
            };
            AddEventListener(begin.EventListener);
            AddChild(begin, 100);

            var reg = new CozySampleButton(690, 0, 100, 50)
            {
                Text        = "注册帐号",
                FontSize    = 18,
                OnClick     = new Action(OnRegisterButton),
            };
            AddEventListener(reg.EventListener);
            AddChild(reg, 100);
        }

        public void OnBeginButtonDown()
        {
            AppDelegate.SharedWindow.DefaultDirector.PushScene(new LoadingScene(OnMessage, OnTimeOut, 10));
            UserLogic.Login("kingwl", "123456");
        }

        public void OnRegisterButton()
        {
            UserLogic.Regist("kingwl", "123456", "hehe");
            //AppDelegate.SharedWindow.DefaultDirector.PushScene(new RegistScene());
        }

        private bool OnMessage(MessageBase msg)
        {
            if (msg.Id == (uint)MessageId.Inner.LoginResultMessage)
            {
                return OnLoginRspMessage((LoginResultMessage)msg);
            }
            else if (msg.Id == (uint)MessageId.User.PushMessage)
            {
                return OnPushMessage((PushMessage)msg);
            }
            else if(msg.Id == (uint)MessageId.Mercenary.HireResultMessage)
            {
                return OnHireResultMessage((HireResultMessage)msg);
            }
            return false;
        }

        private bool OnLoginRspMessage(LoginResultMessage msg)
        {
            if (msg.Result == "OK")
            {
                PlayerObject.Instance.Self.PlayerId = msg.PlayerId;

                var sendMsg = new PullMessage()
                {
                    PlayerId = msg.PlayerId,
                };

                MessageManager.SendMessage("Client.Send", sendMsg);
            }
            else if (msg.Result == "Error")
            {
                AppDelegate.SharedWindow.DefaultDirector.PopScene();
                return true;
            }
            return false;
        }

        private bool OnHireResultMessage(HireResultMessage msg)
        {
            var res = FollowerPackageModule.GetFollowerPackages();
            if (msg.Result == "Ok")
            {
                foreach (var obj in msg.Followers)
                {
                    var follower = res.GetFollowerById(obj.Value, obj.Key);
                    PlayerObject.Instance.Self.AllFollower.Followers.Add(follower);
                    FollowerObjectManager.Instance.AddObj(follower.ObjectId, follower);
                }
            }
            return false;
        }

        private bool OnPushMessage(PushMessage msg)
        {
            var res     = FollowerPackageModule.GetFollowerPackages();
            var resId   = res.Followers.Select(x => x.Id);
            var result  = msg.FollowerList.Where(x => resId.Contains(x.Value)).Select(x => res.GetFollowerById(x.Value, x.Key));
            var ftres   = msg.FightFollowerList.Where(x => resId.Contains(x));

            PlayerObject.Instance.Self.AllFollower.Followers    = result.ToList();
            PlayerObject.Instance.Self.Money                    = msg.Money;
            PlayerObject.Instance.Self.Exp                      = msg.Exp;

            PlayerObject.Instance.Self.FightFollower.Followers.Clear();
            foreach (var obj in PlayerObject.Instance.Self.AllFollower.Followers)
            {
                if(ftres.Contains(obj.Id))
                {
                    obj.IsFighting = true;
                    PlayerObject.Instance.Self.FightFollower.Followers.Add(obj);
                }
            }

            foreach (var obj in PlayerObject.Instance.Self.AllFollower.Followers)
            {
                FollowerObjectManager.Instance.AddObj(obj.ObjectId, obj);
            }

            AppDelegate.SharedWindow.DefaultDirector.ReplaceScene(new CampScene());
            return true;
        }

        private void OnTimeOut()
        {
            AppDelegate.SharedWindow.DefaultDirector.PopScene();
        }
    }
}
