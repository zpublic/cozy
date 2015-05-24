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


        public virtual void Update(GameTime gameTime)
        {

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

        public void InsertChild(CozyNode child)
        {
            Children.Add(child);
            IsChildDirty = true;
        }

        public void DetachChild(CozyNode child)
        {
            child.Parent = null;
            Children.Remove(child);
        }

        public void AddChind(CozyNode child)
        {
            if (child == null) return;

            InsertChild(child);
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
