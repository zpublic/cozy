using System;
using CocosSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.Game.Manager;
using CozyNetworkProtocol;

namespace CozyAdventure.View.Layer
{
    public class LoadingUiLayer : CCLayer
    {
        public CCSprite logo { get; set; }

        public CCLabel label { get; set; }

        public CCLabel load { get; set; }

        public int DotNumber { get; set; }

        // true -> cleanUp
        public Func<MessageBase, bool> MessageCallback { get; set; }

        public Action TimeOutCallback { get; set; }

        public LoadingUiLayer(Func<MessageBase, bool> msgCallback, Action timeoutCallback = null)
        {
            MessageCallback = msgCallback;
            TimeOutCallback = timeoutCallback;

            label = new CCLabel("加载中", "微软雅黑", 24)
            {
                Position = new CCPoint(381, 220),
            };
            AddChild(label, 100);

            load = new CCLabel("程序员正在加班写代码", "微软雅黑", 20)
            {
                AnchorPoint = CCPoint.Zero,
                Position    = new CCPoint(250, 150),
            };
            AddChild(load, 100);

            Schedule(OnChangeText, 1.0f);
            Schedule(OnTimeOut, 10.0f);

            MessageManager.RegisterMessage("Client.Data", OnMessage);
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
            if(MessageCallback != null)
            {
                if(MessageCallback(msg))
                {
                    CleanUp();
                }
            }
        }

        private void OnTimeOut(float dt)
        {
            if(TimeOutCallback != null)
            {
                TimeOutCallback();
            }
            CleanUp();
        }

        private void CleanUp()
        {
            MessageManager.UnRegisterMessage("Client.Data", OnMessage);
            Unschedule(OnTimeOut);
            Unschedule(OnChangeText);
        }
    }
}
