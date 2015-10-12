using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using CozyAdventure.Public.Controls.Enum;

namespace CozyAdventure.Public.Controls
{
    public class CozySampleEditBox : CozyControl
    {
        private CCTextField TiledInputNode { get; set; }

        public CozySampleEditBox(string text, string font, int fontsize)
        {
            TiledInputNode = new CCTextField(text, font, fontsize)
            {
                AnchorPoint = CCPoint.Zero,
            };
            this.AddChild(TiledInputNode);

            EventListener = new CCEventListenerTouchOneByOne()
            {
                OnTouchBegan = OnTouchBegan,
            };
        }

        public override CCSize ContentSize
        {
            get
            {
                return TiledInputNode.ContentSize;
            }

            set
            {
                TiledInputNode.ContentSize = value;
            }
        }

        public CCEventListenerTouchOneByOne EventListener { get; set; }

        private bool OnTouchBegan(CCTouch touch, CCEvent e)
        {
            if (!Visible)
            {
                return false;
            }

            var rect = new CCRect(PositionWorldspace.X, PositionWorldspace.Y, ContentSize.Width, ContentSize.Height);
            if (!rect.ContainsPoint(touch.Location))
            {
                return false;
            }

            TiledInputNode.Edit();
            e.StopPropogation();
            return true;
        }
    }
}
