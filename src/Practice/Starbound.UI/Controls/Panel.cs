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

        private Dictionary<UIElement, Action> clickActions = new Dictionary<UIElement, Action>();


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

        public void DispatchClick(Int32 x, Int32 y)
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
    }
}
