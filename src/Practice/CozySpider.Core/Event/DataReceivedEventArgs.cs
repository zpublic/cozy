using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Event
{
    public class DataReceivedEventArgs : EventArgsBase
    {
        public DataReceivedEventArgs(string url)
            :base(url)
        {

        }
    }
}
