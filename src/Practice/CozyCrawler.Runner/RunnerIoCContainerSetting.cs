using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Runner
{
    public class RunnerIoCContainerSetting
    {
        public Dictionary<Type, Type> Types { get; set; } = new Dictionary<Type, Type>();

        public void RegisterType(Type InterfaceType, Type ImplmentType)
        {
            Types[InterfaceType] = ImplmentType;
        }

        public void RegisterType<T, U>()
            where U : T
        {
            Types[typeof(T)] = typeof(U);
        }
    }
}
