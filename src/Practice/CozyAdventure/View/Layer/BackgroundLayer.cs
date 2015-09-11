using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
    public class BackgroundLayer : CCLayer
    {
        public BackgroundLayer(string bgPic)
        {
            var sprite = new CCSprite(bgPic);
            AddChild(sprite, 0);
        }
    }
}
