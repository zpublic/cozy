using CocosSharp;
using Cozy.Game.Manager;
using CozyAdventure.Game.Logic;
using CozyAdventure.Game.Manager;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
    public class LoginUiLayer : CCLayer
    {
        private bool IsExit { get; set; } = true;

        private ButtonEventDispatcher dispatcher { get; set; } = new ButtonEventDispatcher();

        protected override void AddedToScene()
        {
            base.AddedToScene();
            InitUI();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if(IsExit)
            {
                RegisterEvent();
                IsExit = false;
            }
            dispatcher.AttachListener(this);
        }

        public override void OnExit()
        {
            base.OnExit();
            dispatcher.DetachListener(this);
            if(IsExit)
            {
                UnregisterEvent();
                IsExit = false;
            }
        }

        private void InitUI()
        {
            var title = new CCLabel("冒险与编程", StringManager.GetText("GlobalFont"), 72)
            {
                Position    = new CCPoint(400, 320),
                Color       = CCColor3B.Yellow
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
            AddChild(begin, 100);
            dispatcher.Add(begin);

            var reg = new CozySampleButton(690, 0, 100, 50)
            {
                Text        = "注册帐号",
                FontSize    = 18,
                OnClick     = new Action(OnRegisterButton),
            };
            AddChild(reg, 100);
            dispatcher.Add(reg);

        }

        private void RegisterEvent()
        {
            MessageManager.RegisterMessage("MessageManager.Login.Success", OnLoginSuccess);
            MessageManager.RegisterMessage("Message.Login.Failed", OnLoginFailed);
        }

        private void UnregisterEvent()
        {
            MessageManager.UnRegisterMessage("Message.Login.Failed", OnLoginSuccess);
            MessageManager.UnRegisterMessage("MessageManager.Login.Success", OnLoginFailed);
        }

        public void OnBeginButtonDown()
        {
            AppDelegate.SharedWindow.DefaultDirector.PushScene(new LoadingScene(OnTimeOut, 10));
            UserLogic.Login("kingwl", "123456");
        }

        public void OnRegisterButton()
        {
            AppDelegate.SharedWindow.DefaultDirector.PushScene(new RegistScene());
        }

        private void OnLoginSuccess()
        {
            IsExit = true;
            AppDelegate.SharedWindow.DefaultDirector.ReplaceScene(new CampScene());
        }

        private void OnLoginFailed()
        {
            AppDelegate.SharedWindow.DefaultDirector.PopScene();
        }

        private void OnTimeOut()
        {

        }
    }
}
