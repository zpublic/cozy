using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyQuick.Event;

namespace CozyQuick.Plugin.Timer
{
    public class TimerEvent : EventBase
    {
        private Dictionary<string, string> _Arg;

        public override string EventName
        {
            get { return "TimerEvent";  }
        }

        public void SetTimerId(string name)
        {
            _Arg = new Dictionary<string, string>();
            _Arg.Add("name", name);
            this.Switches = _Arg;
        }

        public static TimerEvent CreateEvent(string name)
        {
            TimerEvent e = new TimerEvent();
            e.SetTimerId(name);
            return e;
        }
    }
}
