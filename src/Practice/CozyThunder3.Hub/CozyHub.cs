using Griffin.Net.Channels;
using Griffin.Net.Protocols.MicroMsg;
using System;
using System.IO;
using System.Net;

namespace CozyThunder3.Hub
{
    public class CozyHub
    {
        public void Start(int port)
        {
            var server = new MicroMessageTcpListener();
            server.MessageReceived += OnServerMessageReceived;
            server.Start(IPAddress.Any, port);;
        }

        private void OnServerMessageReceived(ITcpChannel channel, object message)
        {
            var s = message as string;
            if (s == null)
            {
                var f = (Stream)message;
                if (f == null)
                    throw new Exception("Server received unexpected object type.");
            }
            Console.WriteLine("Server received: " + message);
            channel.Send("world");
        }
    }
}
