namespace CozyAnywhere.Protocol
{
    public static class MessageId
    {
        public const uint CommandMessage                = 1000;
        public const uint FileEnumMessage               = 1001;
        public const uint FileEnumMessageRsp            = 1002;
        public const uint ProcessEnumMessage            = 1003;
        public const uint ProcessEnumMessageRsp         = 1004;
        public const uint ProcessTerminateMessage       = 1005;
        public const uint FileDeleteMessage             = 1006;
        public const uint CommandMessageRsp             = 1007;
        public const uint PluginLoadMessage             = 1008;
        public const uint PluginQueryMessage            = 1009;
        public const uint BinaryPacketMessage           = 1010;
        public const uint ConnectionTypeQueryMessage    = 1011;
        public const uint ConnectionTypeQueryMessageRsp = 1012;
        public const uint CloseConnectionMessage        = 1013;
        public const uint ConnectMessage                = 1014;
        public const uint ConnectMessageRsp             = 1015;
    }
}