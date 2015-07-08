using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using NetworkProtocol;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        public void InitClientMessage()
        {
            MessageReader.RegisterType<FileEnumMessageRsp>(MessageId.FileEnumMessageRsp);
        }

        public void OnFileEnumMessageRsp(IMessage msg)
        {
            var rspMsg = (FileEnumMessageRsp)msg;

            // TODO
        }
    }
}