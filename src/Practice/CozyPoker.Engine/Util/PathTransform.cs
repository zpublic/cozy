using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Util
{
    public class PathTransform
    {
        public static string LuaScript(string name)
        {
            return "../../doc/CozyPokerScript/" + name + ".lua";
        }
    }
}
