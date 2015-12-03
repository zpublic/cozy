using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Collections;
using CozyCrawler.Interface;
using Autofac.Core;

namespace CozyCrawler.Runner
{
    public class RunnerIoCContainer
    {
        private static RunnerIoCContainer _Instance = new RunnerIoCContainer();
        public static RunnerIoCContainer Instance { get { return _Instance; } }

        private IContainer InnerContainer { get; set; }

        public void Init(RunnerIoCContainerSetting setting)
        {
            var builder = new ContainerBuilder();
            foreach(var t in setting.Types)
            {
                builder.RegisterType(t.Value).As(t.Key);
            }

            InnerContainer = builder.Build();
        }

        public T Resolve<T>()
        {
            using (var scope = InnerContainer.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }
    }
}
