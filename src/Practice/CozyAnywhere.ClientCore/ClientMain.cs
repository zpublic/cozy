using NetworkServer;
using CozyAnywhere.Plugin.WinFile;
using CozyAnywhere.Protocol.Messages;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        private Server server { get; set; }

        public bool IsListing { get; set; }

        public AnywhereClient(int MaxConn, int Port)
        {
            RegisterResponseActions();
            InitNetwork(MaxConn, Port);
        }

        public void InitNetwork(int MaxConn, int Port)
        {
            server = new Server(MaxConn, Port);
            InitClientMessage();
            InitClientEvent();
        }

        public void Listen()
        {
            if (server != null)
            {
                server.Listen();
                IsListing = true;
            }
        }

        public void Shutdown()
        {
            if (server != null)
            {
                server.Shutdown();
                IsListing = false;
            }
        }

        public void EnterMainLoop()
        {
            if (server != null)
            {
                server.EnterMainLoop();
            }
        }

        public void Update()
        {
            if(server != null)
            {
                server.RecivePacket();
            }
        }

        public void SendTestMessage()
        {
            if (server != null)
            {
                var command     = FilePlugin.MakeFileEnumCommand(@"D:\", false, false);
                var commandMsg  = new CommandMessage()
                {
                    Command = command,
                };
                server.SendMessage(commandMsg);
            }
        }
    }
}