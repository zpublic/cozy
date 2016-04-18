using System.Net.Sockets;
using System.Text;

namespace CozyThunder.Botnet.Common
{
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024 * 4;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
    }
}
