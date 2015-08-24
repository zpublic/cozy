using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.View.Scene
{
    public class HomePageScene : CCScene
    {
        public HomePageScene(CCWindow window) : base(window)
        {
            AddChild(new BgLayer(@"pic\bg1.png"));
        }
    }
}
