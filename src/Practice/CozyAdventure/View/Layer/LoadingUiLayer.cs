using System;
using CocosSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharpExt;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Scene;
using CozyAdventure.Game.Logic;
using CozyAdventure.Events;
using CozyAdventure.Protocol;
using CozyAdventure.Protocol.Msg;


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
            //logo = new CCSprite("@face.png")
            //{
            //    Position = new CCPoint(381, 154),
            //};
            //AddChild(logo);

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

            AppDelegate.MessageReceiveEventHandler += OnMessage;

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

        private void OnMessage(object sender, MessageReceiveEventArgs e)
        {
            if(e.Id == (uint)MessageId.Inner.LoginResultMessage)
            {
                var rsp = (LoginResultMessage)e.Msg;
                if (rsp.Result == "OK")
                {
                    OnSuccess();
                }
                else if(rsp.Result == "Error")
                {
                    OnFailed();
                }
            }
        }

        private void OnTimeOut(float dt)
        {
            CleanUp();
            AppDelegate.SharedWindow.DefaultDirector.PopScene();
        }

        private void OnSuccess()
        {
            CleanUp();
            AppDelegate.SharedWindow.DefaultDirector.ReplaceScene(new AdventureScene());
        }

        private void OnFailed()
        {
            CleanUp();
            AppDelegate.SharedWindow.DefaultDirector.PopScene();
        }

        private void CleanUp()
        {
            AppDelegate.MessageReceiveEventHandler -= OnMessage;
            Unschedule(OnTimeOut);
        }
    }
}
