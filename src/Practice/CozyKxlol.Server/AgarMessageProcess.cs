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
        private static bool OnProcessAccountReg(NetServer server, int id, NetIncomingMessage msg)
        {
            Msg_AccountReg r        = new Msg_AccountReg();
            r.R(msg);
            Msg_AccountRegRsp rr    = new Msg_AccountRegRsp();
            rr.suc                  = true;
            rr.detail               = r.name + r.pass;
            SendMessage(server, rr, msg.SenderConnection);
            return true;
        }

        private static bool OnProcessLogin(NetServer server, int id, NetIncomingMessage msg)
        {
            uint uid                = AgarGameId;

            Msg_AgarLogin r         = new Msg_AgarLogin();
            r.R(msg);

            Msg_AgarLoginRsp rr     = new Msg_AgarLoginRsp();
            rr.Uid                  = uid;
            rr.Width                = GameWidth;
            rr.Height               = GameHeight;
            SendMessage(server, rr, msg.SenderConnection);

            // 推送之前加入的玩家数据到新玩家
            var PlayerList = PlayerBallMgr.ToList();
            var PlayerPackList =
                from p
                in PlayerList
                select Tuple.Create<uint, float, float, int, uint, string>
                (p.Key, p.Value.X, p.Value.Y, p.Value.Radius, p.Value.Color, p.Value.Name);

            var PlayerPack          = new Msg_AgarPlayInfoPack();
            PlayerPack.PLayerList   = PlayerPackList.ToList();
            SendMessage(server, PlayerPack, msg.SenderConnection);

            // 为新加入的玩家推送FixedBall
            var FixedList       = FixedBallMgr.ToList();
            var FixedPackList   =
                from f
                in FixedList
                select Tuple.Create<uint, float, float, int, uint>(f.Key, f.Value.X, f.Value.Y,
                f.Value.Radius, f.Value.Color);

            var FixedPack           = new Msg_AgarFixBallPack();
            FixedPack.FixedList     = FixedPackList.ToList();
            SendMessage(server, FixedPack, msg.SenderConnection);
            return true;
        }

        private static bool OnProcessPlayerInfo(NetServer server, int id, NetIncomingMessage msg)
        {
            Msg_AgarPlayInfo r  = new Msg_AgarPlayInfo();
            r.R(msg);
            uint uid            = r.UserId;
            if (r.Operat == Msg_AgarPlayInfo.Changed)
            {
                if (!PlayerBallMgr.IsContain(uid)) return true;

                PlayerBall newBall  = PlayerBallMgr.Get(uid);
                uint tag            = r.Tag;
                if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.POSITION_TAG))
                {
                    newBall.X = r.X;
                    newBall.Y = r.Y;
                }
                if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.RADIUS_TAG))
                {
                    newBall.Radius = r.Radius;
                }
                if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.COLOR_TAG))
                {
                    newBall.Color = r.Color;
                }
                if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.NAME_TAG))
                {
                    newBall.Name = r.Name;
                }

                bool RaduisChanged  = false;
                bool FoodChanged    = false;
                bool PlayerChanged  = false;
                // 检查食物有没有被吃
                if (UpdateFood(uid, ref newBall))
                {
                    FoodChanged = true;
                }
                // 检查能不能吃其他玩家
                if (UpdatePlayer(uid, ref newBall))
                {
                    PlayerChanged = true;
                }
                if (FoodChanged || PlayerChanged)
                {
                    r.Tag           = r.Tag | GameMessageHelper.RADIUS_TAG;
                    r.Radius        = newBall.Radius;
                    var self        = new Msg_AgarSelf();
                    self.Operat     = Msg_AgarSelf.GroupUp;
                    self.Radius     = newBall.Radius;
                    RaduisChanged   = true;
                    SendMessage(server, self, msg.SenderConnection);
                }
                PlayerBallMgr.Change(uid, newBall);

                // 检查会不会被其他玩家吃
                uint EatId = 0;
                if (UpdateOtherPlayer(uid, newBall, out EatId))
                {
                    if (PlayerBallMgr.IsContain(EatId))
                    {
                        var EatBall     = PlayerBallMgr.Get(EatId);
                        var conn        = AgarConnMgr.Get(EatId);
                        EatBall.Radius  += newBall.Radius;
                        PlayerBallMgr.Change(EatId, EatBall);

                        // 向其他玩家发送
                        Msg_AgarPlayInfo eatMsg = new Msg_AgarPlayInfo();
                        eatMsg.Operat           = Msg_AgarPlayInfo.Changed;
                        eatMsg.UserId           = EatId;
                        eatMsg.Tag              = GameMessageHelper.RADIUS_TAG;
                        eatMsg.Radius           = EatBall.Radius;
                        RaduisChanged           = true;
                        SendMessageExceptOne(server, eatMsg, conn);

                        // 向自身发送
                        var selfEatMsg          = new Msg_AgarSelf();
                        selfEatMsg.Operat       = Msg_AgarSelf.GroupUp;
                        selfEatMsg.Radius       = EatBall.Radius;
                        SendMessage(server, selfEatMsg, conn);
                    }
                }

                if (RaduisChanged)
                {
                    MarkMgr.Update(uid, newBall.Radius);
                }
            }
            SendMessageExceptOne(server, r, msg.SenderConnection);
            return true;
        }

        private static bool OnProcessBorn(NetServer server, int id, NetIncomingMessage msg)
        {
            var r = new Msg_AgarBorn();
            r.R(msg);

            uint uid    = r.UserId;
            string name = r.Name;

            int x       = RandomMaker.Next(GameWidth);
            int y       = RandomMaker.Next(GameHeight);
            int radius  = PlayerBall.DefaultPlayerRadius;
            uint c      = CustomColors.RandomColor;

            // 添加Player到Manager
            PlayerBall player   = new PlayerBall();
            player.X            = x;
            player.Y            = y;
            player.Radius       = radius;
            player.Color        = c;
            player.Name         = name;
            PlayerBallMgr.Add(uid, player);

            MarkMgr.Update(uid, radius);

            // 更新链接对应的ID
            AgarConnMgr.Modify(msg.SenderConnection, uid);

            // 向自身发送出生位置等信息
            var selfMsg         = new Msg_AgarSelf();
            selfMsg.Operat      = Msg_AgarSelf.Born;
            selfMsg.X           = x;
            selfMsg.Y           = y;
            selfMsg.Radius      = radius;
            selfMsg.Color       = c;
            SendMessage(server, selfMsg, msg.SenderConnection);

            // 向之前加入的玩家推送新用户出生信息
            var oMsg        = new Msg_AgarPlayInfo();
            oMsg.Operat     = Msg_AgarPlayInfo.Add;
            oMsg.UserId     = uid;
            oMsg.Tag        = GameMessageHelper.ALL_TAG;
            oMsg.X          = x;
            oMsg.Y          = y;
            oMsg.Radius     = radius;
            oMsg.Color      = c;
            oMsg.Name       = name;
            SendMessageExceptOne(server, oMsg, msg.SenderConnection);
            return true;
        }
    }
}
