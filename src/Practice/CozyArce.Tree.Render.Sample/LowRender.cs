using CozyArce.Tree.Base;
using CozyArce.Tree.Base.Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CozyArce.Tree.Render.Sample
{
    public class LowRender : ITreeRender
    {
        public Canvas _canvas;
        public LowRender(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Draw(CozyTree tree)
        {
            foreach (var b in tree.Branchs)
            {
                LineGeometry geo = new LineGeometry();
                geo.StartPoint = b.begin;
                geo.EndPoint = b.end;
                Path myPath = new Path();
                myPath.Stroke = Brushes.Black;
                myPath.StrokeThickness = b.width;
                myPath.Data = geo;
                _canvas.Children.Add(myPath);
            }
            foreach (var b in tree.Leaves)
            {
                EllipseGeometry geo = new EllipseGeometry();
                geo.Center = b.begin;
                geo.RadiusX = 5;
                geo.RadiusY = 3;
                Path myPath = new Path();
                myPath.Stroke = Brushes.Green;
                myPath.StrokeThickness = 1;
                myPath.Fill = Brushes.Green;
                myPath.Data = geo;
                _canvas.Children.Add(myPath);
            }
            foreach (var b in tree.Flowers)
            {
                EllipseGeometry geo = new EllipseGeometry();
                geo.Center = b.pos;
                geo.RadiusX = 5;
                geo.RadiusY = 3;
                Path myPath = new Path();
                myPath.Stroke = Brushes.Pink;
                myPath.StrokeThickness = 1;
                myPath.Fill = Brushes.Pink;
                myPath.Data = geo;
                _canvas.Children.Add(myPath);
            }
        }
    }
}
