using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Public.Controls
{
    public class ButtonEventDispatcher
    {
        private List<BaseButton> ControlList { get; set; } = new List<BaseButton>();
        private CCEventListener Listener { get; set; }
        private CCNode Target { get; set; }

        public bool Enable { get; set; } = true;

        public ButtonEventDispatcher()
        {
            Listener = new CCEventListenerTouchOneByOne()
            {
                OnTouchBegan = OnTouchBegan,
                OnTouchEnded = OnTouchEnded,
            };
        }

        public void  AttachListener(CCNode node)
        {
            node.AddEventListener(Listener);
            Target = node;
        }

        public void DetachListener(CCNode node)
        {
            Target = null;
            node.RemoveEventListener(Listener);
        }

        public void Add(BaseButton control)
        {
            ControlList.Add(control);
        }

        public void Remove(BaseButton control)
        {
            ControlList.Remove(control);
        }

        public void Clear()
        {
            ControlList.Clear();
        }

        public bool OnTouchBegan(CCTouch touch, CCEvent e)
        {
            if(Enable)
            {
                foreach (var control in ControlList)
                {
                    var p = control.PositionWorldspace;
                    var rect = new CCRect(p.X, p.Y, control.ContentSize.Width, control.ContentSize.Height);
                    if (rect.ContainsPoint(Target.Layer.ScreenToWorldspace(touch.LocationOnScreen)))
                    {
                        return control.OnTouchBegan(touch, e);
                    }
                }
            }
            return false;
        }

        public void OnTouchEnded(CCTouch touch, CCEvent e)
        {
            if (Enable)
            {
                foreach (var control in ControlList)
                {
                    var p = control.PositionWorldspace;
                    var rect = new CCRect(p.X, p.Y, control.ContentSize.Width, control.ContentSize.Height);
                    if (rect.ContainsPoint(Target.Layer.ScreenToWorldspace(touch.LocationOnScreen)))
                    {
                        control.OnTouchEnded(touch, e);
                        return;
                    }
                }
            }
        }
    }
}
