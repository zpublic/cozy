using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using CozyAdventure.View.Layer;

namespace CozyAdventure.View.Scene
{
    public class LevelSelectScene : CCScene
    {
        public LevelSelectScene()
            : base(AppDelegate.SharedWindow)
        {
            var bg = new BackgroundLayer(@"pic\1.png");
            AddChild(bg);

            var ui = new LevelSelectUiLayer();
            AddChild(ui);
        }
    }
}
