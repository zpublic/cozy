using CozyPlague.Models;
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
using CozyPlague.Ext;

namespace CozyPlague.Controls
{
    /// <summary>
    /// UserColorControls.xaml 的交互逻辑
    /// </summary>
    public partial class UserColorControls : UserControl
    {
        public static readonly DependencyProperty UserColorProperty = DependencyProperty.Register(
            "UserColor", 
            typeof(UserColor), 
            typeof(UserColorControls), 
            new PropertyMetadata(null, new PropertyChangedCallback(OnColorListChanged)));

        public UserColor UserColor
        {
            get
            {
                return (UserColor)this.GetValue(UserColorProperty);
            }
            set
            {
                this.SetValue(UserColorProperty, value);
            }
        }

        public UserColorControls()
        {
            InitializeComponent();
        }

        public static void OnColorListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as UserColorControls;
            if(obj != null)
            {
                obj.OnColorListChanged((UserColor)e.NewValue);
            }
        }

        private void OnColorListChanged(UserColor color)
        {
            Brush brush = null;
            if(color.RGB.ToColor(ref brush))
            {
                this.MainText.Text = $"#{color.RGB}".ToUpper();
                this.MainColor.Fill = brush;
            }
        }
    }
}
