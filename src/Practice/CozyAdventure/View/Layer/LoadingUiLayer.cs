using System;
using CocosSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cozy.Game.Manager;
using CozyNetworkProtocol;
using CozyAdventure.Game.Manager;

namespace CozyAdventure.View.Layer
{
    /// <summary>
    /// 管理注册的消息的生命周期
    /// </summary>
    public class LoadingUiLayer : CCLayer
    {
        public CCSprite logo { get; set; }

        public CCLabel label { get; set; }

        public CCLabel load { get; set; }

        public int DotNumber { get; set; }

        /// <summary>
        /// 超时
        /// </summary>
        public Action TimeOutCallback { get; set; }

        public override void OnEnter()
        {
            base.OnEnter();
            InitUI();
        }

        public override void OnExit()
        {
            base.OnExit();
            Unschedule(OnTimeOut);
            Unschedule(OnChangeText);
        }

        public LoadingUiLayer(Action timeoutCallback = null, int timeout = 10)
        {
            TimeOutCallback = timeoutCallback;

            RegisterSchedule(timeout);
        }

        private void RegisterSchedule(int timeout)
        {
            Schedule(OnChangeText, 1.0f);
            Schedule(OnTimeOut, timeout);
        }

        private void InitUI()
        {
            label = new CCLabel("加载中", StringManager.GetText("GlobalFont"), 24)
            {
                Position = new CCPoint(381, 220),
            };
            AddChild(label, 100);

            load = new CCLabel("程序员正在加班写代码", StringManager.GetText("GlobalFont"), 20)
            {
                AnchorPoint = CCPoint.Zero,
                Position    = new CCPoint(250, 150),
            };
            AddChild(load, 100);
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

        /// <summary>
        /// 自动popScene 执行callback
        /// </summary>
        /// <param name="dt"></param>
        private void OnTimeOut(float dt)
        {
            if(TimeOutCallback != null)
            {
                TimeOutCallback();
                AppDelegate.SharedWindow.DefaultDirector.PopScene();
            }
        }
    }
}
