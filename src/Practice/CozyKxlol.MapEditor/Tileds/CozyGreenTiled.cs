using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.MapEditor.Tileds
{
    public class CozyGreenTiled : CozyColorTiled
    {
        public static uint TiledId { get { return CozyTiledId.Green; } }

        public override uint Id
        {
            get 
            {
                return TiledId;
            }
        }

        public CozyGreenTiled()
        {
            ColorProperty = Color.Green;
        }
    }
}
