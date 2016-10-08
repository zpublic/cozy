using CozyArce.Tree.Base;
using CozyArce.Tree.Generator.Sample;
using CozyArce.Tree.Render.Sample;
using CozyArce.Tree.Generator.SensitivePlant;
using System.Windows;
using CozyArce.Tree.Generator.FlourishingTree;
using System.Diagnostics;
using CozyArce.Tree.Generator.Sample2;

namespace CozyArce.FractalTree
{
    public partial class MainWindow : Window
    {
        ITreeGenerator g;
        ITreeRender r;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            g = new LowGenerator();
            r = new LowRender(xCanvas);
            xCanvas.Children.Clear();
            r.Draw(g.Generate());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            g = new SensitivePlantGenerator();
            r = new LowRender(xCanvas);
            xCanvas.Children.Clear();
            r.Draw(g.Generate());
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            g = new FlourishingTreeGenerator();
            r = new LowRender(xCanvas);
            xCanvas.Children.Clear();
            r.Draw(g.Generate());
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            g = new SampleTree();
            r = new LowRender(xCanvas);
            xCanvas.Children.Clear();
            r.Draw(g.Generate());
        }
    }
}
