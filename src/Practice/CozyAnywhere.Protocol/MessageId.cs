namespace CozyAnywhere.Protocol
{
    public static class MessageId
    {
        public const uint CommandMessage                = 1000;
        public const uint CommandMessageRsp             = 1001;
        public const uint PluginLoadMessage             = 1002;
        public const uint PluginQueryMessage            = 1003;
        public const uint BinaryPacketMessage           = 1004;
        public const uint ConnectMessage                = 1005;
        public const uint ConnectMessageRsp             = 1006;
        public const uint QueryConnectMessage           = 1007;
        public const uint QueryConnectMessageRsp        = 1008;
    }
}