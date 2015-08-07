using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Model
{
    public partial class SpiderWorkerList
    {
        public void OnBeginWork()
        {
            SubWorkerCount();
        }

        public void OnFinishWork()
        {
            AddWrokerCount();
        }
    }
}
