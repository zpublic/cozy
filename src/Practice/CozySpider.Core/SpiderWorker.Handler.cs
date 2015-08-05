using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core.Event;

namespace CozySpider.Core
{
    public partial class SpiderWorker
    {
        public EventHandler<AddUrlEventArgs> AddUrlEventHandler;

        public EventHandler<DataReceivedEventArgs> DataReceivedEventHandler;
    }
}
