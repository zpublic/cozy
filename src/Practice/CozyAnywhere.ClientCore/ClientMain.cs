using NetworkServer;
using CozyAnywhere.Plugin.WinFile;
using CozyAnywhere.Plugin.WinProcess;
using CozyAnywhere.Plugin.WinCapture;
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

        public void SendEnumFileMessage(string path)
        {
            if (server != null)
            {
                var command = FilePlugin.MakeFileEnumCommand(path, false, false);
                var commandMsg = new CommandMessage()
                {
                    Command = command,
                };
                server.SendMessage(commandMsg);
            }
        }

        public void SendEnumProcessMessage()
        {
            if (server != null)
            {
                var command1 = ProcessPlugin.MakeProcessEnumCommand();
                var commandMsg1 = new CommandMessage()
                {
                    Command = command1,
                };
                server.SendMessage(commandMsg1);
            }
        }

        public void SendDeleteMessage(string path)
        {
            if(server != null)
            {
                var command    = FilePlugin.MakeFileDeleteCommand(@"D:\" + path);
                var commandMsg = new CommandMessage()
                {
                    Command = command,
                };
                server.SendMessage(commandMsg);
            }
        }

        public void SendTerminateMessage(uint pid)
        {
            if(server != null)
            {
                var command     = ProcessPlugin.MakeProcessTerminateCommand(pid);
                var commandMsg  = new CommandMessage()
                {
                    Command = command,
                };
                server.SendMessage(commandMsg);
            }
        }

        public void SendPluginLoadMessage()
        {
            if(server != null)
            {
                var loadMsg = new PluginLoadMessage();
                server.SendMessage(loadMsg);
            }
        }

        public void SendCaptureMessage()
        {
            if(server != null)
            {
                var command1 = CapturePlugin.MakeGetCaptureDataCommand();
                var commandMsg1 = new CommandMessage()
                {
                    Command = command1,
                };
                server.SendMessage(commandMsg1);
            }
        }
    }
}