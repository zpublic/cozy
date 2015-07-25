using NetworkRelayServer;
using System;

namespace CozyAnywhere.ConsoleRelayServer
{
    public class Program
    {
        public static ServerRelay server { get; set; }
        static void Main(string[] args)
        {
            server = new ServerRelay(1000, 36048);
            server.Listen();
            while (true)
            {
                server.EnterMainLoop();
            }
        }
    }
}
