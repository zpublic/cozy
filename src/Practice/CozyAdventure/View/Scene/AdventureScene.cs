using CocosSharp;
using System;
using CozyAdventure.View.Layer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Scene
{
    public class AdventureScene: CCScene
    {
        public AdventureScene() : base(AppDelegate.SharedWindow)
        {
            var bg = new BackgroundLayer(@"pic\1.png");
            AddChild(bg);
            var ui = new LoadingUiLayer();
            AddChild(ui);
        }
    }
}
