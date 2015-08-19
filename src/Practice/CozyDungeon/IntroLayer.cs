using CocosDenshion;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyDungeon
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
            base.Update(dt);
        }
    }
}
