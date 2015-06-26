using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Network;
using CozyKxlol.Network.Msg;
using CozyKxlol.Kxlol.Object;
using CozyKxlol.Network.Msg.Happy;
using Microsoft.Xna.Framework;
using CozyKxlol.Kxlol.Converter;

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
                    var rspMsg = (Msg_HappyPlayerLoginRsp)b;
                    Uid = rspMsg.Uid;
                    Player = CozyTileSprite.Create("player");
                    Player.TilePosition = new Point(rspMsg.X, rspMsg.Y);
                    this.AddChind(Player, 2);
                    break;
                case MsgId.HappyOtherPlayerLogin:
                    var otherMsg = (Msg_HappyOtherPlayerLogin)b;
                    var sp = CozyTileSprite.Create("player");
                    sp.TilePosition = new Point(otherMsg.X, otherMsg.Y);
                    OtherPlayerList[otherMsg.Uid] = sp;
                    this.AddChind(sp, 1);
                    break;
                case MsgId.HappyPlayerPack:
                    var packMsg = (Msg_HappyPlayerPack)b;
                    foreach(var obj in packMsg.PlayerPack)
                    {
                        if(obj.Item4)
                        {
                            var osp = CozyTileSprite.Create("player");
                            osp.TilePosition = new Point(obj.Item2, obj.Item3);
                            OtherPlayerList[obj.Item1] = osp;
                            this.AddChind(osp, 1);
                        }
                    }
                    break;
                case MsgId.HappyPlayerMove:
                    var moveMsg = (Msg_HappyPlayerMove)b;
                    uint uid = moveMsg.Uid;
                    if(OtherPlayerList.ContainsKey(uid))
                    {
                        var player = OtherPlayerList[uid];
                        if(player != null)
                        {
                            var dire = MoveDirectionToPointConverter.PointConvertToMoveDirection(new Point(moveMsg.X, moveMsg.Y));
                            player.Move(dire);
                        }
                    }
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
