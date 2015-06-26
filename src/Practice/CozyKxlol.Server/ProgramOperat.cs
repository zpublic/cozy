using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Manager;
using CozyKxlol.Network.Msg;
using CozyKxlol.Network.Msg.Agar;
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
            SendMessage(AgarServer, r);
        }

        private static void OnFixedRemove(object sender, FixedBallManager.FixedRemoveArgs msg)
        {
            Msg_AgarFixedBall r = new Msg_AgarFixedBall();
            r.Operat            = Msg_AgarFixedBall.Remove;
            r.BallId            = msg.BallId;
            SendMessage(AgarServer, r);
        }

        private static void OnPlayerExit(object sender, PlayerBallManager.PlayerExitArgs msg)
        {
            var removeMsg       = new Msg_AgarPlayInfo();
            removeMsg.Operat    = Msg_AgarPlayInfo.Remove;
            removeMsg.UserId    = msg.UserId;

            MarkMgr.Remove(msg.UserId);
            SendMessage(AgarServer, removeMsg);
        }

        private static void OnPlayerDead(object sender, PlayerBallManager.PlayerDeadArgs msg)
        {
            var conn = ConnectionMgr.First(obj => obj.Value == msg.UserId).Key;

            MarkMgr.Remove(msg.UserId);

            // 为自己发送死亡信息
            var selfMsg     = new Msg_AgarSelf();
            selfMsg.Operat  = Msg_AgarSelf.Dead;
            SendMessage(AgarServer, selfMsg, conn);

            // 为其他玩家推送玩家死亡信息
            var pubMsg      = new Msg_AgarPlayInfo();
            pubMsg.Operat   = Msg_AgarPlayInfo.Remove;
            pubMsg.UserId   = msg.UserId;
            SendMessageExceptOne(AgarServer, pubMsg, conn);
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
            SendMessage(AgarServer, markMsg);

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
