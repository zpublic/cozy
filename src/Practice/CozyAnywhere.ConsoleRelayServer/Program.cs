using NetworkRelayServer;

namespace CozyAnywhere.ConsoleRelayServer
{
    public class Program
    {
        public static ServerRelay server = new ServerRelay(1000, 48360);
        static void Main(string[] args)
        {
            server.Listen();
            while (true)
            {
                server.EnterMainLoop();
            }
        }
    }
}
