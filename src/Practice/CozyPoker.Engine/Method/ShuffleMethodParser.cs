using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Method
{
    public class ShuffleMethodParser
    {
        public static ShuffleMethod Parse(string s)
        {
            var ss = s.Split(':');
            switch (ss[0])
            {
                case "cpp":
                    return cppShuffleMethod(ss[1]);
                case "lua":
                    return luaShuffleMethod(ss[1]);
            }
            return null;
        }

        // cpp的先不写了
        private static ShuffleMethod cppShuffleMethod(string s)
        {
            return null;
        }

        private static ShuffleMethod luaShuffleMethod(string s)
        {
            return new ShuffleMethod_Lua(s);
        }
    }
}
