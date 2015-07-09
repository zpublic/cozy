using System;

namespace CozyAnywhere.Plugin.WinFile.Ext
{
    public static class UnsignedLongConvertToDateTimeExt
    {
        public static DateTime ToDateTime(this ulong nanoseconds)
        {
            DateTime pointOfReference   = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long ticks                  = (long)(nanoseconds / 100);
            return pointOfReference.AddTicks(ticks);
        }
    }
}