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

        public Panel()
        {
            children = new List<UIElement>();
        }

        public void AddChild(UIElement child)
        {
            if (!children.Contains(child))
            {
                children.Add(child);
                UpdateLayout();
            }
        }

        public void RemoveChild(UIElement child)
        {
            if(children.Remove(child))
            {
                UpdateLayout();
            }
        }

        public abstract void UpdateLayout();
    }
}
