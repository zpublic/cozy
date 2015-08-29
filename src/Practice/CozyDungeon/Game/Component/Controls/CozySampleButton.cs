using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace CozyDungeon.Game.Component.Controls
{
    public class CozySampleButton : BaseButton
    {
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
                    normalSprite.Position = ContentSize.Center;
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
                    clickedSprite.Position = ContentSize.Center;
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
        }

        public CozySampleButton(float x, float y, float width, float height)
            :base(x, y, width, height)
        {
        }

        public CozySampleButton(CCSprite normal, CCSprite clicked)
        {
            InitWithSprite(normal, clicked);
        }

        public void InitWithSprite(CCSprite normal, CCSprite clicked)
        {
            NormalSprite    = normal;
            ClickedSprite   = clicked;
        }

        #endregion

        #region Event

        protected override void OnKeyDown()
        {
            base.OnKeyDown();
            RefreshDisplaySprite();
        }

        protected override void OnKeyUp()
        {
            base.OnKeyUp();
            RefreshDisplaySprite();
        }

        #endregion
    }
}
