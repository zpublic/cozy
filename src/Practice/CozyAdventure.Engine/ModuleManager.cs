using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Engine.Module;
using System.Reflection;

namespace CozyAdventure.Engine
{
    public class ModuleManager
    {
        private static ModuleManager instance = new ModuleManager();
        public static ModuleManager Instance
        {
            get
            {
                return instance;
            }
        }

        private Dictionary<string, ModuleBase> Modules { get; set; }

        public void Init()
        {

            Modules = new Dictionary<string, ModuleBase>();

            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach(var type in types)
            {
                if(type.BaseType == typeof(ModuleBase) && type != typeof(ModuleBase))
                {
                    var instance = (ModuleBase)Activator.CreateInstance(type);
                    if(instance.Init(instance.ModuleType.ToString() + "/" + type.Name))
                    {
                        Modules[type.Name] = instance;
                    }
                }
            }
        }

        public ModuleBase GetModule(string name)
        {
            if(Modules.ContainsKey(name))
            {
                return Modules[name];
            }
            return null;
        }
    }
}
