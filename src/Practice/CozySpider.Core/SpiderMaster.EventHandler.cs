using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozySpider.Core.Event;
using System.Threading.Tasks;

namespace CozySpider.Core
{
    public partial class SpiderMaster
    {
        public EventHandler<AddUrlEventArgs> AddUrlEventHandler;

        public EventHandler<DataReceivedEventArgs> DataReceivedEventHandler;


        private void OnAddUrlEventHandler(object sender, AddUrlEventArgs args)
        {
            if (AddUrlEventHandler != null)
            {
                AddUrlEventHandler(sender, args);
            }
        }

        private void OnDataReceivedEventHandler(object sender, DataReceivedEventArgs args)
        {
            if(DataReceivedEventHandler != null)
            {
                DataReceivedEventHandler(sender, args);
            }
        }
    }
}
