using System;
using CocosSharp;
using CozyAdventure.Public.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Game.Logic;
using CozyAdventure.Game.Object;
using Cozy.Game.Manager;
using CozyAdventure.Protocol.Msg;
using CozyNetworkProtocol;
using CozyAdventure.Protocol;

namespace CozyAdventure.View.Layer
{
    public class AdventureUiLayer : CCLayer
    {
        private CCLabel PlayerInfoLabel { get; set; }

        private CCLabel MapInfoLabel { get; set; }

        public int Level { get; private set; }

        private long CurrExp { get; set; }

        private long CurrMoney { get; set; }

        private int ExpAdd { get; set; }

        private int MoneyAdd { get; set; }

        private int Fighting { get; set; }

        public AdventureUiLayer()
        {
            InitUI();
            RegisterEvent();
            FarmMapLogic.EnterMap(Level);
        }

        #region UI

        private void InitUI()
        {
            PlayerInfoLabel = new CCLabel("", "Consolas", 14)
            {
                Position = new CCPoint(37, 36),
                Color = CCColor3B.White,
            };

            MapInfoLabel = new CCLabel("", "Consolas", 14)
            {
                Position = new CCPoint(37, 55),
                Color = CCColor3B.White,
            };

            this.AddChild(PlayerInfoLabel, 100);
            this.AddChild(MapInfoLabel, 100);

            var Details = new CozySampleButton(542, 38, 78, 36)
            {
                Text        = "详情",
                FontSize    = 14
            };
            AddChild(Details, 100);
            var DoCamp = new CozySampleButton(630, 38, 78, 36)
            {
                Text        = "安营",
                FontSize    = 14
            };
            AddChild(DoCamp, 100);
            var Leave = new CozySampleButton(718, 38, 78, 36)
            {
                Text        = "离开",
                FontSize    = 14
            };
            AddChild(Leave, 100);

            RefreshPlayerInfo();
            RefreshMapInfo();
        }

        #endregion

        private void RegisterEvent()
        {
            MessageManager.RegisterMessage("Client.Data", OnMessage);
            Schedule(OnTimerAnimation, 1.0f);
        }

        private void UnRegisterEvent()
        {
            Unschedule(OnTimerAnimation);
            MessageManager.UnRegisterMessage("Client.Data", OnMessage);
        }

        private void RefreshPlayerInfo()
        {
            Fighting        = FollowerCollectLogic.GetAttack(PlayerObject.Instance.Self.FightFollower);
            CurrMoney       = PlayerObject.Instance.Self.Money;
            CurrExp         = PlayerObject.Instance.Self.Exp;

            PlayerInfoLabel.Text = string.Format("战斗力 : {0} 金币 : {1} 经验 : {2}" , Fighting, CurrMoney, CurrExp);
        }

        private void RefreshMapInfo()
        {
            Level           = PlayerObject.Instance.Self.CurrLevel;
            MoneyAdd        = FarmMapLogic.Money(Level);
            ExpAdd          = FarmMapLogic.Exp(Level);

            MapInfoLabel.Text = string.Format("当前地点 : {0} 金币速度 ： {1} 经验 : {2}", Level, MoneyAdd, ExpAdd);
        }

        private void OnMessage(object obj)
        {
            var msg = (MessageBase)obj;
            if (msg.Id == (uint)MessageId.Farm.FarmIncomeMessage)
            {
                OnFarmIncommingMessage((FarmIncomeMessage)msg);
            }
            else if (msg.Id == (uint)MessageId.Farm.GotoResultMessage)
            {
                OnGotoResultMessage((GotoResultMessage)msg);
            }
        }

        private void OnFarmIncommingMessage(FarmIncomeMessage msg)
        {
            SyncPlayerInfo(msg.Exp, msg.Money);
        }

        private void OnGotoResultMessage(GotoResultMessage msg)
        {
        }

        private void OnTimerAnimation(float dt)
        {
            CurrExp     += ExpAdd;
            CurrMoney   += ExpAdd;

            PlayerInfoLabel.Text = string.Format("战斗力 : {0} 金币 : {1} 经验 : {2}", Fighting, CurrMoney, CurrExp);
        }

        private void SyncPlayerInfo(int money, int exp)
        {
            CurrExp     = PlayerObject.Instance.Self.Exp + exp;
            CurrMoney   = PlayerObject.Instance.Self.Money + money;
            PlayerObject.Instance.Self.Exp      = CurrExp;
            PlayerObject.Instance.Self.Money    = CurrMoney;
        }

        private void CleanUp()
        {
        }
    }
}
