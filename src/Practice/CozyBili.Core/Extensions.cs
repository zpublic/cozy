using System;
using System.Linq;

namespace CozyBili.Core {

    public static class Extensions {

        public static byte[] ToBE(this byte[] b) {
            var result = BitConverter.IsLittleEndian ? b.Reverse().ToArray() : b;
            return result;
        }
    }
}
