using CozyArce.Tree.Base;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CozyArce.FractalTree
{
    class LowGenerator : ITreeGenerator
    {
        const double PI = 3.1415926;
        const double Arg = PI / 5;
        const double GoldenSection = 0.618;
        Random rand = new Random();
        CozyTree tree;

        public CozyTree Generate()
        {
            tree = new CozyTree();
            DrawTree(new Point(500, 850), PI / 2, 200, 30);
            return tree;
        }
        public void DrawTree(Point begin, double angle, double length, short width)
        {
            if (length < 3) return;

            int x = (int)(begin.X + length * Math.Cos(angle));
            int y = (int)(begin.Y - length * Math.Sin(angle));
            var end = new Point(x, y);

            tree.Branchs.Add(new CozyBranch()
            {
                begin = begin,
                end = end,
                width = width,
            });

            width -= (short)rand.Next(5, 8);
            if (width < 1) width = 1;
            var sub = rand.Next(2, 4);
            for (var i = 0; i < sub; ++i)
            {
                var len = rand.NextDouble();
                if (len < 0.4) len += 0.4;
                if (len > 0.8) len -= 0.2;
                DrawTree(end, angle + (rand.NextDouble() - 0.5) * PI / 2, len * length, width);
            }
        }
    }

    class LowRender : ITreeRender
    {
        public Canvas _canvas;
        public LowRender(Canvas canvas)
        {
            _canvas = canvas;
        }
        public void Draw(CozyTree tree)
        {
            foreach(var b in tree.Branchs)
            {
                LineGeometry myLineGeometry = new LineGeometry();
                myLineGeometry.StartPoint = b.begin;
                myLineGeometry.EndPoint = b.end;
                Path myPath = new Path();
                myPath.Stroke = Brushes.Black;
                myPath.StrokeThickness = b.width;
                myPath.Data = myLineGeometry;
                _canvas.Children.Add(myPath);
            }
        }
    }

    public partial class MainWindow : Window
    {
        ITreeGenerator g;
        ITreeRender r;

        public MainWindow()
        {
            InitializeComponent();
            g = new LowGenerator();
            r = new LowRender(xCanvas);
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            xCanvas.Children.Clear();
            r.Draw(g.Generate());
        }
    }
}
