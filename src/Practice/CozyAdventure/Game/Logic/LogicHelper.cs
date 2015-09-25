using CozyAdventure.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public static class LogicHelper
    {
        public static object[] CallThisFunc(string moduleName, MethodBase method, params object[] args)
        {
            return ModuleManager.Instance.GetModule(moduleName).CallFunc(method.Name, args);
        }
    }
}
