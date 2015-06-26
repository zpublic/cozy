using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using CozyKxlol.Network.Msg;
using CozyKxlol.Network.Msg.Agar;
using CozyKxlol.Network.Msg.Happy;
using CozyKxlol.Server.Model;
using CozyKxlol.Server.Manager;
using CozyKxlol.Server.Model.Interface;

namespace CozyKxlol.Server
{
    public partial class Program
    {
        private static bool OnProcessHappyLogin(NetServer server, int id, NetIncomingMessage msg)
        {
            var r       = new Msg_HappyPlayerLogin();
            r.R(msg);

            uint uid    = HappyGameId;
            int x       = RandomMaker.Next(10);
            int y       = RandomMaker.Next(10);
            var selfMsg = new Msg_HappyPlayerLoginRsp();
            selfMsg.Uid = uid;
            selfMsg.X   = x;
            selfMsg.Y   = y;
            SendMessage(server, selfMsg, msg.SenderConnection);

            // 更新链接对应的ID
            HappyConnMgr.Modify(msg.SenderConnection, uid);

            var playerPackMsg = new Msg_HappyPlayerPack();

            // 为新玩家推送旧玩家信息
            var playerList = HappyPlayerMgr.ToList();
            var playerPackList =
                from f
                in playerList
                select Tuple.Create<uint, int, int, bool>(f.Key, f.Value.X, f.Value.Y, f.Value.IsAlive);
            playerPackMsg.PlayerPack = playerPackList.ToList();
            SendMessage(server, playerPackMsg, msg.SenderConnection);

            // 为旧玩家推送新玩家信息
            var otherMsg    = new Msg_HappyOtherPlayerLogin();
            otherMsg.Uid    = uid;
            otherMsg.X      = x;
            otherMsg.Y      = y;
            SendMessageExceptOne(server, otherMsg, msg.SenderConnection);

            // 添加新玩家到玩家管理中
            var player      = new HappyPlayer();
            player.X        = x;
            player.Y        = y;
            player.IsAlive  = true;
            HappyPlayerMgr.Add(uid, player);

            return true;
        }

        private static bool OnProgressHappyPlayerMove(NetServer server, int id, NetIncomingMessage msg)
        {
            var r           = new Msg_HappyPlayerMove();
            r.R(msg);

            uint uid        = r.Uid;
            int x           = r.X;
            int y           = r.Y;

            var newPlayer   = HappyPlayerMgr.Get(uid);
            newPlayer.X     = x;
            newPlayer.Y     = y;
            HappyPlayerMgr.Modify(uid, newPlayer);

            SendMessageExceptOne(server, r, msg.SenderConnection);

            return true;
        }
    }
}
