using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Engine.Util
{
    public static class PathTransform
    {
        public static string PackagePath { get; } = @"../doc/CozyAdventureScript/";

        public static string LuaScript(string name)
        {
            return PackagePath + name + ".lua";
        }
    }
}
