using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace CozyAdventure.View.Sprite
{
    public class BorderNode : CCNode
    {
        #region Constructors

        public BorderNode()
        {
            this.AddChild(Border);
        }

        #endregion

        #region Border 

        private CCDrawNode Border { get; set; } = new CCDrawNode();

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

        public override CCSize ContentSize
        {
            get
            {
                return base.ContentSize;
            }

            set
            {
                base.ContentSize = value;
                RefreshBorder();
            }
        }

        private void RefreshBorder()
        {
            if (HasBorder)
            {
                Border.Cleanup();
                Border.DrawRect(
                    new CCRect(0, 0, ContentSize.Width, ContentSize.Height),
                    new CCColor4B(0, 0, 0),
                    BorderSize,
                    new CCColor4B(255, 255, 255));
            }
        }

        #endregion
    }
}
