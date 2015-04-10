using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCozy.A;
using WpfCozy.B.Books;

namespace WpfCozy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnBtnShapesDemo(object sender, RoutedEventArgs e)
        {
            ShapesDemo w = new ShapesDemo();
            w.ShowDialog();
        }

        private void OnBtnGeometryDemo(object sender, RoutedEventArgs e)
        {
            GeometryDemo w = new GeometryDemo();
            w.ShowDialog();
        }

        private void OnBtnTransformationDemo(object sender, RoutedEventArgs e)
        {
            TransformationDemo w = new TransformationDemo();
            w.ShowDialog();
        }

        private void OnBtnBrushesDemo(object sender, RoutedEventArgs e)
        {
            BrushesDemo w = new BrushesDemo();
            w.ShowDialog();
        }

        private void OnBtnFrameDemo(object sender, RoutedEventArgs e)
        {
            FrameDemo w = new FrameDemo();
            w.ShowDialog();
        }

        private void OnBtnExpanderDemo(object sender, RoutedEventArgs e)
        {
            ExpanderDemo w = new ExpanderDemo();
            w.ShowDialog();
        }

        private void OnBtnDecorationsDemo(object sender, RoutedEventArgs e)
        {
            DecorationsDemo w = new DecorationsDemo();
            w.ShowDialog();
        }

        private void OnBtnLayoutDemo(object sender, RoutedEventArgs e)
        {
            LayoutDemo w = new LayoutDemo();
            w.ShowDialog();
        }

        private void OnBtnStylesAndResources(object sender, RoutedEventArgs e)
        {
            StylesAndResources w = new StylesAndResources();
            w.ShowDialog();
        }

        private void OnBtnTriggerDemo(object sender, RoutedEventArgs e)
        {
            TriggerDemo w = new TriggerDemo();
            w.ShowDialog();
        }

        private void OnBtnTemplateDemo(object sender, RoutedEventArgs e)
        {
            TemplateDemo w = new TemplateDemo();
            w.ShowDialog();
        }

        private void OnBtnAnimationDemo(object sender, RoutedEventArgs e)
        {
            AnimationDemo w = new AnimationDemo();
            w.ShowDialog();
        }

        private void OnBtnVisualStateDemo(object sender, RoutedEventArgs e)
        {
            VisualStateDemo w = new VisualStateDemo();
            w.ShowDialog();
        }

        private void OnBtn3DDemo(object sender, RoutedEventArgs e)
        {
            _3DDemo w = new _3DDemo();
            w.ShowDialog();
        }

        private void OnBooksDemo(object sender, RoutedEventArgs e)
        {
            BooksDemo w = new BooksDemo();
            w.ShowDialog();
        }
    }
}
