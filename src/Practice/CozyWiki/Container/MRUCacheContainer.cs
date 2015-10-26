using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Container
{
    public class MRUCacheContainer<K, V> : ICacheContainer<K, V>
        where V : class
    {
        private V[] ObjectList;
        private BitArray BitFlag;
        private Dictionary<K, int> ObjectSet { get; set; } = new Dictionary<K, int>();

        public int MaxSize { get; set; }
        public int FreeNode { get; set; }

        public MRUCacheContainer(int maxSize = 64)
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

        /// <summary>
        /// 如果获取到元素 返回元素 否则返回null
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public V Get(K k)
        {
            if(ObjectSet.ContainsKey(k))
            {
                int pos = ObjectSet[k];
                return ObjectList[pos];
            }
            return null;
        }

        /// <summary>
        /// insert或exchange元素
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v"></param>
        public void Update(K k, V v)
        {
            // 如果元素存在 直接修改元素
            if (ObjectSet.ContainsKey(k))
            {
                int pos = ObjectSet[k];
                ObjectList[pos] = v;
            }
            else // 元素不存在 缓存一个新元素
            {
                // 获取到最左边的被标记为空的位置
                int pos = -1;
                for(int i = 0; i < BitFlag.Length; ++i)
                {
                    if(BitFlag.Get(i) == false)
                    {
                        pos = i;
                        break;
                    }
                }

                // 将value填入对应位置 标记为已使用
                ObjectSet[k]    = pos;
                ObjectList[pos] = v;
                BitFlag.Set(pos, true);

                //减少空位置计数 如果变成0 则进行ReCache操作
                FreeNode--;
                if(FreeNode == 0)
                {
                    ReCache(pos);
                }
            }
        }

        /// <summary>
        /// 不删除元素 只把除最后添加的元素的位置外的所有位置标记为空 重置空位置计数
        /// </summary>
        /// <param name="pos"></param>
        private void ReCache(int pos)
        {
            BitFlag.SetAll(false);
            BitFlag.Set(pos, true);
            FreeNode = MaxSize - 1;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            BitFlag.SetAll(false);
            ObjectSet.Clear();
            Array.Clear(ObjectList, 0, ObjectList.Length);
        }

        /// <summary>
        /// 根据条件移除元素
        /// </summary>
        /// <param name="p"></param>
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
