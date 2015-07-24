using NetworkClient;
using CozyAnywhere.PluginMgr;

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