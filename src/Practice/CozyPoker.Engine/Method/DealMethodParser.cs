using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Method
{
    public class DealMethodParser
    {
        public static DealMethod Parse(string s)
        {
            var ss = s.Split(':');
            switch (ss[0])
            {
                case "cpp":
                    return cppDealMethod(ss[1]);
                case "lua":
                    return luaDealMethod(ss[1]);
            }
            return null;
        }

        // cpp的先不写了
        private static DealMethod cppDealMethod(string s)
        {
            return null;
        }

        private static DealMethod luaDealMethod(string s)
        {
            return new DealMethod_Lua(s);
        }
    }
}
