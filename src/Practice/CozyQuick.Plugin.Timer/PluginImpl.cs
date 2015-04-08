using CozyQuick.Interface;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Windows;
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
            //给创建完成Timer事件注册一个方法
            _window.EventCreateTimer += WindowOnEventCreateTimer;
            return true;
        }

        private void WindowOnEventCreateTimer(string timerName, string second)
        {

            Observable.Start(() =>
            {
                System.Threading.Thread.Sleep(int.Parse(second) * 1000);
                _disp.OnReceiveEvent(TimerEvent.CreateEvent(timerName));
            });
            //MessageBox.Show(string.Format("Timer名称:{0}\n {1}秒后触发", timerName, second));
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
            return true;
        }
    }
}
