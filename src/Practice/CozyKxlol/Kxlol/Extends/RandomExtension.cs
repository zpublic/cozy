using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Kxlol.Extends
{
    public static class RandomExtension
    {
        private const string BaseStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
        public static string NextString(this Random rd, int size)
        {
            StringBuilder Builder = new StringBuilder();
            for(int i = 0; i < size; ++i)
            {
                Builder.Append(BaseStr[rd.Next(BaseStr.Length)]);
            }
            return Builder.ToString();
        }
    }
}
