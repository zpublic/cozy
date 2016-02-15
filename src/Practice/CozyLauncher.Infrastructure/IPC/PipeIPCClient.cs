using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Infrastructure.IPC
{
    public class PipeIPCClient : IDisposable
    {
        public string ClientName { get; private set; }

        private NamedPipeClientStream NPC { get; set; }

        public PipeIPCClient(string name)
        {
            ClientName = name;
            if(string.IsNullOrEmpty(name))
            {
                throw new Exception("name cannot be empty");
            }

            NPC = new NamedPipeClientStream(name);
        }

        public void Send(string data)
        {
            try
            {
                NPC.Connect(0);
            }
            catch (TimeoutException)
            {

            }

            if (NPC.IsConnected)
            {
                using (var sw = new StreamWriter(NPC))
                {
                    sw.AutoFlush = true;
                    sw.Write(data);
                }
            }
        }

        public void Dispose()
        {
            NPC?.Dispose();
        }
    }
}
