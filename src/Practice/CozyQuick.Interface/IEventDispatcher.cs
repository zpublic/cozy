using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CozyDef = CozyQuick.Event;

namespace CozyQuick.Interface
{
    public interface IEventDispatcher
    {
        void OnReceiveEvent(CozyDef.EventBase context);
    }
}
