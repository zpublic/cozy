using System;
using CocosSharp;
using CozyAdventure.Public.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Game.Object;
using CozyAdventure.Game.Logic;
using CozyAdventure.View.Scene;
using CozyAdventure.Game.Manager;
using Cozy.Game.Manager;

namespace CozyAdventure.View.Layer
{
    public class CampUiLayer : CCLayer
    {
        private CCLabel EditNode { get; set; }

        public override void OnEnter()
        {
            base.OnEnter();
            RegisterEvent();
        }

        public override void OnExit()
        {
            base.OnExit();
            UnregisterEvent();
        }

        private void RegisterEvent()
        {
            MessageManager.RegisterMessage("Message.GotoMap.Success", OnGotoMapSuccess);
            MessageManager.RegisterMessage("Message.GotoMap.Failed", OnGotoMapFailed);
        }

        private void UnregisterEvent()
        {
            MessageManager.UnRegisterMessage("Message.GotoMap.Failed", OnGotoMapFailed);
            MessageManager.UnRegisterMessage("Message.GotoMap.Success", OnGotoMapSuccess);
        }

        public CampUiLayer()
        {
            InitUI();
            RefreshPlayerInfo();
        }

        private void InitUI()
        {
            EditNode = new CCLabel("", StringManager.GetText("GlobalFont"), 22)
            {
                Position    = new CCPoint(100, 120),
                AnchorPoint = CCPoint.Zero,
                Color       = CCColor3B.Blue
            };
            AddChild(EditNode, 100);

            var Goon = new CozySampleButton(631, 86, 78, 36)
            {
                Text        = "继续冒险",
                FontSize    = 14,
                OnClick     = () =>
                {
                    FarmMapLogic.EnterMap(PlayerObject.Instance.Self.CurrLevel);
                }
            };
            this.AddEventListener(Goon.EventListener);
            AddChild(Goon, 100);

            var MercMange = new CozySampleButton(631, 160, 78, 36)
            {
                Text        = "佣兵管理",
                FontSize    = 14,
                OnClick     = () =>
                {
                    AppDelegate.SharedWindow.DefaultDirector.PushScene(new FollowerListScene());
                }
            };
            this.AddEventListener(MercMange.EventListener);
            AddChild(MercMange, 100);

            var MyGoods = new CozySampleButton(631, 227, 78, 36)
            {
                Text        = "我的物品",
                FontSize    = 14
            };
            this.AddEventListener(MyGoods.EventListener);
            AddChild(MyGoods, 100);

            var MyFriends = new CozySampleButton(631, 299, 78, 36)
            {
                Text        = "我的好友",
                FontSize    = 14
            };
            this.AddEventListener(MyFriends.EventListener);
            AddChild(MyFriends, 100);
        }

        private void RefreshPlayerInfo()
        {
            var Fighting    = FollowerCollectLogic.GetAttack(PlayerObject.Instance.Self.FightFollower);
            var Money       = PlayerObject.Instance.Self.Money;
            var Exp         = PlayerObject.Instance.Self.Exp;

            EditNode.Text = string.Format("战斗力: + {0} +  金币 + {1} + 经验 + {2}", Fighting, Money, Exp);
        }

        private void OnGotoMapFailed()
        {

        }

        private void OnGotoMapSuccess()
        {
            AppDelegate.SharedWindow.DefaultDirector.ReplaceScene(new AdventureScene());
        }
    }
}
