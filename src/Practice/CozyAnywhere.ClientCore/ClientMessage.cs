using System;
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
            if(FileCollection != null)
            {
                foreach(var obj in rspMsg.FileInfoList)
                {
                    var file = Tuple.Create<string, bool>(obj.Item1, obj.Item3);
                    FileCollection.Add(file);
                }
            }
        }
    }
}