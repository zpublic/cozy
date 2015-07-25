using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NetworkProtocol;
using NetworkHelper;

namespace CozyAnywhere.RelayServerCore
{
    public partial class AnywhereRelayServer
    {
        private void InitMessage()
        {
            Assembly asm = Assembly.Load("CozyAnywhere.Protocol");
            if (asm != null)
            {
                foreach (Type type in asm.GetTypes())
                {
                    if (type.Namespace == "CozyAnywhere.Protocol.Messages")
                    {
                        uint id = ((IMessage)Activator.CreateInstance(type)).Id;
                        MessageReader.RegisterType(type, id);
                    }
                }
            }
        }
    }
}
