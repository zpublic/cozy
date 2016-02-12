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

namespace CozyLauncher.Controls
{
    /// <summary>
    /// HelpControl.xaml 的交互逻辑
    /// </summary>
    public partial class HelpControl : UserControl
    {
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(int), typeof(HelpControl), new PropertyMetadata(new PropertyChangedCallback(OnImageWidthPropertyChanged)));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HelpControl), new PropertyMetadata(new PropertyChangedCallback(OnTextPropertyChanged)));

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(BitmapImage), typeof(HelpControl), new PropertyMetadata(new PropertyChangedCallback(OnImagePropertyChanged)));

        private static void OnImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as HelpControl;
            if (obj != null)
            {
                obj.InnerImage.Source = (BitmapImage)e.NewValue;
            }
        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as HelpControl;
            if (obj != null)
            {
                obj.InnerText.Text = (string)e.NewValue;
            }
        }

        private static void OnImageWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as HelpControl;
            if (obj != null)
            {
                obj.InnerImage.Height = (int)e.NewValue;
                obj.InnerText.Height = obj.Height - (int)e.NewValue;
            }
        }

        public int ImageHeight
        {
            get
            {
                return (int)this.GetValue(ImageHeightProperty);
            }
            set
            {
                this.SetValue(ImageHeightProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        public BitmapImage Image
        {
            get
            {
                return (BitmapImage)this.GetValue(ImageProperty);
            }
            set
            {
                this.SetValue(ImageProperty, value);
            }
        }

        public HelpControl()
        {
            InitializeComponent();
        }

    }
}
