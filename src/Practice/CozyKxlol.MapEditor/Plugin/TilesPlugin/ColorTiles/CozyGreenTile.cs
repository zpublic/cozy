using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.MapEditor.TilesPlugin.ColorTiles
{
    public class CozyGreenTile : CozyColorTile
    {
        public static uint TiledId { get { return CozyTileId.Green; } }

        public override uint Id
        {
            get 
            {
                return TiledId;
            }
        }

        public CozyGreenTile()
        {
            ColorProperty = Color.Green;
        }
    }
}
