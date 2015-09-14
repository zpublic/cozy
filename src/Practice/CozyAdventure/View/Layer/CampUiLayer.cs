using System;
using CocosSharp;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Layer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
   public class CampUiLayer:CCLabel
    {
            int Fighting=0;
            int Gold=0;
            int Exper =0;
        public CampUiLayer()
        {
            
            var edit = new CocosSharpExt.CCTextField("战斗力:"+Fighting+"金币"+Gold+"经验"+Exper, "微软雅黑", 22)
            {
                Position = new CCPoint(100, 120),
                Color = CCColor3B.Blue
            };
            AddChild(edit, 100);
            var Goon = new BaseButton(631, 86, 78, 36)
            {
                Text = "继续冒险",
                FontSize = 14
            };
            AddChild(Goon, 100);
            var MercMange = new BaseButton(631, 160, 78, 36)
            {
                Text = "佣兵管理",
                FontSize = 14
            };
            AddChild(MercMange, 100);
            var MyGoods = new BaseButton(631, 227, 78, 36)
            {
                Text = "我的物品",
                FontSize = 14
            };
            AddChild(MyGoods, 100);
            var MyFriends = new BaseButton(631, 299, 78, 36)
            {
                Text = "我的好友",
                FontSize = 14
            };
            AddChild(MyFriends, 100);
        }
    }
}
