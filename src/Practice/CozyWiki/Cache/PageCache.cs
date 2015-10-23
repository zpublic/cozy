using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Cache
{
    public class PageCache
    {
        private Dictionary<string, CacheBlock> CachePool { get; set; } 
            = new Dictionary<string, CacheBlock>();
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

        public CacheBlock GetCache(string key)
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

        public void Update(string key, CacheBlock block)
        {
            lock(Locker)
            {
                CachePool[key] = block;
            }
        }
    }
}
