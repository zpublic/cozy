namespace CozyAnywhere.PluginBase
{
    public class PluginMethodReturnValueType
    {
        public static readonly PluginMethodReturnValueType NoReturnValueType = new PluginMethodReturnValueType()
        {
            DataType = NoDataType,
        };

        public const byte NoDataType            = 0;    // null
        public const byte StringDataType        = 1;    // "xxx" => string
        public const byte BinaryDataType        = 2;    //  0xFF => byte[]
        public const byte PacketBinaryDataType  = 3;    // PluginMehtodReturnValuePacket

        public byte DataType { get; set; }

        public object Data { get; set; }
    }
}