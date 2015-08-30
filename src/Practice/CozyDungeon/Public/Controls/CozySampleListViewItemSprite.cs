using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace CozyDungeon.Public.Controls
{
    public class CozySampleListViewItemSprite : CozySampleListViewItem
    {
        public CozySampleListViewItemSprite(CCSprite sprite)
        {
            sprite.AnchorPoint = CCPoint.Zero;
            this.AddChild(sprite);
            this.ContentSize = sprite.ContentSize;
        }
    }
}
