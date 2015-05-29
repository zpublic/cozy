using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyNode
    {
        private Vector2 _Position;
        public Vector2 Position 
        { 
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
                UpdateAnchorPointInPoint();
            }
        }

        public bool IsVisible { get; set; }

        public int ZOrder { get; set; }

        public int GlobalZOrder 
        {
            get
            {
                int zorder = ZOrder;
                for(CozyNode iter = this; iter != null; iter = iter.Parent)
                {
                    zorder += iter.ZOrder;
                }
                return zorder;
            }
        }

        public Vector2 GlobalPosition
        {
            get
            {
                var _GlobalPosition = Position;
                for (CozyNode iter = this.Parent; iter != null; iter = iter.Parent)
                {
                    _GlobalPosition += iter.OriginPoint;
                }
                return _GlobalPosition;
            }
        }

        public CozyNode()
        {
            IsVisible = true;
            IsTransformDirty = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach(var child in Children)
            {
                child.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!IsVisible) return;

            if (IsTransformDirty) UpdateTransform();

            SortAllChildren();
            int i = 0;
            for (; i < Children.Count; ++i)
            {
                if(Children[i].ZOrder < 0)
                {
                    Children[i].Draw(gameTime, spriteBatch);
                }
                else
                {
                    break;
                }
            }

            DrawSelf(gameTime, spriteBatch);

            for (; i < Children.Count; ++i)
            {
                Children[i].Draw(gameTime, spriteBatch);
            }
        }

        protected virtual void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        protected List<CozyNode> Children = new List<CozyNode>();
        public CozyNode Parent { get; set; }

        public bool IsChildDirty { get; set; }

        public void InsertChild(CozyNode child, int zorder = 0)
        {
            child.ZOrder = zorder;
            Children.Add(child);
            IsChildDirty = true;
        }

        public void DetachChild(CozyNode child)
        {
            child.Parent = null;
            Children.Remove(child);
        }

        public void AddChind(CozyNode child, int zorder = 0)
        {
            if (child == null) return;

            InsertChild(child, zorder);
            child.Parent = this;
        }

        public void RemoveChild(CozyNode child)
        {
            if (Children.Count <= 0) return;

            if(Children.Contains(child))
            {
                DetachChild(child);
            }
        }

        public void SortAllChildren()
        {
            if(IsChildDirty)
            {
                Children.Sort((a, b) =>
                {
                    if (a.ZOrder < b.ZOrder)
                    {
                        return -1;
                    }
                    else if (a.ZOrder > b.ZOrder)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                });
                IsChildDirty = false;
            }
        }

        private Vector2 _ContentSize;
        public Vector2 ContentSize
        {
            get
            {
                return _ContentSize;
            }
            set
            {
                _ContentSize = value;
                UpdateAnchorPointInPoint();
                IsTransformDirty = true;
            }
        }

        private Vector2 _AnchorPoint;
        public Vector2 AnchorPoint 
        { 
            get
            {
                return _AnchorPoint;
            }
            set
            {
                _AnchorPoint = value;
                UpdateAnchorPointInPoint();
                IsTransformDirty = true;
            }
        }

        public Vector2 AnchorPointInPoint { get; set; }

        public Vector2 OriginPoint 
        {
            get 
            {
                return Position - AnchorPointInPoint;
            } 
        }
        public void UpdateAnchorPointInPoint()
        {
            AnchorPointInPoint = new Vector2(AnchorPoint.X * ContentSize.X, AnchorPoint.Y * ContentSize.Y);
        }

        private Vector2 _Transform;
        public Vector2 Transform
        {
            get
            {
                return _Transform;
            }
            set
            {
                _Transform = value;
            }
        }

        public bool IsTransformDirty { get; set; }

        public void UpdateTransform()
        {
            Transform = -AnchorPointInPoint;

            IsTransformDirty = false;
        }

        public void RunAction(CozyAction action)
        {
            CozyDirector.Instance.ActionManagerInstance.AddAction(action, this);
        }

        public void StopAllActions()
        {
            CozyDirector.Instance.ActionManagerInstance.RemoveAllActionsWithTarget(this);
        }

        public void StopAction(CozyAction action)
        {
            CozyDirector.Instance.ActionManagerInstance.RemoveAction(action);
        }
    }
}
