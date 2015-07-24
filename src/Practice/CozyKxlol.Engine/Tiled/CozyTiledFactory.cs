using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled
{
    public static class CozyTiledFactory
    {
        private static Dictionary<uint, CozyTiledNode> NodeDictionary
            = new Dictionary<uint, CozyTiledNode>();

        private static object _objLock = new object();

        static CozyTiledFactory()
        {
            NodeDictionary[0] = new CozyDefaultTiled();
        }

        // 0保留为无方块
        public static CozyTiledNode GetInstance(uint id)
        {
            if (!NodeDictionary.ContainsKey(id))
            {
                lock(_objLock)
                {
                    if (!NodeDictionary.ContainsKey(id))
                    {
                        return NodeDictionary[0];
                    }
                }
            }

            return NodeDictionary[id];
        }

        // 使用Factory前必须先注册Tiled类型
        public static void RegisterTiled(uint id, CozyTiledNode node)
        {
            if(id != 0 && node != null)
            {
                lock(_objLock)
                {
                    NodeDictionary[id] = node;
                }
            }
        }

        public static IEnumerable<KeyValuePair<uint, CozyTiledNode>> GetTiles()
        {
            return NodeDictionary.ToList();
        }
    }
}
