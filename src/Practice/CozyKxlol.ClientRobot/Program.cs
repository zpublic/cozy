using CozyKxlol.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            };
            while (true)
            {
                netClient.Update();
                Thread.Sleep(1);
            }
        }
    }
}
