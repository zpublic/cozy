using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyQuick.Event
{
    public class EventBase
    {
        public virtual string EventName { get; internal set; }
        public IEnumerable<string> Arguments { get; set; }
        public IDictionary<string, string> Switches { get; set; }
    }
}
