using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Container
{
    public class MRUContainer<K, V> : ICacheContainer<K, V>
        where V : class
    {
        private V[] ObjectList;
        private BitArray BitFlag;
        private Dictionary<K, int> ObjectSet { get; set; } = new Dictionary<K, int>();

        public int MaxSize { get; set; }
        public int FreeNode { get; set; }

        public MRUContainer(int maxSize = 64)
        {
            SetMaxSize(maxSize);
        }

        public void SetMaxSize(int maxSize)
        {
            FreeNode    = maxSize;
            MaxSize     = maxSize;

            BitFlag     = new BitArray(MaxSize);
            Clear();
        }

        public V Get(K k)
        {
            if(ObjectSet.ContainsKey(k))
            {
                int pos = ObjectSet[k];
                return ObjectList[pos];
            }
            return null;
        }

        public void Update(K k, V v)
        {
            if (ObjectSet.ContainsKey(k))
            {
                int pos = ObjectSet[k];
                ObjectList[pos] = v;
            }
            else
            {
                // Cache Miss

                int pos = -1;
                for(int i = 0; i < BitFlag.Length; ++i)
                {
                    if(BitFlag.Get(i) == false)
                    {
                        pos = i;
                        break;
                    }
                }

                ObjectSet[k]    = pos;
                ObjectList[pos] = v;
                BitFlag.Set(pos, true);

                FreeNode--;
                if(FreeNode == 0)
                {
                    ReCache(pos);
                }
            }
        }

        private void ReCache(int pos)
        {
            BitFlag.SetAll(false);
            BitFlag.Set(pos, true);
            FreeNode = MaxSize - 1;
        }

        public void Clear()
        {
            BitFlag.SetAll(false);
            ObjectSet.Clear();
            ObjectList = new V[MaxSize];
        }

        public void RemoveAll(Predicate<V> p)
        {
            List<K> RemoveList = new List<K>();

            foreach(var obj in ObjectSet)
            {
                if(p(ObjectList[obj.Value]))
                {
                    RemoveList.Add(obj.Key);
                }
            }

            foreach(var obj in RemoveList)
            {
                int pos = ObjectSet[obj];
                ObjectSet.Remove(obj);
                BitFlag.Set(pos, false);
                FreeNode++;
            }
        }
    }
}
