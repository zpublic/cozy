using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace CozyDungeon.Game.Component.Controls
{
    public class CozySampleButton : CCNode
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

        #region Display

        private CCSprite normalSprite;
        public CCSprite NormalSprite
        {
            get
            {
                return normalSprite;
            }
            set
            {
                if (value != null && value != normalSprite)
                {
                    if (normalSprite != null)
                    {
                        this.RemoveChild(normalSprite);
                    }

                    normalSprite = value;
                    normalSprite.Position = new CCPoint(ContentSize.Width / 2, ContentSize.Height / 2);
                    this.AddChild(normalSprite, 1);
                    RefreshDisplaySprite();
                }
            }
        }

        private CCSprite clickedSprite;
        public CCSprite ClickedSprite
        {
            get
            {
                return clickedSprite;
            }
            set
            {
                if (value != null && value != clickedSprite)
                {
                    if (normalSprite != null)
                    {
                        this.RemoveChild(clickedSprite);
                    }
                    clickedSprite = value;
                    clickedSprite.Position = new CCPoint(ContentSize.Width / 2, ContentSize.Height / 2);
                    this.AddChild(clickedSprite, 1);
                    RefreshDisplaySprite();
                }
            }
        }

        private void RefreshDisplaySprite()
        {
            if (Status == ButtonStatus.Pressed)
            {
                if (ClickedSprite != null)
                {
                    ClickedSprite.Visible = true;
                }
                if (NormalSprite != null)
                {
                    NormalSprite.Visible = false;
                }
            }
            else
            {
                if (ClickedSprite != null)
                {
                    ClickedSprite.Visible = false;
                }
                if (NormalSprite != null)
                {
                    NormalSprite.Visible = true;
                }
            }
        }

        #endregion

        #region Constructors

        public CozySampleButton()
        {
            Init();
        }

        public CozySampleButton(float x, float y, float width, float height)
        {
            InitWithRect(x, y, width, height);
        }

        public CozySampleButton(CCSprite normal, CCSprite clicked)
        {
            InitWithSprite(normal, clicked);
        }

        public void Init()
        {
            Status = ButtonStatus.Released;

            EventListener = new CCEventListenerTouchOneByOne()
            {
                OnTouchBegan = OnTouchBegan,
                OnTouchEnded = OnTouchEnded,
            };
            this.AddChild(BorderNode);
        }

        public void InitWithRect(float x, float y, float width, float height)
        {
            Init();

            Position = new CCPoint(x, y);
            ContentSize = new CCSize(width, height);
        }

        public void InitWithSprite(CCSprite normal, CCSprite clicked)
        {
            NormalSprite = normal;
            ClickedSprite = clicked;
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

        private void OnKeyDown()
        {
            Status = ButtonStatus.Pressed;
            RefreshDisplaySprite();
            OnClick();
        }

        private void OnKeyUp()
        {
            Status = ButtonStatus.Released;
            RefreshDisplaySprite();
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
