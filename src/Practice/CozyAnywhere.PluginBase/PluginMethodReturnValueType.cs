namespace CozyAnywhere.PluginBase
{
    public class PluginMethodReturnValueType
    {
        public static readonly PluginMethodReturnValueType NoReturnValueType = new PluginMethodReturnValueType()
        {
            DataType = NoDataType,
        };

        public const byte NoDataType        = 0;
        public const byte StringDataType    = 1;
        public const byte BinaryDataType    = 2;

        public byte DataType { get; set; }

        public object Data { get; set; }
    }
}