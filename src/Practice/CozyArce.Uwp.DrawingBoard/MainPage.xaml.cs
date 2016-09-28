using CozyArce.Tree.Generators;
using CozyArce.Tree.Renders;
using CozyArce.Tree.Shared;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CozyArce.Uwp.DrawingBoard
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        ITreeGenerator g;
        ITreeRender r;

        void canvas_Draw(
            CanvasControl sender,
            CanvasDrawEventArgs args)
        {
            var r = new LowRender(sender, args);
            var tree = g?.Generate();
            if (tree != null)
                r?.Draw(tree);
        }

        void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            canvas.RemoveFromVisualTree();
            canvas = null;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            g = new LowGenerator();
            canvas.Invalidate();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            g = new FlourishingTreeGenerator();
            canvas.Invalidate();
        }
    }
}
