using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyRedTiled : CozyTiledNode, ITiledNodeBase
    {
        public uint Id { get { return CozyTiledId.RedTiled; } }

        public Color Color { get; set; }

        public CozyRedTiled()
        {
            ColorProperty = Color.Red;
        }
    }
}
