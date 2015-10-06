using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Manager
{
    public class FollowerObjectManager
    {
        private static FollowerObjectManager instance = new FollowerObjectManager();
        public static FollowerObjectManager Instance { get { return instance; } }

        private Dictionary<int, object> ObjDict { get; set; } = new Dictionary<int, object>();
        private object Locker { get; set; } = new object();

        public object GetObj(int id)
        {
            lock(Locker)
            {
                if (ObjDict.ContainsKey(id))
                {
                    return ObjDict[id];
                }
                return null;
            }
        }

        public void AddObj(int id, object obj)
        {
            lock (Locker)
            {
                ObjDict[id] = obj;
            }
        }
    }
}
