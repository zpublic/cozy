using NetworkServer;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        private Server server { get; set; }

        public bool IsListing { get; set; }

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
            server.EnterMainLoop();
        }
    }
}