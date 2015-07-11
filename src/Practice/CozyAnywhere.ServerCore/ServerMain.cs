using NetworkClient;
using CozyAnywhere.PluginMgr;
using CozyAnywhere.Plugin.WinFile;
using CozyAnywhere.Plugin.WinProcess;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        private Client client { get; set; }

        public PluginManager ServerPluginMgr { get; set; }

        public bool IsConnected { get; set; }

        public AnywhereServer()
        {
            InitNetwork();
            InitPlugin();
        }

        public void InitNetwork()
        {
            client          = new Client();
            InitServerEvent();
            InitServerMessage();
        }

        private void InitPlugin()
        {
            ServerPluginMgr     = new PluginManager();
            var fileplugin      = new FilePlugin();
            var processplugin   = new ProcessPlugin();
            ServerPluginMgr.AddPlugin(fileplugin);
            ServerPluginMgr.AddPlugin(processplugin);
        }

        public void Connect(string ip, int port)
        {
            client.Connect(ip, port);
            IsConnected = true;
        }

        public void DisConnect()
        {
            client.DisConnect();
            IsConnected = false;
        }

        public void Update()
        {
            client.Update();
        }
    }
}