using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyGreenTiled : CozyTiledNode,ITiledNodeBase
    {
        public uint Id { get { return CozyTiledId.GreenTiled; } }

        public CozyGreenTiled()
        {
            ColorProperty = Color.Green;
        }
    }
}
