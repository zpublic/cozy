using CozyArce.Tree.Generators;
using CozyArce.Tree.Renders;
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
        bool _NeedDraw = false;

        void canvas_Draw(
            CanvasControl sender,
            CanvasDrawEventArgs args)
        {
            _NeedDraw = false;
            var g = new LowGenerator();
            var r = new LowRender(sender, args);
            r.Draw(g.Generate());
        }

        void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            canvas.RemoveFromVisualTree();
            canvas = null;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            canvas.Invalidate();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            canvas.Invalidate();
        }
    }
}
