using System;
using CocosSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharpExt;
using CozyAdventure.View.Scene;
using CozyAdventure.Game.Logic;
using CozyAdventure.Protocol;
using CozyAdventure.Protocol.Msg;
using CozyAdventure.View.Sprite;
using CozyAdventure.Model;
using Cozy.Game.Manager;
using CozyNetworkProtocol;
using CozyAdventure.Engine.Module.Data;
using CozyAdventure.Game.Object;

namespace CozyAdventure.View.Layer
{
    public class LoadingUiLayer : CCLayer
    {
        public CCSprite logo { get; set; }

        public CCLabel label { get; set; }

        public CCLabel load { get; set; }

        public int DotNumber { get; set; }

        public LoadingUiLayer()
        {

            var res = FollowerPackageModule.GetFollowerPackages();
            var node = new FollowerSprite(res.Followers[0], true)
            {
                Position = new CCPoint(10, 200)
            };
            this.AddChild(node, 201);

            label = new CCLabel("加载中", "微软雅黑", 24)
            {
                Position = new CCPoint(381, 220),
            };
            AddChild(label, 100);

            load = new CCLabel("程序员正在加班写代码", "微软雅黑", 20)
            {
                AnchorPoint = CCPoint.Zero,
                Position = new CCPoint(250, 150),
            };
            AddChild(load, 100);

            Schedule(OnChangeText, 1.0f);
            Schedule(OnTimeOut, 10.0f);

            MessageManager.RegisterMessage("Client.Data", OnMessage);

            UserLogic.Login("kingwl", "123456");
        }

        private void OnChangeText(float dt)
        {
            DotNumber = (DotNumber + 1) % 4;
            RefreshDot();
        }

        private void RefreshDot()
        {
            var result = "程序员正在加班写代码";
            for (int i = 0; i < DotNumber; ++i)
            {
                result += '.';
            }
            load.Text = result;
        }

        private void OnMessage(object obj)
        {
            var msg = (MessageBase)obj;
            if(msg.Id == (uint)MessageId.Inner.LoginResultMessage)
            {
                OnLoginRspMessage((LoginResultMessage)msg);
            }
            else if(msg.Id == (uint)MessageId.User.PushMessage)
            {
                OnPushMessage((PushMessage)msg);
            }
        }

        private void OnTimeOut(float dt)
        {
            CleanUp();
            AppDelegate.SharedWindow.DefaultDirector.PopScene();
        }

        private void OnLoginRspMessage(LoginResultMessage msg)
        {
            if (msg.Result == "OK")
            {
                var sendMsg = new PullMessage();
                MessageManager.SendMessage("Client.Send", sendMsg);
            }
            else if (msg.Result == "Error")
            {
                CleanUp();
                AppDelegate.SharedWindow.DefaultDirector.PopScene();
            }
        }

        private void OnPushMessage(PushMessage msg)
        {
            PlayerObject.Instance.Self.AllFollower.Followers = msg.FollowerList;

            CleanUp();
            AppDelegate.SharedWindow.DefaultDirector.ReplaceScene(new FollowerListScene());
        }

        private void CleanUp()
        {
            MessageManager.UnRegisterMessage("Client.Data", OnMessage);
            Unschedule(OnTimeOut);
            Unschedule(OnChangeText);
        }
    }
}
