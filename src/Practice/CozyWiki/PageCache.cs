using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki
{
    public class PageCache
    {
        private Dictionary<string, Tuple<string, DateTime>> CachePool { get; set; } 
            = new Dictionary<string, Tuple<string, DateTime>>();
        private readonly object Locker = new object();

        public void Remove(string key)
        {
            lock(Locker)
            {
                if (CachePool.ContainsKey(key))
                {
                    CachePool.Remove(key);
                }
            }
        }

        public Tuple<string, DateTime> GetCache(string key)
        {
            lock(Locker)
            {
                if (CachePool.ContainsKey(key))
                {
                    return CachePool[key];
                }
                return null;
            }
        }

        public void Clear()
        {
            lock(Locker)
            {
                CachePool.Clear();
            }
        }

        public void Update(string key, string value, DateTime change)
        {
            lock(Locker)
            {
                CachePool[key] = Tuple.Create(value, change);
            }
        }
    }
}
