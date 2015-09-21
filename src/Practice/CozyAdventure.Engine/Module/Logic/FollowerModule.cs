using CozyAdventure.Model;
using CozyLua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Engine.Module.Logic
{
    public class FollowerModule : ModuleBase
    {
        public override ModuleTypeEnum ModuleType
        {
            get
            {
                return ModuleTypeEnum.Logic;
            }
        }
        #region LuaFunc
        CozyLuaFunction GetGrowAttackFunc { get; set; }

        CozyLuaFunction GetAttackFunc { get; set; }

        public int GetGrowAttack(int star, int level)
        {
            return (int)GetGrowAttackFunc.Call(star, level)[0];
        }

        public int GetAttack(Follower f)
        {
            return (int)GetAttackFunc.Call(f)[0];
        }

        #endregion

        public override bool Init(string script)
        {
            if (!base.Init(script))
            {
                return false;
            }

            GetGrowAttackFunc   = GetTableFunction("GetGrowAttack");
            GetAttackFunc       = GetTableFunction("GetAttack");

            if (GetGrowAttackFunc == null || GetAttackFunc == null)
            {
                return false;
            }

            return true;
        }
    }
}
