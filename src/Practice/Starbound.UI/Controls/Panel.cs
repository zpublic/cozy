using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public abstract class Panel : UIElement
    {
        private List<UIElement> children;

        public ReadOnlyCollection<UIElement> Children { get { return children.AsReadOnly(); } }

        protected Dictionary<UIElement, Action> clickActions = new Dictionary<UIElement, Action>();
        public bool IsFocus { get; set; }

        public Panel()
        {
            children = new List<UIElement>();
        }

        public void AddChild(UIElement child, Action action = null)
        {
            if (!children.Contains(child))
            {
                children.Add(child);
                clickActions.Add(child, action);
                UpdateLayout();
            }
        }

        public void RemoveChild(UIElement child)
        {
            if (children.Remove(child))
            {
                clickActions.Remove(child);
                UpdateLayout();
            }
        }

        private bool IsContainPoint(int x, int y)
        {
            return (x > X && x < X + ActualWidth && y > Y && y < Y + ActualHeight);
        }

        protected virtual void DispatchClick(Int32 x, Int32 y)
        {
            foreach (var obj in children)
            {
                if (x > obj.X && x < obj.X + obj.ActualWidth &&
                    y > obj.Y && y < obj.Y + obj.ActualHeight)
                {
                    var click = clickActions[obj];
                    if (click != null)
                    {
                        click();
                    }
                }
            }
        }

        public abstract void UpdateLayout();

        public virtual bool OnMousePressed(int x, int y)
        {
            IsFocus = IsContainPoint(x, y);
            return IsFocus;
        }
        public virtual bool OnMouseMoved(int x, int y)
        {
            return false;
        }
        public virtual bool OnMouseReleased(int x, int y)
        {
            bool result = IsFocus;
            if(IsFocus)
            {
                DispatchClick(x, y);
                IsFocus = false;
            }
            return result;
        }
    }
}
