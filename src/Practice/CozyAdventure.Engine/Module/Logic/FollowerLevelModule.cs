using CozyLua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Engine.Module.Logic
{
    public class FollowerLevelModule : ModuleBase
    {
        public override ModuleTypeEnum ModuleType
        {
            get
            {
                return ModuleTypeEnum.Logic;
            }
        }

        #region LuaFunc

        private CozyLuaFunction CanUpgradeFunc { get; set; }
        private CozyLuaFunction UpgradeRequireFunc { get; set; }
        private CozyLuaFunction UpgradeFunc { get; set; }

        public object CanUpgrade(object Follower)
        {
            return CanUpgradeFunc.Call(Follower)[0];
        }

        public object UpgradeRequire(object Follower)
        {
            return UpgradeRequireFunc.Call(Follower)[0];
        }

        public object Upgrade(object Follower)
        {
            return UpgradeFunc.Call(Follower)[0];
        }

        #endregion

        public override bool Init(string script)
        {
            if(!base.Init(script))
            {
                return false;
            }

            CanUpgradeFunc      = GetTableFunction("CanUpgrade");
            UpgradeRequireFunc  = GetTableFunction("UpgradeRequire");
            UpgradeFunc         = GetTableFunction("Upgrade");

            if(CanUpgradeFunc == null || UpgradeRequireFunc == null || UpgradeFunc == null)
            {
                return false;
            }

            return true;
        }
    }
}
