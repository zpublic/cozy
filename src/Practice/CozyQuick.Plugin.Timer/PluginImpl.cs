using CozyQuick.Interface;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Reactive.Linq;

namespace CozyQuick.Plugin.Timer
{
    [Export(typeof(IEventPublish))]
    public class PluginImpl : IEventPublish
    {
        private IEventDispatcher _disp = null;

        public bool Init(IEventDispatcher disp)
        {
            _disp = disp;
            return true;
        }

        public bool Uninit()
        {
            return true;
        }

        public string Name()
        {
            return "Timer";
        }

        public bool ShowPublishConfigurePanel()
        {
            Observable.Start(() =>
            {
                System.Threading.Thread.Sleep(2000);
                _disp.OnReceiveEvent(TimerEvent.CreateEvent(10086));
            })
            .Delay(new TimeSpan(4000))
            .Subscribe(x => _disp.OnReceiveEvent(TimerEvent.CreateEvent(65535)));
            return true;
        }
    }
}
