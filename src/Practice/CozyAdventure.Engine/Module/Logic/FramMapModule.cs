using CozyLua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Engine.Module.Logic
{
    public class FramMapModule : ModuleBase
    {
        public override ModuleTypeEnum ModuleType
        {
            get
            {
                return ModuleTypeEnum.Logic;
            }
        }

        #region LuaFunc

        private CozyLuaFunction RequirementFunc { get; set; }
        private CozyLuaFunction ExpFunc { get; set; }
        private CozyLuaFunction MoneyFunc { get; set; }

        public int Requirement(int level)
        {
            return (int)(double)RequirementFunc.Call(level)[0];
        }

        public int Exp(int level)
        {
            return (int)(double)ExpFunc.Call(level)[0];
        }

        public int Money(int level)
        {
            return (int)(double)MoneyFunc.Call(level)[0];
        }

        #endregion

        public override bool Init(string script)
        {
            if (!base.Init(script))
            {
                return false;
            }

            RequirementFunc = GetTableFunction("Requirement");
            ExpFunc         = GetTableFunction("Exp");
            MoneyFunc       = GetTableFunction("Money");

            if (RequirementFunc == null || ExpFunc == null || MoneyFunc == null)
            {
                return false;
            }

            return true;
        }
    }
}
