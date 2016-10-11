using Griffin.Net.Protocols.MicroMsg;
using Griffin.Net.Protocols.Serializers;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CozyThunder3.Hub.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            CozyHub hub = new CozyHub();
            hub.Start(48360);
            SendHello().Wait();
            SendFile().Wait();
        }

        private static async Task SendHello()
        {
            var ser = new DataContractMessageSerializer();
            var client = new MicroMessageClient(ser);
            await client.ConnectAsync(IPAddress.Loopback, 48360);
            await client.SendAsync("hello");
            var response = await client.ReceiveAsync();
            Console.WriteLine("Client received: " + response);
        }

        private static async Task SendFile()
        {
            var ser = new DataContractMessageSerializer();
            var client = new MicroMessageClient(ser);
            await client.ConnectAsync(IPAddress.Loopback, 48360);
            await client.SendAsync(new FileStream(@"D:\data\img\lulu.jpg", FileMode.Open));
            var response = await client.ReceiveAsync();
            Console.WriteLine("Client received: " + response);
        }
    }
}
