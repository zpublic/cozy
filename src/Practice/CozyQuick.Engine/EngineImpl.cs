using CozyQuick.Interface;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyQuick.Engine
{
    public class EngineImpl : IEventDispatcher
    {
        //[Import]
        private IEventPublish _pub;

       // [Import]
        private IEventSubscribe _sub;

        public void Init()
        {
            var a1 = System.Reflection.Assembly.LoadFile(Environment.CurrentDirectory + "/CozyQuick.Plugin.Timer.dll");
            var a2 = System.Reflection.Assembly.LoadFile(Environment.CurrentDirectory + "/CozyQuick.Plugin.Msgbox.dll");
            var configuration1 = new ContainerConfiguration();
            configuration1.WithAssembly(a1);
            var c1 = configuration1.CreateContainer();
            _pub = c1.GetExport<IEventPublish>();
            var configuration2 = new ContainerConfiguration();
            configuration2.WithAssembly(a2);
            var c2 = configuration2.CreateContainer();
            _sub = c2.GetExport<IEventSubscribe>();

            _pub.Init(this);
            _pub.ShowPublishConfigurePanel();
        } 

        public void OnReceiveEvent(Event.EventBase context)
        {
            _sub.OnReceiveEvent(context);
        }
    }
}
