using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled
{
    public static class CozyTiledFactory
    {
        private static Dictionary<uint, CozyTiledNode> NodeDictionary 
            = new Dictionary<uint,CozyTiledNode>();

        public static CozyTiledNode GetInstance(uint id)
        {
            CozyTiledNode node = null;

            if (NodeDictionary.ContainsKey(id))
            {
                node = NodeDictionary[id];
            }
            else
            {
                switch(id)
                {
                    case CozyTiledId.NoneTiled:
                        node                = new CozyNoneTiled();
                        NodeDictionary[id]  = node;
                        break;
                    case CozyTiledId.RedTiled:
                        node                = new CozyRedTiled();
                        NodeDictionary[id]  = node;
                        break;
                    case CozyTiledId.GreenTiled:
                        node                = new CozyGreenTiled();
                        NodeDictionary[id]  = node;
                        break;
                    default:
                        break;
                }
            }

            return node;
        }
    }
}
