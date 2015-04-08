using CozyQuick.Interface;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using CozyQuick.Plugin.Timer.Ui;

namespace CozyQuick.Plugin.Timer
{
    [Export(typeof(IEventPublish))]
    public class PluginImpl : IEventPublish
    {
        private IEventDispatcher _disp = null;
        private CreateTimerWindow _window = new CreateTimerWindow();

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
            _window.Show();
//             Observable.Start(() =>
//             {
//                 System.Threading.Thread.Sleep(2000);
//                 _disp.OnReceiveEvent(TimerEvent.CreateEvent(10086));
//             })
            return true;
        }
    }
}
