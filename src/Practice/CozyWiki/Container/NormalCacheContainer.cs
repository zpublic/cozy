using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyWiki.Ext;

namespace CozyWiki.Container
{
    public class NormalCacheContainer<K, V> : ICacheContainer<K, V>
        where V : class
    {
        private Dictionary<K, V> ObjectSet { get; set; } = new Dictionary<K, V>();

        public void SetMaxSize(int maxSize)
        {
            // Nothing to do
        }

        public V Get(K k)
        {
            if(ObjectSet.ContainsKey(k))
            {
                return ObjectSet[k];
            }
            return null;
        }

        public void Update(K k, V v)
        {
            ObjectSet[k] = v;
        }

        public void Clear()
        {
            ObjectSet.Clear();
        }

        public void RemoveAll(Predicate<V> p)
        {
            ObjectSet.RemoveAll(p);
        }
    }
}
