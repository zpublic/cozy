using Lidgren.Network;

namespace NetworkProtocol
{
    public interface IMessage
    {
        uint Id { get; }

        void Write(NetOutgoingMessage om);

        void Read(NetIncomingMessage im);
    }
}