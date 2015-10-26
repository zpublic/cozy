using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Container
{
    public interface ICacheContainer<K, V>
        where V : class
    {
        void SetMaxSize(int maxSize);
        V Get(K k);
        void Update(K k, V v);
        void Clear();
        void RemoveAll(Predicate<V> p);
    }
}
