using CozyLua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Engine.Module
{
    public class FollowerCollectModule : ModuleBase
    {
        #region LuaFunc
        CozyLuaFunction GetAttackFunc { get; set; }

        CozyLuaFunction GetHpFunc { get; set; }

        public object GetAttack(object fc)
        {
            return GetAttackFunc.Call(fc)[0];
        }

        public object GetHp(object fc)
        {
            return GetHpFunc.Call(fc)[0];
        }

        #endregion

        public override bool Init(string script)
        {
            if (!base.Init(script))
            {
                return false;
            }

            GetAttackFunc = GetTableFunction("GetAttack");
            GetHpFunc = GetTableFunction("GetHp");

            if (GetAttackFunc == null || GetHpFunc == null)
            {
                return false;
            }

            return true;
        }
    }
}
