using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.View.Scene
{
    public class BgLayer : CCLayer
    {
        public BgLayer(string bgPic)
        {
            var sprite = new CCSprite(bgPic);
            AddChild(sprite, 0);
        }
    }
}
