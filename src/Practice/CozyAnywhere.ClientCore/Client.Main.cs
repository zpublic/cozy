using NetworkServer;
using CozyAnywhere.Protocol.Messages;
using PluginHelper;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        private Server server { get; set; }

        public bool IsListing { get; set; }

        private PluginCommandMaker CommandMaker { get; set; }

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
            InitCommand();
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

        public void ConnectServer(string ip, int port)
        {
            if(server != null)
            {
                server.Connect(ip, port);
            }
        }

        private void InitCommand()
        {
            CommandMaker    = new PluginCommandMaker();
            var files       = EnumPluginFolder();
            var asss        = LoadPlugins(files);
            foreach(var obj in asss)
            {
                CommandMaker.LoadCommand(obj.Item2, obj.Item1);
            }
        }

        public void SendPluginLoadMessage()
        {
            if (server != null)
            {
                var loadMsg = new PluginLoadMessage();
                server.SendMessage(loadMsg);
            }
        }
    }
}