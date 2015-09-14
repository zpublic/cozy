using CocosSharp;
using System;
using CozyAdventure.View.Layer;
using System.Collections.Generic;
using CozyAdventure.Public.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Scene
{
    public class CampScene : CCScene
    {
        public CampScene() : base(AppDelegate.SharedWindow)
        {
            var bg = new BackgroundLayer(@"pic\1.png");
            AddChild(bg);
            var ui =new CampUiLayer();
            AddChild(ui);
        }
    }
}
