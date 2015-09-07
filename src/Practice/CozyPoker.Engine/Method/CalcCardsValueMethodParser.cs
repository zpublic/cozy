using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Method
{
    public class CalcCardsValueMethodParser
    {
        public static CalcCardsValueMethod Parse(string s)
        {
            var ss = s.Split(':');
            switch (ss[0])
            {
                case "cpp":
                    return cppCalcCardsValueMethod(ss[1]);
                case "lua":
                    return luaCalcCardsValueMethod(ss[1]);
            }
            return null;
        }

        // cpp的先不写了
        private static CalcCardsValueMethod cppCalcCardsValueMethod(string s)
        {
            return null;
        }

        private static CalcCardsValueMethod luaCalcCardsValueMethod(string s)
        {
            return new CalcCardsValueMethod_Lua(s);
        }
    }
}
