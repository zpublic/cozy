using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Infrastructure.IPC
{
    public class PipeIPCServer : IDisposable
    {
        public string ServerName { get; private set; }
        public Action<string> Callback { get; set; }
        public bool IsConnected { get; set; }

        private NamedPipeServerStream NPS { get; set; }

        public PipeIPCServer(string name)
        {
            ServerName = name;

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("name cannot be empty");
            }
            NPS = new NamedPipeServerStream(name);
        }

        public void Wait()
        {
            NPS.WaitForConnection();
            NPS.WaitForPipeDrain();
            IsConnected = true;

            try
            {
                using (var sr = new StreamReader(NPS))
                {
                    var res = sr.ReadToEnd();
                    if (res != PipeIPCCommon.CloseCommand)
                    {
                        Callback?.Invoke(res);
                    }
                }
            }
            catch (IOException)
            {

            }
        }

        public static bool TryCloseServer(string ServerName)
        {
            using (var npc = new NamedPipeClientStream(ServerName))
            {
                try
                {
                    npc.Connect(0);
                }
                catch (TimeoutException)
                {

                }

                if (npc.IsConnected)
                {
                    using (var sw = new StreamWriter(npc))
                    {
                        sw.AutoFlush = true;
                        sw.Write(PipeIPCCommon.CloseCommand);
                        return true;
                    }
                }
            }
            return false;
        }

        public void Dispose()
        {
            NPS?.Dispose();
        }
    }
}
