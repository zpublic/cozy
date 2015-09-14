using System;
using CocosSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharpExt;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Scene;

namespace CozyAdventure.View.Layer
{
    public class LoadingUiLayer : CCLayer
    {
        public LoadingUiLayer()
        {

            var logo = new CCSprite("@face.png")
            {
                Position = new CCPoint(381, 154),
            };
            AddChild(logo);

            var lable = new CCLabel("加载中", "微软雅黑", 72)
            {
                Position = new CCPoint(381, 260),
            };
            AddChild(lable, 100);
            var lable1 = new CCLabel("程序员正在加班写代码。。。", "微软雅黑", 72)
            {
                Position = new CCPoint(306, 314),
                Color = CCColor3B.Yellow
            };
            AddChild(lable1, 100);
        }
    }
}
