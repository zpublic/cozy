using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkRelayServer;
using CozyAnywhere.RelayServerCore;

namespace CozyAnywhere.ConsoleRelayServer
{
    public class Program
    {
        public static ServerRelay server = new ServerRelay(1000, 48360);
        static void Main(string[] args)
        {
            while (true)
            {
                server.EnterMainLoop();
            }
        }
    }
}
