using NetworkClient;
using NetworkHelper;
using NetworkHelper.Event;
using System;
using System.Threading;

namespace ConsoleClientTester
{
    internal class Program
    {
        private static Client client { get; set; }

        private static SynchronizationContext SynContext { get; set; }

        private static void Main(string[] args)
        {
            SynContext = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(SynContext);

            client = new Client();
            client.StatusMessage += new EventHandler<StatusMessageArgs>(OnStatusMessage);
            client.Connect("127.0.0.1", 48360);

            while (true)
            {
                client.Update();
            }
        }

        public static void OnStatusMessage(object sender, StatusMessageArgs e)
        {
            if (e.Status == NetConnectionStatus.Connected)
            {
                Console.WriteLine("Connected");
            }
        }
    }
}