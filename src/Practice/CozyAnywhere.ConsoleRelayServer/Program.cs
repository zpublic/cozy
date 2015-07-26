using NetworkRelayServer;
using CozyAnywhere.RelayServerCore.Events;
using System;
using CozyAnywhere.RelayServerCore;

namespace CozyAnywhere.ConsoleRelayServer
{
    public class Program
    {
        public static AnywhereRelayServer server { get; set; }
        static void Main(string[] args)
        {
            server = new AnywhereRelayServer(1000, 48360);
            server.ServerConnectMessage += new EventHandler<ServerConnectArgs>(OnServerConnect);
            server.ClientConnectMessage += new EventHandler<ClientConnectArgs>(OnClientConnect);
            server.Listen();
            server.EnterMainLoop();
        }

        private static void OnClientConnect(object sender, ClientConnectArgs msg)
        {
            Console.WriteLine("Client Connect");
        }

        private static void OnServerConnect(object sender, ServerConnectArgs msg)
        {
            Console.WriteLine("Server Connect");
        }
    }
}
