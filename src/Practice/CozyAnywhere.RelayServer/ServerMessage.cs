using NetworkHelper;

namespace CozyAnywhere.RelayServerCore
{
    public partial class AnywhereRelayServer
    {
        private void InitMessage()
        {
            MessageReader.RegisterTypeWithAssembly("CozyAnywhere.Protocol", "CozyAnywhere.Protocol.Messages");
        }
    }
}
