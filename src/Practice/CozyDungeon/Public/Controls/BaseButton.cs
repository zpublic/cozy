using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace CozyDungeon.Public.Controls
{
    public class BaseButton : CozyControl
    {
        #region Text

        private CCLabel DisplayText { get; set; }

        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != null && value != text && value.Length > 0)
                {
                    text = value;
                    RefreshText();
                }
            }
        }

        private int fontSize = 12;
        public int FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
                RefreshText();
            }
        }

        private void RefreshText()
        {
            if (DisplayText != null)
            {
                this.RemoveChild(DisplayText);
            }
            DisplayText = new CCLabel(text, "微软雅黑", FontSize);
            DisplayText.Position = new CCPoint(ContentSize.Width / 2, ContentSize.Height / 2);
            this.AddChild(DisplayText, 2);
        }

        #endregion

        #region Constructors

        public BaseButton(float width, float height)
        {
            Init(width, height);
        }

        public BaseButton(float x, float y, float width, float height)
        {
            InitWithRect(x, y, width, height);
        }

        public virtual void Init(float width, float height)
        {
            EventListener = new CCEventListenerTouchOneByOne()
            {
                OnTouchBegan = OnTouchBegan,
                OnTouchEnded = OnTouchEnded,
            };
            this.AddChild(BorderNode);
            ContentSize = new CCSize(width, height);
        }

        public virtual void InitWithRect(float x, float y, float width, float height)
        {
            Init(width, height);

            Position    = new CCPoint(x, y);
        }

        #endregion

        #region Event

        public Action OnClick { get; set; }

        public ButtonStatus Status { get; set; }

        public CCEventListenerTouchOneByOne EventListener { get; set; }

        private bool OnTouchBegan(CCTouch touch, CCEvent e)
        {
            var rect = new CCRect(PositionX, PositionY, ContentSize.Width, ContentSize.Height);
            if (rect.ContainsPoint(touch.Location))
            {
                OnKeyDown();
                return true;
            }
            return false;
        }

        private void OnTouchEnded(CCTouch touch, CCEvent e)
        {
            OnKeyUp();
        }

        protected virtual void OnKeyDown()
        {
            Status = ButtonStatus.Pressed;
            OnClick();
        }

        protected virtual void OnKeyUp()
        {
            Status = ButtonStatus.Released;
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
