using Lidgren.Network;

namespace NetworkProtocol
{
    public interface IMessage
    {
        uint Id { get; }

        void Write(NetBuffer om);

        void Read(NetBuffer im);
    }
}