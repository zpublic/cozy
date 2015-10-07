using System;
using CocosSharp;
using CozyAdventure.Public.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
    class AdventureUiLayer:CCLabel
    {
        public AdventureUiLayer()
        {
            InitUI();
        }

        private void InitUI()
        {
            int Fighting = 0;
            int Gold = 0;
            int Exper = 0;
            var edit = new CCLabel("战斗力:" + Fighting + "金币" + Gold + "经验" + Exper, "微软雅黑", 14)
            {
                Position = new CCPoint(37, 36),
                Color = CCColor3B.Blue
            };
            AddChild(edit, 100);
            var Forest = 0;
            var GoldSpeed = 0;
            var ExperSpeed = 0;
            var edit1 = new CCLabel("当前地点:" + Forest + "金币速度" + GoldSpeed + "经验" + ExperSpeed, "微软雅黑", 14)
            {
                Position = new CCPoint(37, 55),
                Color = CCColor3B.Black
            };
            AddChild(edit1, 100);
            var Details = new CozySampleButton(542, 38, 78, 36)
            {
                Text = "详情",
                FontSize = 14
            };
            AddChild(Details, 100);
            var DoCamp = new CozySampleButton(630, 38, 78, 36)
            {
                Text = "安营",
                FontSize = 14
            };
            AddChild(DoCamp, 100);
            var Leave = new CozySampleButton(718, 38, 78, 36)
            {
                Text = "离开",
                FontSize = 14
            };
            AddChild(Leave, 100);
        }
    }
}
