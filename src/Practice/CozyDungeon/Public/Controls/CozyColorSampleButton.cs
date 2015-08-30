using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using CozyDungeon.Public.Controls.Enum;

namespace CozyDungeon.Public.Controls
{
    public class CozyColorSampleButton : BaseButton
    {
        #region Display

        private CCDrawNode BackgroundNode { get; set; } = new CCDrawNode();

        private CCColor4B normalColor;
        public CCColor4B NormalColor
        {
            get
            {
                return normalColor;
            }
            set
            {
                normalColor = value;
                RefreshColor();
            }
        }

        private CCColor4B clickedColor;
        public CCColor4B ClickedColor
        {
            get
            {
                return clickedColor;
            }
            set
            {
                clickedColor = value;
                RefreshColor();
            }
        }

        private void RefreshColor()
        {
            BackgroundNode.Cleanup();

            CCColor4B CurrColor = new CCColor4B();
            if (Status == ButtonStatus.Pressed)
            {
                CurrColor = ClickedColor;
            }
            else
            {
                CurrColor = NormalColor;
            }

            BackgroundNode.DrawRect(new CCRect(0, 0, ContentSize.Width, ContentSize.Height), CurrColor);
        }

        #endregion

        #region Constructors

        public CozyColorSampleButton(float width, float height)
            :base(width, height)
        {
        }

        public CozyColorSampleButton(float x, float y, float width, float height)
            : base(x, y, width, height)
        {
        }

        public CozyColorSampleButton(float width, float height, CCColor4B normal, CCColor4B clicked)
            :base(width, height)
        {
            NormalColor = normal;
            ClickedColor = clicked;
        }

        public override void Init(float width, float height)
        {
            base.Init(width, height);
            this.AddChild(BackgroundNode);
        }

        #endregion

        #region Event

        protected override void OnKeyDown()
        {
            base.OnKeyDown();
            RefreshColor();
        }

        protected override void OnKeyUp()
        {
            base.OnKeyUp();
            RefreshColor();
        }

        #endregion
    }
}
