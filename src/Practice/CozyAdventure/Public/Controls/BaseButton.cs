using CocosSharp;
using CozyAdventure.Public.Controls.Enum;
using System;

namespace CozyAdventure.Public.Controls
{
    public abstract class BaseButton : CozyControl
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

        #region 缩放用

        private CCScaleTo m_scaleTo;
        private CCScaleTo m_scaleForm;
        private CCCallFuncN m_scaleFunc;

        #endregion 缩放用

        #endregion Text

        #region Constructors

        /// <summary>
        /// 初始化缩放
        /// </summary>
        /// <param name="_scaleForm"></param>
        /// <param name="_scaleTo"></param>
        /// <param name="_duration"></param>
        private void InitScale(float _scaleForm = 1.0f, float _scaleTo = 1.1f, float _duration = 0.1f)
        {
            m_scaleFunc = new CCCallFuncN(node => ((CozySampleButton)node).ScaleComplete());

            m_scaleTo = new CCScaleTo(_duration, _scaleTo);
            m_scaleForm = new CCScaleTo(_duration, _scaleForm);
        }

        public BaseButton(float width, float height)
        {
            InitScale();

            Init(width, height);
        }

        public BaseButton(float x, float y, float width, float height)
        {
            InitScale();

            InitWithRect(x, y, width, height);
        }

        public virtual void Init(float width, float height)
        {
            EventListener = new CCEventListenerTouchOneByOne()
            {
                OnTouchBegan = OnTouchBegan,
                OnTouchEnded = OnTouchEnded,
            };
            ContentSize = new CCSize(width, height);
        }

        public virtual void InitWithRect(float x, float y, float width, float height)
        {
            Init(width, height);

            Position = new CCPoint(x, y);
        }

        #endregion Constructors

        #region Event

        public Action OnClick { get; set; }

        public ButtonStatus Status { get; set; }

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

            OnKeyDown();
            e.StopPropogation();
            return true;
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
            // 缩放
            this.RunActions(m_scaleTo, m_scaleFunc);

            Status = ButtonStatus.Released;
        }

        /// <summary>
        /// 缩放结束后弹回
        /// </summary>
        private void ScaleComplete()
        {
            this.RunAction(m_scaleForm);
        }

        #endregion Event
    }
}