using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkHelper;
using NetworkProtocol;
using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public void InitServerMessage()
        {
            MessageReader.RegisterType<FileEnumMessage>(MessageId.FileEnumMessage);
            MessageReader.RegisterType<FileDeleteMessage>(MessageId.FileDeleteMessage);
        }

        public void OnFileEnumMessage(IMessage msg)
        {
            var enumMsg = (FileEnumMessage)msg;

            // TODO
        }

        public void OnFileDeleteMessage(IMessage msg)
        {
            var enumMsg = (FileDeleteMessage)msg;

            // TODO
        }
    }
}
