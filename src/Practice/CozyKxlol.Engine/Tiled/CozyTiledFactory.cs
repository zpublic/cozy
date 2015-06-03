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

        public delegate CozyTiledNode CreateFunc(uint id);

        public static CreateFunc Create;

        // 必须先设置Create委托
        public static CozyTiledNode GetInstance(uint id)
        {
            if (!NodeDictionary.ContainsKey(id))
            {
                lock(_objLock)
                {
                    if(!NodeDictionary.ContainsKey(id))
                    {
                        if (Create != null)
                            NodeDictionary[id] = Create(id);
                        else
                            NodeDictionary[id] = new CozyTiledNode();
                    }
                }
            }

            return NodeDictionary[id];
        }
    }
}
