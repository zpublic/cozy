using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Event
{
    public class AddUrlEventArgs : EventArgs
    {
        public String Url { get; set; }

        public AddUrlEventArgs(string url)
        {
            Url = url;
        }
    }
}
