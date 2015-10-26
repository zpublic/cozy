using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CozyWiki.Container;

namespace CozyWiki.Cache
{
    public class PageCache
    {
        private ICacheContainer<string, CacheBlock> CachePool { get; set; }

        private readonly object Locker = new object();

        System.Timers.Timer timer { get; set; }

        private int timeout { get; set; }
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
                UpdateTimeout();
            }
        }

        private int maxSize { get; set; }
        public int MaxSize
        {
            get
            {
                return maxSize;
            }
            set
            {
                int oldsize = maxSize;
                maxSize     = value;

                if (value > 0 && oldsize <= 0)
                {
                    CachePool = new MRUContainer<string, CacheBlock>(MaxSize);
                    SwitchCleanStatus(true);
                }
                else if (value <= 0 && oldsize > 0)
                {
                    CachePool = new NormalCacheContainer<string, CacheBlock>();
                    SwitchCleanStatus(false);
                }
            }
        }

        private void UpdateTimeout()
        {
            if (timer != null)
            {
                timer.Interval = Timeout;
            }
        }

        private void SwitchCleanStatus(bool turnOn)
        {
            if (timer == null)
            {
                timer = new System.Timers.Timer()
                {
                    Interval = (Timeout != 0 ? Timeout : int.MaxValue),
                };

                timer.Elapsed += new ElapsedEventHandler(OnTimerProcess);
            }

            timer.Enabled = turnOn;
        }

        private void OnTimerProcess(object sender, ElapsedEventArgs e)
        {
            lock (Locker)
            {
                DateTime now = DateTime.Now;

                CachePool.RemoveAll(x =>
                {
                    if ((now - x.CacheTime).TotalMilliseconds > Timeout)
                    {
                        return true;
                    }
                    return false;
                });
            }
        }

        public CacheBlock GetCache(string key)
        {
            lock (Locker)
            {
                return CachePool.Get(key);
            }
        }

        public void Clear()
        {
            lock (Locker)
            {
                CachePool.Clear();
            }
        }

        public void Update(string key, CacheBlock block)
        {
            lock (Locker)
            {
                CachePool.Update(key, block);
            }
        }
    }
}
