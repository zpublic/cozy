using CocosDenshion;
using CocosSharp;
using CozyAdventure.View.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyAdventure
{
    public class IntroLayer : CCLayerColor
    {
        public IntroLayer(CCSize size) : base(size)
        {
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            Schedule(Update);
        }

        public override void Update(float dt)
        {
            AppDelegate.SharedWindow.DefaultDirector.ReplaceScene(new HomePageScene(AppDelegate.SharedWindow));
            base.Update(dt);
        }
    }
}
