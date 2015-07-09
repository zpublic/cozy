using NetworkClient;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        private Client client { get; set; }

        public bool IsConnected { get; set; }

        public void InitNetwork()
        {
            client = new Client();
            InitServerEvent();
            InitServerMessage();
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