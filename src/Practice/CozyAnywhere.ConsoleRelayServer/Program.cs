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
            server = new AnywhereRelayServer(1000, 36048);
            server.ServerConnectMessage += new EventHandler<ServerConnectArgs>(OnServerConnect);
            server.ClientConnectMessage += new EventHandler<ClientConnectArgs>(OnClientConnect);
            server.MessageSendMessage   += new EventHandler<MessageSendMessage>(OnMessageSend);
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

        private static void OnMessageSend(object sender, MessageSendMessage msg)
        {
            Console.WriteLine(msg.From + " send " + msg.Id + " to " + msg.To);
        }
    }
}
