using CocosSharp;
using Cozy.Game.Manager;
using CozyAdventure.Game.Logic;
using CozyAdventure.Game.Object;
using CozyAdventure.Protocol;
using CozyAdventure.Protocol.Msg;
using CozyNetworkProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
    public class FarmUiLayer : CCLayer
    {
        public CCLabel PlayerExpNode { get; set; }

        public CCLabel PlayerMoneyNode { get; set; }

        public int Level { get; private set; }

        private long CurrExp { get; set; }

        private long CurrMoney { get; set; }

        private int ExpAdd { get; set; }

        private int MoneyAdd { get; set; }

        public FarmUiLayer(int level)
        {
            Level       = level;
            ExpAdd      = FramMapLogic.Exp(level);
            MoneyAdd    = FramMapLogic.Money(level);
            CurrExp     = PlayerObject.Instance.Self.Exp;
            CurrMoney   = PlayerObject.Instance.Self.Money;

            InitUI();

            MessageManager.RegisterMessage("Client.Data", OnMessage);
            FramMapLogic.EnterMap(level);

            Schedule(OnTimerAnimation, 1.0f);
        }

        #region UI

        private void InitUI()
        {
            PlayerExpNode = new CCLabel(CurrExp.ToString(), "Consolas", 14)
            {
                Position = new CCPoint(100, 100),
            };
            PlayerMoneyNode = new CCLabel(CurrMoney.ToString(), "Consolas", 14)
            {
                Position = new CCPoint(100, 200),
            };

            this.AddChild(PlayerExpNode);
            this.AddChild(PlayerMoneyNode);
        }

        #endregion

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
            //int i = 42;
        }

        private void OnTimerAnimation(float dt)
        {
            CurrExp     += ExpAdd;
            CurrMoney   += ExpAdd;
            RefreshNodeText();
        }

        private void SyncPlayerInfo(int money, int exp)
        {
            CurrExp     = PlayerObject.Instance.Self.Exp + exp;
            CurrMoney   = PlayerObject.Instance.Self.Money + money;
            PlayerObject.Instance.Self.Exp      = CurrExp;
            PlayerObject.Instance.Self.Money    = CurrMoney;
        }

        private void RefreshNodeText()
        {
            PlayerExpNode.Text      = "Exp : " + CurrExp;
            PlayerMoneyNode.Text    = "Money : " + CurrMoney;
        }

        private void CleanUp()
        {
            Unschedule(OnTimerAnimation);
        }
    }
}
