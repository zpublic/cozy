using CocosSharp;
using CocosSharpExt;
using CozyAdventure.Public.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Scene
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

            var edit = new CocosSharpExt.CCTextField("aaaaaaa", "微软雅黑", 22)
            {
                Position = new CCPoint(100, 120),
                Color = CCColor3B.Yellow
            };
            AddChild(edit, 100);

            var begin = new BaseButton(300, 100, 200, 80)
            {
                Text = "开始游戏",
                FontSize = 24
            };
            AddChild(begin, 100);

            var reg = new BaseButton(690, 0, 100, 50)
            {
                Text = "注册帐号",
                FontSize = 18
            };
            AddEventListener(reg.EventListener);
            reg.OnClick += () =>
            {
                AppDelegate.SharedWindow.DefaultDirector.PushScene(new RegistScene());
            };
            AddChild(reg, 100);
        }
    }
}
