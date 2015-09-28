using CocosSharp;
using CozyAdventure.View.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Scene
{
    public class FarmScene : CCScene
    {
        public FarmScene(int level) : base(AppDelegate.SharedWindow)
        {
            var bg = new BackgroundLayer(@"pic\1.png");
            AddChild(bg);
            var ui = new FarmUiLayer(level);
            AddChild(ui);
        }
    }
}
