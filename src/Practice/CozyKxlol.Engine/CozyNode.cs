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
        public Vector2 Position { get; set; }

        public Vector2 AnchorPoint { get; set; }

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
                    _GlobalPosition += iter.Position;
                }
                return _GlobalPosition;
            }
        }

        public CozyNode()
        {
            IsVisible = true;
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
            SortAllChildren();
            foreach(var child in Children)
            {
                child.Draw(gameTime, spriteBatch);
            }
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
    }
}
