using CocosSharp;
using System;
using CozyAdventure.View.Layer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;

namespace CozyAdventure.View.Scene
{
    public class LoadingScene : CCScene
    {
        public LoadingScene(Action timeoutCallback = null, int timeout = 10) : base(AppDelegate.SharedWindow)
        {
            var bg = new BackgroundLayer(@"pic\1.png");
            AddChild(bg);
            var ui = new LoadingUiLayer(timeoutCallback, timeout);
            AddChild(ui);
        }
    }
}
