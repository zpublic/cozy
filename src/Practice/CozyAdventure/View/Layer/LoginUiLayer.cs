using CocosSharp;
using CocosSharpExt;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Scene;
using CozyAdventure.Game.Logic;
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

            var begin = new CozySampleButton(300, 100, 200, 80)
            {
                Text        = "开始游戏",
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
            AppDelegate.SharedWindow.DefaultDirector.PushScene(new LoadingScene());
            UserLogic.Login("kingwl", "123456");
        }

        public void OnRegisterButton()
        {
            AppDelegate.SharedWindow.DefaultDirector.PushScene(new RegistScene());
        }
    }
}
