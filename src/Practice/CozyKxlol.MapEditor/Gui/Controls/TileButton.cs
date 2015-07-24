using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Starbound.UI.XNA.Resources;

namespace CozyKxlol.MapEditor.Gui.Controls
{
    public class TileButton : Starbound.UI.Controls.TextureButton
    {
        public TileButton(CozyTexture texture, Rectangle SourceRect)
        {
            PreferredHeight     = 32;
            PreferredWidth      = 32;
            Margin              = new Starbound.UI.Thickness(3, 3, 0, 0);
            Content             = new XNAImageResource(texture.Get());
            SourcePosition      = new Starbound.UI.Vector2(SourceRect.X, SourceRect.Y);
            SourceSize          = new Starbound.UI.Vector2(SourceRect.Width, SourceRect.Height);
        }
    }
}
