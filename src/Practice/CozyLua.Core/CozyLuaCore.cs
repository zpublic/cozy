using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLua.Core
{
    public class CozyLuaCore
    {
        private NLua.Lua mLua;

        public CozyLuaCore()
        {
            mLua = new NLua.Lua();
        }

        public object[] DoString(string sLua)
        {
            return mLua.DoString(sLua);
        }

        public object[] DoFile(string sLuaFile)
        {
            return mLua.DoFile(sLuaFile);
        }

        public object this [string fullPath]
        {
            get
            {
                return mLua[fullPath];
            }
            set
            {
                mLua[fullPath] = value;
            }
        }

        public double GetNumber(string fullPath)
        {
            return mLua.GetNumber(fullPath);
        }

        public string GetString(string fullPath)
        {
            return mLua.GetString(fullPath);
        }

        public CozyLuaTable GetTable(string fullPath)
        {
            NLua.LuaTable table = mLua.GetTable(fullPath);
            if (table != null)
            {
                return new CozyLuaTable(table);
            }
            return null;
        }

        public CozyLuaFunction GetFunction(string fullPath)
        {
            NLua.LuaFunction func = mLua.GetFunction(fullPath);
            if (func != null)
            {
                return new CozyLuaFunction(func);
            }
            return null;
        }
    }
}
