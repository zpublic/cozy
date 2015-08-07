using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CozySpider.Core.Model
{
    public partial class SpiderWorkerList
    {
        public long AllWorkersCount { get; private set; }

        private long freeWokersCount;

        public long FreeWorkersCount
        {
            get
            {
                return ReadWorkerCount();
            }
        }

        public bool IsWorkersFree
        {
            get
            {
                return FreeWorkersCount == AllWorkersCount;
            }
        }

        public void AddWrokerCount()
        {
            Interlocked.Increment(ref freeWokersCount);
        }

        public void SubWorkerCount()
        {
            Interlocked.Decrement(ref freeWokersCount);
        }

        public long ReadWorkerCount()
        {
            return Interlocked.Read(ref freeWokersCount);
        }
    }
}
