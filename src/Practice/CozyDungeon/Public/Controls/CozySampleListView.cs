using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyDungeon.Public.Controls.Events;
using CocosSharp;
using CozyDungeon.Public.Controls.Enum;

namespace CozyDungeon.Public.Controls
{
    public class CozySampleListView : CozyControl
    {
        public List<CozyControl> Items { get; set; } = new List<CozyControl>();

        public void AddItem(CozyControl control)
        {
            this.AddChild(control);
            Items.Add(control);
            RefreshItems();
            if (ItemAddEventHandler != null)
            {
                ItemAddEventHandler(this, new ItemAddEventArgs(control));
            }
        }

        public void RemoveItem(CozyControl control)
        {
            this.RemoveChild(control);
            Items.Remove(control);
            RefreshItems();
            if(ItemRemoveEventHandler != null)
            {
                ItemRemoveEventHandler(this, new ItemRemoveEventArgs(control));
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
            float y = ContentSize.Height;
            foreach (var item in Items)
            {
                if (y - item.MarginTop - item.ContentSize.Height <= 0)
                {
                    item.Visible = false;
                }
                else
                {
                    item.Visible = true;
                    y -= item.MarginTop;
                    item.PositionX = x;
                    item.PositionY = y - item.ContentSize.Height;
                    y -= item.ContentSize.Height;
                    y -= item.MarginBottom;
                }
            }
        }

        private void RefreshHorizontal()
        {
            float x = 0.0f;
            float y = 0.0f;
            foreach (var item in Items)
            {
                if (x + item.MarginBottom + item.ContentSize.Width > ContentSize.Width)
                {
                    item.Visible = false;
                }
                else
                {
                    item.Visible = true;
                    x += item.MarginLeft;
                    item.PositionX = x;
                    item.PositionY = y;
                    x += item.ContentSize.Width;
                    x += item.MarginRight;
                }
            }
        }

        public ControlOrientation Orientation { get; set; }

        public event EventHandler<ItemAddEventArgs> ItemAddEventHandler;

        public event EventHandler<ItemRemoveEventArgs> ItemRemoveEventHandler;
    }
}
