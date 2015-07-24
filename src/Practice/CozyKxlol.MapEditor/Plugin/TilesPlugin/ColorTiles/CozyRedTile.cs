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
    public class CozyRedTile : CozyColorTile
    {
        public static uint TiledId { get { return CozyTileId.Red; } }
        public override uint Id
        {
            get 
            {
                return TiledId;
            }
        }

        public CozyRedTile()
        {
            ColorProperty = Color.Red;
        }
    }
}
