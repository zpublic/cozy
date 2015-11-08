using System.Drawing;
using CozyPixel.Interface;

namespace CozyPixel.Tool
{
    public abstract class PixelToolBase
    {
        public Color ColorHolder { get; set; }

        public abstract bool WillModify { get; }

        protected IPixelGridDrawable Target { get; set; }

        public PixelToolBase()
        {

        }

        public PixelToolBase(Color holder)
        {
            ColorHolder = holder;
        }

        public void Begin(IPixelGridDrawable paint, Point p)
        {
            OnEnter(paint);
            OnBegin(p);
        }

        public void Move(Point p)
        {
            OnMove(p);
        }

        public bool End(Point p)
        {
            var retVal = OnEnd(p);
            OnExit();
            return retVal;
        }

        protected virtual void OnEnter(IPixelGridDrawable paint)
        {
            Target = paint;
        }

        protected virtual void OnBegin(Point p)
        {
            // Nothing Todo
        }

        protected virtual void OnMove(Point p)
        {
            // Nothing Todo
        }

        protected virtual bool OnEnd(Point p)
        {
            return false;
        }

        protected virtual void OnExit()
        {
            Target = null;
        }
    }
}
