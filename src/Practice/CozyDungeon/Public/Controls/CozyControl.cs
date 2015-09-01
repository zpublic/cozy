using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace CozyDungeon.Public.Controls
{
    public class CozyControl : CCNode
    {
        #region Display

        public float MarginLeft { get; set; }

        public float MarginRight { get; set; }

        public float MarginTop { get; set; }

        public float MarginBottom { get; set; }

        #endregion

        #region Constructors

        public CozyControl()
        {
            this.AddChild(BorderNode);
        }

        #endregion

        #region Border 

        private CCDrawNode BorderNode { get; set; } = new CCDrawNode();

        private int borderSize = 1;
        private int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                RefreshBorder();
            }
        }

        private bool hasBorder;
        public bool HasBorder
        {
            get
            {
                return hasBorder;
            }
            set
            {
                if (hasBorder != value)
                {
                    hasBorder = value;
                    RefreshBorder();
                }
            }
        }

        private void RefreshBorder()
        {
            if (HasBorder)
            {
                BorderNode.Cleanup();
                BorderNode.DrawRect(
                    new CCRect(0, 0, ContentSize.Width, ContentSize.Height),
                    new CCColor4B(0, 0, 0),
                    BorderSize,
                    new CCColor4B(255, 255, 255));
            }
        }

        #endregion
    }
}
