using System;
using CocosSharp;
using CozyAdventure.Public.Controls;
using CozyAdventure.View.Layer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Game.Object;
using CozyAdventure.Game.Logic;

namespace CozyAdventure.View.Layer
{
    public class CampUiLayer : CCLayer
    {
        public CampUiLayer()
        {
            
            var Fighting    = FollowerCollectLogic.GetAttack(PlayerObject.Instance.Self.FightFollower);
            var Money       = PlayerObject.Instance.Self.Money;
            var Exp         = PlayerObject.Instance.Self.Exp;
            var edit = new CCLabel(string.Format("战斗力: + {0} +  金币 + {1} + 经验 + {2}", Fighting, Money, Exp), "微软雅黑", 22)
            {
                Position    = new CCPoint(100, 120),
                AnchorPoint = CCPoint.Zero,
                Color       = CCColor3B.Blue
            };
            AddChild(edit, 100);
            var Goon = new CozySampleButton(631, 86, 78, 36)
            {
                Text        = "继续冒险",
                FontSize    = 14
            };
            AddChild(Goon, 100);
            var MercMange = new CozySampleButton(631, 160, 78, 36)
            {
                Text        = "佣兵管理",
                FontSize    = 14
            };
            AddChild(MercMange, 100);
            var MyGoods = new CozySampleButton(631, 227, 78, 36)
            {
                Text        = "我的物品",
                FontSize    = 14
            };
            AddChild(MyGoods, 100);
            var MyFriends = new CozySampleButton(631, 299, 78, 36)
            {
                Text        = "我的好友",
                FontSize    = 14
            };
            AddChild(MyFriends, 100);
        }
    }
}
