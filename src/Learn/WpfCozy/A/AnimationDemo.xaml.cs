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
using System.Windows.Shapes;
using WpfCozy.A.AnimationDemoDetails;

namespace WpfCozy.A
{
    /// <summary>
    /// Interaction logic for AnimationDemo.xaml
    /// </summary>
    public partial class AnimationDemo : Window
    {
        public AnimationDemo()
        {
            InitializeComponent();
        }

        private void OnEllipseAnimation(object sender, RoutedEventArgs e)
        {
            new EllipseWindow().Show();
        }

        private void OnButtonAnimation(object sender, RoutedEventArgs e)
        {
            new ButtonAnimationWindow().Show();
        }

        private void OnKeyframeAnimation(object sender, RoutedEventArgs e)
        {
            new KeyFrameWindow().Show();
        }

        private void OnEventTrigger(object sender, RoutedEventArgs e)
        {
            new EventTriggerWindow().Show();
        }
    }
}
