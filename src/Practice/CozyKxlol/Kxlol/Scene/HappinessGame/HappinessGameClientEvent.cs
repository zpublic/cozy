using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Network;
using CozyKxlol.Network.Msg;
using CozyKxlol.Network.Msg.Happy;

namespace CozyKxlol.Kxlol.Scene
{
    public partial class HappinessGameLayer
    {
        NetClientHelper client = new NetClientHelper();
        public bool IsConnect = false;
        public uint Uid = 0;

        private void OnStatusMessage(object sender, NetClientHelper.StatusMessageArgs msg)
        {
            if (msg.Status == ConnectionStatus.Connected)
            {
                IsConnect = true;
                var LoginMsg = new Msg_HappyPlayerLogin();

                client.SendMessage(LoginMsg);
            }
            else if (msg.Status == ConnectionStatus.Disconnected)
            {
                IsConnect = false;
            }
        }

        private void OnDataMessage(object sender, NetClientHelper.DataMessageArgs msg)
        {
            if (!IsConnect) return;

            MsgBase b = msg.Msg;
            switch (b.Id)
            {
                case MsgId.HappyPlayerLoginRsp:
                    OnHappyPlayerLoginRsp(b);
                    break;
                case MsgId.HappyOtherPlayerLogin:
                    OnHappyOtherPlayerLogin(b);
                    break;
                case MsgId.HappyPlayerPack:
                    OnHappyPlayerPack(b);
                    break;
                case MsgId.HappyPlayerMove:
                    OnHappyPlayerMove(b);
                    break;
                case MsgId.HappyPlayerQuit:
                    OnHappyPlayerQuit(b);
                    break;
                default:
                    break;
            }
        }

        private void RegisterClientEvent()
        {
            client.DataMessage += new EventHandler<NetClientHelper.DataMessageArgs>(OnDataMessage);
            client.StatusMessage += new EventHandler<NetClientHelper.StatusMessageArgs>(OnStatusMessage);
        }
    }
}
