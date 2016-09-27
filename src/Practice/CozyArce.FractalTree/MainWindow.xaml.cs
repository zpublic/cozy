using CozyArce.Tree.Base;
using CozyArce.Tree.Generator.Sample;
using CozyArce.Tree.Render.Sample;
using System.Windows;

namespace CozyArce.FractalTree
{
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
