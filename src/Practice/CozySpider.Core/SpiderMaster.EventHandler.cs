using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozySpider.Core.Event;
using System.Threading.Tasks;
using CozySpider.Core.Model;

namespace CozySpider.Core
{
    public partial class SpiderMaster
    {
        public EventHandler<AddUrlEventArgs> AddUrlEventHandler;

        public EventHandler<DataReceivedEventArgs> DataReceivedEventHandler;

        public EventHandler<ErrorEventArgs> ErrorEventHandler;


        private void OnAddUrlEventHandler(object sender, AddUrlEventArgs args)
        {
            bool isExist = urlPool.Add(args.Url);
            if (!isExist)
            {
                urlQueue.EnQueue(new UrlInfo(args.Url, args.Depth + 1));

            }

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

        private void OnErrorEventHandler(object sender, ErrorEventArgs args)
        {
            if(ErrorEventHandler != null)
            {
                ErrorEventHandler(sender, args);
            }
        }

        private void RegisterEventHandler()
        {
            Workers.AddUrlEventAction   = new Action<object, AddUrlEventArgs>(OnAddUrlEventHandler);
            Workers.DataReceivedAction  = new Action<object, DataReceivedEventArgs>(OnDataReceivedEventHandler);
            Workers.ErrorAction         = new Action<object, ErrorEventArgs>(OnErrorEventHandler);
        }
    }
}
