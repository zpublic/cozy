using System.IO;

namespace CozyHammer.MnistSvm
{
    public class Utils
    {
        public static uint ReverseUint(uint value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }

        public static long GetFileSize(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            return fi.Length;
        }
    }
}
