using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using System.Collections;

namespace CozyLua.Core
{
    public class CozyLuaTable
    {
        private LuaTable mTable;

        public CozyLuaTable(LuaTable table)
        {
            mTable = table;
        }

        public object this[object field]
        {
            get { return mTable[field]; }
            set { mTable[field] = value; }
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return mTable.GetEnumerator();
        }

        public ICollection Keys
        {
            get { return mTable.Keys; }
        }

        public ICollection Values
        {
            get { return mTable.Values; }
        }
    }
}
