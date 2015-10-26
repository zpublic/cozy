using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using CozyWiki.Container;

namespace CozyWiki.Cache
{
    public class PageCache
    {
        private MRUContainer<string, CacheBlock> CachePool { get; set; } = new MRUContainer<string, CacheBlock>();
        private Dictionary<string, CacheBlock> CacheDict { get; set; } = new Dictionary<string, CacheBlock>();

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
                maxSize = value;

                if (value > 0 && oldsize <= 0)
                {
                    SwitchCleanStatus(true);
                    CacheDict.Clear();
                }
                else if (value <= 0 && oldsize > 0)
                {
                    SwitchCleanStatus(false);
                    CachePool.MaxSize = value;
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
                Predicate<CacheBlock> pre = x =>
                {
                    if ((now - x.CacheTime).TotalMilliseconds > Timeout)
                    {
                        return true;
                    }
                    return false;
                };

                if (MaxSize != 0)
                {
                    CachePool.RemoveAll(pre);
                }
                else
                {
                    List<string> RemoveList = new List<string>();
                    foreach(var obj in CacheDict)
                    {
                        if(pre(obj.Value))
                        {
                            RemoveList.Add(obj.Key);
                        }
                    }
                    foreach(var obj in RemoveList)
                    {
                        CacheDict.Remove(obj);
                    }
                }
            }
        }

        public CacheBlock GetCache(string key)
        {
            lock (Locker)
            {
                if(MaxSize != 0)
                {
                    return CachePool.Get(key);
                }
                else
                {
                    if(CacheDict.ContainsKey(key))
                    {
                        return CacheDict[key];
                    }
                    return null;
                }
            }
        }

        public void Clear()
        {
            lock (Locker)
            {
                CachePool.Clear();
                CacheDict.Clear();
            }
        }

        public void Update(string key, CacheBlock block)
        {
            lock (Locker)
            {
                if(MaxSize != 0)
                {
                    CachePool.Update(key, block);
                }
                else
                {
                    CacheDict[key] = block;
                }
            }
        }
    }
}
