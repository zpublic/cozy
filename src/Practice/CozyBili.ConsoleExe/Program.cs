using System;
using BiliDMLib;
using System.Threading;

namespace CozyBili.ConsoleExe {

    class Program {

        static void Main(string[] args)
        {
            args = args.Length > 0 ? args : new[] { "34083" };
            int roomId;
            if (args.Length > 0 && int.TryParse(args[0], out roomId))
            {
                DanmakuLoader b = new BiliDMLib.DanmakuLoader();
                b.Disconnected += b_Disconnected;
                b.ReceivedDanmaku += b_ReceivedDanmaku;
                b.ReceivedRoomCount += b_ReceivedRoomCount;
                b.ConnectAsync(roomId);
                Thread.Sleep(233333333);
            }
        }

        static private void b_ReceivedRoomCount(object sender, ReceivedRoomCountArgs e)
        {
            string s = e.UserCount + "";
        }

        static private void b_ReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            switch (e.Danmaku.MsgType)
            {
                case MsgTypeEnum.Comment:
                    string s = ("收到彈幕:" + (e.Danmaku.isAdmin ? "[管]" : "") + (e.Danmaku.isVIP ? "[爷]" : "") + e.Danmaku.CommentUser + " 說: " + e.Danmaku.CommentText);
                    break;
                case MsgTypeEnum.GiftTop:
                    break;
                case MsgTypeEnum.GiftSend:
                    {
                        break;
                    }
                case MsgTypeEnum.Welcome:
                    {
                        break;
                    }
            }
        }

        static private void b_Disconnected(object sender, DisconnectEvtArgs args)
        {
        }
    }
}
