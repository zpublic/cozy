using CozyAdventure.Model;
using CozyLua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Engine.Module.Logic
{
    public class FollowerCollectModule : ModuleBase
    {
        public override ModuleTypeEnum ModuleType
        {
            get
            {
                return ModuleTypeEnum.Logic;
            }
        }

        #region LuaFunc
        CozyLuaFunction GetAttackFunc { get; set; }

        CozyLuaFunction GetHpFunc { get; set; }

        public int GetAttack(FollowerCollect fc)
        {
            return (int)(double)GetAttackFunc.Call(fc)[0];
        }

        public int GetHp(FollowerCollect fc)
        {
            return (int)(double)GetHpFunc.Call(fc)[0];
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
