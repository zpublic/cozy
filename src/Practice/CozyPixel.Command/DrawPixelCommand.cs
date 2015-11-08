using CozyPixel.Draw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Command
{
    public class DrawPixelCommand : IPixelCommand
    {
        public IEnumerable<KeyValuePair<Point, Color>> Points { get; set; }

        public IPixelDrawable Target { get; set; }

        private List<Tuple<Point, Color>> History { get; set; } = new List<Tuple<Point, Color>>();

        public void Do()
        {
            if (Target != null && Target.IsReady)
            {
                History.Clear();
                foreach (var p in Points)
                {
                    History.Add(Tuple.Create(p.Key, Target.ReadPixel(p.Key)));
                    Target.DrawPixel(p.Key, p.Value);
                }
            }
        }

        public void Undo()
        {
            if(Target != null && Target.IsReady)
            {
                foreach (var obj in History)
                {
                    Target.DrawPixel(obj.Item1, obj.Item2);
                }
            }
        }
    }
}
