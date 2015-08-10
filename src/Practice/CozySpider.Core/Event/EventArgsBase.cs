using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Event
{
    public class EventArgsBase : EventArgs
    {
        public string Url { get; set; }

        public EventArgsBase(string url)
        {
            Url = url;
        }

        public virtual string Message { get { return Url; } }
    }
}
