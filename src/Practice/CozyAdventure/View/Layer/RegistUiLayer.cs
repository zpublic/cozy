using CocosSharp;
using CozyAdventure.Public.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
    public class RegistUiLayer : CCLayer
    {
        public RegistUiLayer()
        {
            InitUI();
        }

        private void InitUI()
        {
            var title = new CCLabel("冒险与编程", "Consolas", 72)
            {
                Position = new CCPoint(400, 320),
                Color = CCColor3B.Yellow
            };
            AddChild(title, 100);

            var begin = new CozySampleButton(200, 100, 400, 80)
            {
                Text = "已注册成功，点击返回",
                FontSize = 24,
                OnClick = new Action(OnRetButton),
            };
            AddChild(begin, 100);
            AddEventListener(begin.EventListener);
        }

        public void OnRetButton()
        {
            AppDelegate.SharedWindow.DefaultDirector.PopScene();
        }
    }
}
