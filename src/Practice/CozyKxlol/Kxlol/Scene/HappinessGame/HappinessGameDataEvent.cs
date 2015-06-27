using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Network.Msg;
using CozyKxlol.Kxlol.Object;
using CozyKxlol.Network.Msg.Happy;
using Microsoft.Xna.Framework;
using CozyKxlol.Kxlol.Converter;

namespace CozyKxlol.Kxlol.Scene
{
    public partial class HappinessGameLayer
    {
        private void OnHappyPlayerLoginRsp(MsgBase b)
        {
            var rspMsg          = (Msg_HappyPlayerLoginRsp)b;
            Uid                 = rspMsg.Uid;
            var sid             = rspMsg.SpriteId;
            Player              = CozyTileSprite.Create(@"Pokemon\Pokemon_" + sid.ToString("D3"));
            Player.TilePosition = new Point(rspMsg.X, rspMsg.Y);
            this.AddChind(Player, 2);
        }

        private void OnHappyOtherPlayerLogin(MsgBase b)
        {
            var otherMsg                    = (Msg_HappyOtherPlayerLogin)b;
            var sid                         = otherMsg.SpriteId;
            var sp                          = CozyTileSprite.Create(@"Pokemon\Pokemon_" + sid.ToString("D3"));
            sp.TilePosition                 = new Point(otherMsg.X, otherMsg.Y);
            OtherPlayerList[otherMsg.Uid]   = sp;
            this.AddChind(sp, 1);
        }

        private void OnHappyPlayerPack(MsgBase b)
        {
            var packMsg = (Msg_HappyPlayerPack)b;
            foreach (var obj in packMsg.PlayerPack)
            {
                if (obj.Item4)
                {
                    var sid                     = obj.Item5;
                    var osp                     = CozyTileSprite.Create(@"Pokemon\Pokemon_" + sid.ToString("D3"));
                    osp.TilePosition            = new Point(obj.Item2, obj.Item3);
                    OtherPlayerList[obj.Item1]  = osp;
                    this.AddChind(osp, 1);
                }
            }
        }

        private void OnHappyPlayerMove(MsgBase b)
        {
            var moveMsg = (Msg_HappyPlayerMove)b;
            uint uid    = moveMsg.Uid;
            if (OtherPlayerList.ContainsKey(uid))
            {
                var player = OtherPlayerList[uid];
                if (player != null)
                {
                    var dire = MoveDirectionToPointConverter.PointConvertToMoveDirection(new Point(moveMsg.X, moveMsg.Y));
                    player.Move(dire);
                }
            }
        }

        private void OnHappyPlayerQuit(MsgBase b)
        {
            var quitMsg = (Msg_HappyPlayerQuit)b;
            uint quid   = quitMsg.Uid;
            if (OtherPlayerList.ContainsKey(quid))
            {
                var player = OtherPlayerList[quid];
                player.StopAllActions();
                this.RemoveChild(player);
                OtherPlayerList.Remove(quid);
            }
        }
    }
}
