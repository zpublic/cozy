using System;
using CocosSharp;
using System.Collections.Generic;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Layer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Scene
{
    public class FollowerListScene : CCScene
    {
        public FollowerListScene() : base(AppDelegate.SharedWindow)
        {
            var ui = new FollowerListUiLayer();
            AddChild(ui);

            //var bg = new BackgroundLayer(@"pic\1.png");
            //AddChild(bg);
            
        }
    }
}
