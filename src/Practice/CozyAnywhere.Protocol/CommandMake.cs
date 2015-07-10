using System.Text;

namespace CozyAnywhere.Protocol
{
    public static class CommandMake
    {
        public static void AppendHeader(StringBuilder builder, string pluginName)
        {
            builder.Append(pluginName);
            builder.Append(':');
        }

        public static void AppendMethodName(StringBuilder builder, string Name)
        {
            builder.Append(Name);
            builder.Append(':');
        }

        public static void AppendArguments(StringBuilder builder, params  object[] args)
        {
            int l = args.Length;
            for (int i = 0; i < l; ++i)
            {
                builder.Append(args[i].ToString());
                if (i != l - 1)
                {
                    builder.Append(',');
                }
            }
        }

        public static void AppendFooter(StringBuilder builder)
        {
            builder.Append(';');
        }
    }
}