namespace CozyAnywhere.PluginBase
{
    public class PluginMethodReturnValueType
    {
        public const byte NoDataType        = 0;
        public const byte StringDataType    = 1;
        public const byte BinaryDataType    = 2;

        public byte DataType { get; set; }

        public object Data { get; set; }
    }
}