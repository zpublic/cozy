using CozyKxlol.Network;
using CozyKxlol.Network.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CozyKxlol.ClientRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            NetClientHelper netClient = new NetClientHelper();
            netClient.Connect("127.0.0.1", 48360);
            netClient.InternalMessage += (sender, e) =>
            {
                Console.WriteLine(e.Msg);
            };
            netClient.StatusMessage += (sender, e) =>
            {
                Console.WriteLine(e.Status);
                Console.WriteLine(e.Reason);
            };
            netClient.DataMessage += (sender, e) =>
            {
                MsgBase b = e.Msg as MsgBase;
                if (b.Id == MsgId.ChatToAll)
                {
                    Msg_ChatToAll c = e.Msg as Msg_ChatToAll;
                    Console.WriteLine(c.Id);
                    Console.WriteLine(c.chatMsg);
                }
                else if (b.Id == MsgId.AccountRegRsp)
                {
                    Msg_AccountRegRsp c = e.Msg as Msg_AccountRegRsp;
                    Console.WriteLine(c.Id);
                    Console.WriteLine(c.suc);
                    Console.WriteLine(c.detail);
                }
            };

            var timer = new System.Timers.Timer(5000);
            timer.Elapsed += (sender, e) =>
            {
                Msg_ChatToAll chat = new Msg_ChatToAll();
                chat.chatMsg = "hehe";
                netClient.SendMessage(chat);
            };
            timer.Start();

            var timer2 = new System.Timers.Timer(3000);
            timer2.Elapsed += (sender, e) =>
            {
                Msg_AccountReg reg = new Msg_AccountReg();
                reg.name = "zapline";
                reg.pass = "000000";
                netClient.SendMessage(reg);
            };
            timer2.Start();

            while (true)
            {
                netClient.Update();
                Thread.Sleep(1);
            }
        }
    }
}
