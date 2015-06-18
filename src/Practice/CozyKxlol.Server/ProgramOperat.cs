using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Manager;
using CozyKxlol.Network.Msg;
using Lidgren.Network;

namespace CozyKxlol.Server
{
    public partial class Program
    {
        private static void OnFixedCreate(object sender, FixedBallManager.FixedCreateArgs msg)
        {
            Msg_AgarFixedBall r = new Msg_AgarFixedBall();
            r.Operat            = Msg_AgarFixedBall.Add;
            r.BallId            = msg.BallId;
            r.X                 = msg.Ball.X;
            r.Y                 = msg.Ball.Y;
            r.Radius            = msg.Ball.Radius;
            r.Color             = msg.Ball.Color;

            NetOutgoingMessage om = server.CreateMessage();
            om.Write(r.Id);
            r.W(om);
            server.SendToAll(om, NetDeliveryMethod.Unreliable);
        }

        private static void OnFixedRemove(object sender, FixedBallManager.FixedRemoveArgs msg)
        {
            Msg_AgarFixedBall r = new Msg_AgarFixedBall();
            r.Operat            = Msg_AgarFixedBall.Remove;
            r.BallId            = msg.BallId;

            NetOutgoingMessage om = server.CreateMessage();
            om.Write(r.Id);
            r.W(om);
            server.SendToAll(om, NetDeliveryMethod.Unreliable);
        }

        private static void OnPlayerExit(object sender, PlayerBallManager.PlayerExitArgs msg)
        {
            var removeMsg       = new Msg_AgarPlayInfo();
            removeMsg.Operat    = Msg_AgarPlayInfo.Remove;
            removeMsg.UserId    = msg.UserId;

            MarkMgr.Remove(msg.UserId);

            NetOutgoingMessage om = server.CreateMessage();
            om.Write(removeMsg.Id);
            removeMsg.W(om);
            server.SendToAll(om, NetDeliveryMethod.Unreliable);
        }

        private static void OnPlayerDead(object sender, PlayerBallManager.PlayerDeadArgs msg)
        {
            var conn = ConnectionMgr.First(obj => obj.Value == msg.UserId).Key;

            MarkMgr.Remove(msg.UserId);

            // 为自己发送死亡信息
            var selfMsg     = new Msg_AgarSelf();
            selfMsg.Operat  = Msg_AgarSelf.Dead;

            NetOutgoingMessage som = server.CreateMessage();
            som.Write(selfMsg.Id);
            selfMsg.W(som);
            server.SendMessage(som, conn, NetDeliveryMethod.Unreliable, 0);

            // 为其他玩家推送玩家死亡信息
            var pubMsg      = new Msg_AgarPlayInfo();
            pubMsg.Operat   = Msg_AgarPlayInfo.Remove;
            pubMsg.UserId   = msg.UserId;

            NetOutgoingMessage pom = server.CreateMessage();
            pom.Write(pubMsg.Id);
            pubMsg.W(pom);
            SendToAllExceptOne(server, pubMsg.Id, pom, conn);
        }

        private static void OnMarkChange(object sender, MarkManager.MarkChangedArgs msg)
        {
            var markList =
                    from m
                        in msg.TopMarkList
                    orderby m.Value descending
                    select m;

            var markMsg         = new Msg_AgarMarkListPack();
            var sendList        = markList.Take(5).ToList();
            markMsg.MarkList    = sendList;

            NetOutgoingMessage mom = server.CreateMessage();
            mom.Write(markMsg.Id);
            markMsg.W(mom);
            server.SendToAll(mom, NetDeliveryMethod.Unreliable);

            Console.WriteLine("-----------------------------------------------------------");
            foreach (var obj in sendList)
            {
                if (PlayerBallMgr.IsContain(obj.Key))
                {
                    string name = PlayerBallMgr.Get(obj.Key).Name;
                    Console.WriteLine(name + " " + obj.Value);
                }
            }
        }
    }
}
