using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyDungeon.Public.Controls.Events;
using CocosSharp;

namespace CozyDungeon.Public.Controls
{
    public class CozySampleListView : CozyControl
    {
        public List<CCNode> Items { get; set; } = new List<CCNode>();

        public void AddItem(CCNode control)
        {
            control.AnchorPoint = CCPoint.Zero;
            this.AddChild(control);
            Items.Add(control);
            RefreshItems();
            if (ItemAddEventHandler != null)
            {
                ItemAddEventHandler(this, new ItemAddEventArgs(control));
            }
        }

        private void RefreshItems()
        {
            if (Orientation == ControlOrientation.Horizontal)
            {
                RefreshHorizontal();
            }
            else
            {
                RefreshVertical();
            }
        }

        private void RefreshVertical()
        {
            float x = 0.0f;
            float y = 0.0f;
            foreach (var item in Items)
            {
                if (y - PositionY <= ContentSize.Height)
                {
                    item.PositionX = x;
                    item.PositionY = y;
                    y += item.ContentSize.Height;
                }
            }
        }

        private void RefreshHorizontal()
        {
            float x = 0.0f;
            float y = 0.0f;
            foreach (var item in Items)
            {
                if (x - PositionX > ContentSize.Width)
                {
                    break;
                }
                item.PositionX = x;
                item.PositionY = y;
                x += item.ContentSize.Width;

            }
        }

        public ControlOrientation Orientation { get; set; }

        public event EventHandler<ItemAddEventArgs> ItemAddEventHandler;
    }
}
