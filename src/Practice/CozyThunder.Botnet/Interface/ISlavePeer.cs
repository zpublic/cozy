using System.Net;

namespace CozyThunder.Botnet.Interface
{
    public interface ISlavePeer
    {
        bool Start(IPAddress ip, int port, ISlavePeerListener listener);
        bool Stop();
        void Send(string data);
        void Send(byte[] data);
    }
}
