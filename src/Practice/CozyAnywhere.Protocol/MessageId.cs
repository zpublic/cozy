namespace CozyAnywhere.Protocol
{
    public static class MessageId
    {
        public const uint CommandMessage            = 1000;
        public const uint FileEnumMessage           = 1001;
        public const uint FileEnumMessageRsp        = 1002;
        public const uint ProcessEnumMessage        = 1003;
        public const uint ProcessEnumMessageRsp     = 1004;
        public const uint ProcessTerminateMessage   = 1005;
        public const uint FileDeleteMessage         = 1006;
    }
}