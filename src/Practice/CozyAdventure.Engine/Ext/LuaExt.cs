using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLua.Core;

namespace CozyAdventure.Engine.Ext
{
    public static class LuaExt
    {
        public static void InitEnvironment(this CozyLuaCore lua)
        {
            lua.LoadCLRPackage();
            lua.DoString("import ('CozyAdventure.Engine', 'CozyPoker.CozyAdventure.Util')");
            lua.DoString("import ('CozyAdventure', 'CozyAdventure.Game.Model')");
        }

        public static void InitPath(this CozyLuaCore lua)
        {
            lua.DoString(@" local p = '../doc/CozyAdventureScript/'
                            local m_package_path = package.path
                            package.path = string.format("" % s;% s?.lua;% s ?/ init.lua"", m_package_path, p, p)");
        }
    }
}
