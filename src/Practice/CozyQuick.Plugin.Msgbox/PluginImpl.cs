using CozyQuick.Interface;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyQuick.Plugin.Msgbox
{
    [Export(typeof(IEventSubscribe))]
    public class PluginImpl : IEventSubscribe
    {
        public void OnReceiveEvent(Event.EventBase context)
        {
            CozyQuick.CommonDialogs.MessageBox.Show(
                IntPtr.Zero,
                context.EventName,
                context.Switches["name"].ToString(),
                0);
        }
    }
}
