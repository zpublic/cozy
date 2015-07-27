using NetworkServer;
using NetworkHelper;
using NetworkHelper.Event;
using System;
using System.Threading;

namespace ConsoleClientTester
{
    internal class Program
    {
        private static Server server { get; set; }

        private static SynchronizationContext SynContext { get; set; }

        private static void Main(string[] args)
        {
            SynContext              = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(SynContext);

            server                  = new Server(1000, 48360);
            server.StatusMessage    += new EventHandler<DataMessageArgs>(OnStatusMessage);
            server.Listen();

            server.EnterMainLoop();
        }

        public static void OnStatusMessage(object sender, DataMessageArgs e)
        {
            var status = (NetworkHelper.NetConnectionStatus)e.Input.ReadByte();
            string reason = e.Input.ReadString();
            if (status == NetConnectionStatus.Connected)
            {
                Console.WriteLine("Connected");
            }
        }
    }
}