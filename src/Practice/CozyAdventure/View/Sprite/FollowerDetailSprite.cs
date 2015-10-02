using CocosSharp;
using CozyAdventure.Game.Logic;
using CozyAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Public.Controls;

namespace CozyAdventure.View.Sprite
{
    public class FollowerDetailSprite : BorderNode
    {
        private CCSprite CurrAvatar { get; set; }

        private CCLabel CurrName { get; set; }

        private CCLabel CurrDesc { get; set; }

        private CCLabel CurrStar { get; set; }

        private CCLabel CurrLevel { get; set; }

        private CCLabel CurrAttack { get; set; }

        private CozySampleButton CloseButton { get; set; }

        private CozySampleButton StatusChangeButton { get; set; }

        public Action<object> FightStatusChangeCallback { get; set; }

        public FollowerDetailSprite()
        {
            ContentSize = new CCSize(435, 280);
            HasBorder   = true;

            CurrName    = new CCLabel("", "Consolas", 14);
            CurrDesc    = new CCLabel("", "Consolas", 14);
            CurrStar    = new CCLabel("", "Consolas", 14);
            CurrLevel   = new CCLabel("", "Consolas", 14);
            CurrAttack  = new CCLabel("", "Consolas", 14);
            CloseButton = new CozySampleButton(20, 20)
            {
                Text        = "X",
                FontSize    = 20,
                HasBorder   = true,
                OnClick     = () =>
                {
                    this.Visible = false;
                }
            };
            StatusChangeButton = new CozySampleButton(40, 20)
            {
                Text        = "",
                HasBorder   = true,
                OnClick     = () =>
                {
                    if(FightStatusChangeCallback != null)
                    {
                        FightStatusChangeCallback(currFollower);
                    }
                    RefreshInfo();
                }
            };

            this.AddChild(CurrName);
            this.AddChild(CurrDesc);
            this.AddChild(CurrStar);
            this.AddChild(CurrLevel);
            this.AddChild(CurrAttack);
            this.AddChild(CloseButton);
            this.AddChild(StatusChangeButton);
            this.AddEventListener(CloseButton.EventListener, 1);
            this.AddEventListener(StatusChangeButton.EventListener);
        }

        private Follower currFollower;
        public Follower CurrFollower
        {
            get
            {
                return currFollower;
            }
            set
            {
                if (currFollower != value)
                {
                    Clear();
                }
                currFollower = value;
                RefreshInfo();
            }
        }

        private void Clear()
        {
            this.RemoveChild(CurrAvatar);
        }

        private void RefreshInfo()
        {
            if(CurrFollower != null)
            {
                CurrAvatar = new CCSprite(@"face/" + CurrFollower.Avatar);
                this.AddChild(CurrAvatar);

                CurrDesc.Dimensions = new CCSize(250, 100);
                CurrDesc.Text       = CurrFollower.Desc;
                CurrName.Text       = CurrFollower.Name + "王者";
                CurrStar.Text       = string.Format("星级 : {0} / {1}", CurrFollower.CurStar, CurrFollower.MaxStar);
                CurrLevel.Text      = string.Format("等级 : {0} / 30", CurrFollower.CurLevel);
                CurrAttack.Text     = string.Format("战斗力 : {0}", FollowerLogic.GetAttack(CurrFollower));

                StatusChangeButton.Text = currFollower.IsFighting ? "Rest" : "Fight";
                RefreshPos();
            }
        }

        private void RefreshPos()
        {

            if(CurrAvatar != null)
            {
                CurrAvatar.AnchorPoint  = CCPoint.Zero;
                CurrAvatar.Position     = new CCPoint(50, 125);
            }
            CurrName.AnchorPoint    = CCPoint.Zero;
            CurrName.Position       = new CCPoint(50, 200);

            CurrDesc.AnchorPoint    = CCPoint.Zero;
            CurrDesc.Position       = new CCPoint(200, 150);

            CurrStar.AnchorPoint    = CCPoint.Zero;
            CurrStar.Position       = new CCPoint(200, 125);

            CurrLevel.AnchorPoint   = CCPoint.Zero;
            CurrLevel.Position      = new CCPoint(200, 75);

            CurrAttack.AnchorPoint  = CCPoint.Zero;
            CurrAttack.Position     = new CCPoint(200, 25);

            CloseButton.AnchorPoint = CCPoint.Zero;
            CloseButton.Position    = new CCPoint(400, 250);

            StatusChangeButton.AnchorPoint = CCPoint.Zero;
            StatusChangeButton.Position = new CCPoint(50, 25);
        }
    }
}
