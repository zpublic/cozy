using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace CozyLua.Core
{
    public class CozyLuaFunction
    {
        private LuaFunction mFunc;

        public CozyLuaFunction(LuaFunction func)
        {
            mFunc = func;
        }

        public object[] Call(params object[] args)
        {
            return mFunc.Call(args);
        }
    }
}
