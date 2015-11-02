using CozyPixel.Tools;
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
        public IEnumerable<Point> Points { get; set; }

        public IPixelDrawable Target { get; set; }

        public Color Color { get; set; }

        private List<Tuple<Point, Color>> History { get; set; } = new List<Tuple<Point, Color>>();

        public void Do()
        {
            History.Clear();
            foreach (var p in Points)
            {
                History.Add(Tuple.Create(p, Target.ReadPixel(p)));
                Target.DrawPixel(p, Color);
            }
        }

        public void Undo()
        {
            foreach(var obj in History)
            {
                Target.DrawPixel(obj.Item1, obj.Item2);
            }
        }
    }
}
