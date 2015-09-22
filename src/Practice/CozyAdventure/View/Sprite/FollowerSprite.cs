using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Model;

namespace CozyAdventure.View.Sprite
{
    public class FollowerSprite : BorderNode
    {
        private CCSprite FollowerAvatorNode { get; set; }

        private CCLabel FollowerNameNode { get; set; }

        private CCLabel FollowerLevelNode { get; set; }

        private CCLabel FollowerStarNode { get; set; }

        public Follower BindFollower { get; set; }

        public int CurrStar
        {
            get
            {
                return BindFollower == null ? 0 : BindFollower.CurStar;
            }
            set
            {
                if(BindFollower != null)
                {
                    BindFollower.CurStar = value;
                    SetFollowerStar(value, BindFollower.MaxStar);
                }
            }
        }
        public int CurrLevel
        {
            get
            {
                return BindFollower == null ? 0 : BindFollower.CurLevel;
            }
            set
            {
                if (BindFollower != null)
                {
                    BindFollower.CurLevel = value;
                    SetFollowerLevel(value);
                }
            }
        }
        public string CurrName
        {
            get
            {
                return BindFollower == null ? string.Empty : BindFollower.Name;
            }
            set
            {
                if (BindFollower != null)
                {
                    BindFollower.Name = value;
                    SetFollowerName(value);
                }
            }
        }

        public int RestStar
        {
            get
            {
                return BindFollower == null ? 0 : BindFollower.MaxStar - BindFollower.CurStar;
            }
        }

        public FollowerSprite(Follower follower, bool CreateAvator = false)
        {
            if(CreateAvator)
            {
                var sprite = new CCSprite(@"face/" + follower.Avatar);
                InitWithSprite(follower, sprite);
            }
            else
            {
                Init(follower);
            }
        }

        public FollowerSprite(Follower follower, CCSprite sprite)
        {
            InitWithSprite(follower, sprite);
        }

        private void Init(Follower follower)
        {
            HasBorder       = true;
            BindFollower    = follower;

            FollowerNameNode    = new CCLabel("", "Consolas", 10);
            FollowerLevelNode   = new CCLabel("", "Consolas", 10);
            FollowerStarNode    = new CCLabel("", "Consolas", 10);

            FollowerLevelNode.AnchorPoint   = CCPoint.Zero;
            FollowerStarNode.AnchorPoint    = CCPoint.Zero;

            SetFollowerName(CurrName);
            SetFollowerLevel(CurrLevel);
            SetFollowerStar(CurrStar, CurrStar + RestStar);

            this.AddChild(FollowerNameNode);
            this.AddChild(FollowerLevelNode);
            this.AddChild(FollowerStarNode);

            RefreshContentSize();
            RefreshNodePos();
        }

        private void InitWithSprite(Follower follower, CCSprite avator)
        {
            Init(follower);

            if (avator != null)
            {
                avator.AnchorPoint = CCPoint.Zero;
                FollowerAvatorNode = avator;
                FollowerAvatorNode.Position = new CCPoint(2, 2);
                this.AddChild(FollowerAvatorNode);
            }
        }

        private void RefreshNodePos()
        {
            FollowerNameNode.Position   = new CCPoint(100 + (ContentSize.Width - 100) / 2, 80);
            FollowerLevelNode.Position  = new CCPoint(110, 40);
            FollowerStarNode.Position   = new CCPoint(110, 10);
        }

        private void SetFollowerName(string name)
        {
            FollowerNameNode.Text = name;
        }

        private void SetFollowerLevel(int level)
        {
            FollowerLevelNode.Text = string.Format("lv : {0}/30", level);
        }

        private void SetFollowerStar(int star, int maxStar)
        {
            FollowerStarNode.Text = string.Format("star : {0}/{1}", star, maxStar);
        }

        private void RefreshContentSize()
        {
            float size = 0;

            size = Math.Max(FollowerNameNode.ContentSize.Width, size);
            size = Math.Max(FollowerLevelNode.ContentSize.Width, size);
            size = Math.Max(FollowerStarNode.ContentSize.Width, size);

            ContentSize = new CCSize(120 + size, 100);
        }
    }
}
