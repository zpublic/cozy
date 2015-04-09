using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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

namespace CozyQuickGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CozyQuick.Engine.EngineImpl impl = null;
        public MainWindow()
        {
            InitializeComponent();
            impl = new CozyQuick.Engine.EngineImpl();
            impl.Init();

            var mousedown = Observable.FromEventPattern<MouseButtonEventArgs>(CozyObj, "MouseLeftButtonDown")
                .Select(x => x.EventArgs.GetPosition(CozyObj));
            var mouseup = Observable.FromEventPattern<MouseButtonEventArgs>(this, "MouseLeftButtonUp");
            var mousemove = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove")
                .Select(x => x.EventArgs.GetPosition(this));

            var q = from start in mousedown
                    from end in mousemove.TakeUntil(mouseup)
                    select new
                    {
                        X = end.X - start.X,
                        Y = end.Y - start.Y
                    };

            q.Subscribe(value =>
            {
                Canvas.SetLeft(CozyObj, value.X);
                Canvas.SetTop(CozyObj, value.Y);
            });
        }
    }
}
