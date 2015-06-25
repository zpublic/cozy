using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Network;
using CozyKxlol.Network.Msg;
using CozyKxlol.Network.Msg.Agar;
using Microsoft.Xna.Framework;
using CozyKxlol.Kxlol.Object;

namespace CozyKxlol.Kxlol.Scene
{
    public partial class BallGameSceneLayer
    {
        NetClientHelper client  = new NetClientHelper();
        public bool IsConnect   = false;
        public uint Uid         = 0;

        private void OnStatusMessage(object sender, NetClientHelper.StatusMessageArgs msg)
        {
            if (msg.Status == ConnectionStatus.Connected)
            {
                IsConnect       = true;
                var loginMsg    = new Msg_AgarLogin();

                client.SendMessage(loginMsg);
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
            switch(b.Id)
            {
                case MsgId.AgarLoginRsp:
                    OnLoginRsp(b);
                    break;
                case MsgId.AgarFixedBall:
                    OnFixedBall(b);
                    break;
                case MsgId.AgarPlayInfo:
                    OnPlayerInfo(b);   
                    break;
                case MsgId.AgarFixBallPack:
                    OnFixBallPack(b);
                    break;
                case MsgId.AgarPlayInfoPack:
                    OnPlayInfoPack(b);
                    break;
                case MsgId.AgarSelf:
                    OnSelf(b);
                    break;
                case MsgId.AgarMarkListPark:
                    OnMarkListPack(b);
                    break;
                default:
                    break;
            }
        }
    }
}
