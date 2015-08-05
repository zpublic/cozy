using NetworkProtocol;
using Lidgren.Network;

namespace CozyAnywhere.Protocol.Messages
{
    public class ConnectMessage : IMessage
    {
        public uint Id { get { return MessageId.ConnectMessage; } }

        public string Address { get; set; }
        public string ConnectName { get; set; }
        public string Information { get; set; }

        public void Write(NetBuffer om)
        {
            if (Address == null) Address = string.Empty;
            if (ConnectName == null) ConnectName = string.Empty;
            if (Information == null) ConnectName = string.Empty;

            om.Write(Address);
            om.Write(ConnectName);
            om.Write(Information);
        }

        public void Read(NetBuffer im)
        {
            Address     = im.ReadString();
            ConnectName = im.ReadString();
            Information = im.ReadString();
        }
    }
}
