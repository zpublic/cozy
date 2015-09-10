using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace CozyAdventure.Public.Controls
{
    public class CozySampleListViewItemNode : CozySampleListViewItem
    {
        public CozySampleListViewItemNode(CCNode node)
        {
            node.AnchorPoint = CCPoint.Zero;
            this.AddChild(node);
            this.ContentSize = node.ContentSize;
        }
    }
}
