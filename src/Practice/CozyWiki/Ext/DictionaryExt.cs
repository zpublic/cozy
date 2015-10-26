using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Ext
{
    public static class DictionaryExt
    {
        public static void RemoveAll<K, V>(this Dictionary<K, V> dict, Predicate<V> pre)
        {
            List<K> RemoveList = new List<K>();
            foreach (var obj in dict)
            {
                if (pre(obj.Value))
                {
                    RemoveList.Add(obj.Key);
                }
            }

            foreach (var obj in RemoveList)
            {
                dict.Remove(obj);
            }
        }
    }
}
