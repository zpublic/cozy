using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Model;
using System.IO;
using CozyAdventure.Engine.Ext;
using CozyAdventure.Engine.Util;

namespace CozyAdventure.Engine.Module.Data
{
    public static class FollowerPackageModule
    {
        public static FollowerPackage GetFollowerPackages()
        {
            var name = @"Data/FollowerPackage";
            var result = new FollowerPackage();

            int i = 1;
            while (File.Exists(name + i))
            {
                var lua = new CozyLua.Core.CozyLuaCore();
                lua.InitEnvironment();

                lua.DoFile(PathTransform.LuaScript(name + i));
                var retVal = (FollowerPackage)lua.DoString("GetFollowerPackage")[0];
                result.Followers.AddRange(retVal.Followers);
                ++i;
            }
            return null;
        }
    }
}
