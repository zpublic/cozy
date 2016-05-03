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
using CozyFarseer.TopList.Network;
using System.IO;

namespace CozyFarseer.TopList.Controls
{
    /// <summary>
    /// FarseerNodeItem.xaml 的交互逻辑
    /// </summary>
    public partial class FarseerNodeItem : UserControl
    {
        #region Dependency Property

        private const string defaultAvator = "/CozyFarseer.TopList;component/Resources/default.jpg";

        public static readonly DependencyProperty AvatarProperty =
            DependencyProperty.Register("Avatar", typeof(string), typeof(FarseerNodeItem),
                new PropertyMetadata(defaultAvator, new PropertyChangedCallback(OnAvatarChanged)));

        public static readonly DependencyProperty NickProperty =
            DependencyProperty.Register("Nick", typeof(string), typeof(FarseerNodeItem),
                new PropertyMetadata("anonymous", new PropertyChangedCallback(OnNickChanged)));

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(int), typeof(FarseerNodeItem),
                new PropertyMetadata(0, new PropertyChangedCallback(OnTimeChanged)));

        public static readonly DependencyProperty AccuracyProperty =
            DependencyProperty.Register("Accuracy", typeof(int), typeof(FarseerNodeItem),
                new PropertyMetadata(0, new PropertyChangedCallback(OnAccuracyChanged)));

        public static readonly DependencyProperty TTypeProperty =
            DependencyProperty.Register("TType", typeof(int), typeof(FarseerNodeItem),
                new PropertyMetadata(-1, new PropertyChangedCallback(OnTTypeChanged)));

        public static readonly DependencyProperty TStatusProperty =
            DependencyProperty.Register("TStatus", typeof(int), typeof(FarseerNodeItem),
                new PropertyMetadata(-1, new PropertyChangedCallback(OnTStatusChanged)));

        public static readonly DependencyProperty TextContentProperty =
            DependencyProperty.Register("TextContent", typeof(string), typeof(FarseerNodeItem),
                new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnTextContentChanged)));

        #endregion

        #region Property

        public string Avatar
        {
            get { return (string)this.GetValue(AvatarProperty); }
            set { this.SetValue(AvatarProperty, value); }
        }

        public string Nick
        {
            get { return (string)this.GetValue(NickProperty); }
            set { this.SetValue(NickProperty, value); }
        }

        public string Time
        {
            get { return (string)this.GetValue(TimeProperty); }
            set { this.SetValue(TimeProperty, value); }
        }

        public string Accuracy
        {
            get { return (string)this.GetValue(AccuracyProperty); }
            set { this.SetValue(AccuracyProperty, value); }
        }

        public string TType
        {
            get { return (string)this.GetValue(TTypeProperty); }
            set { this.SetValue(TTypeProperty, value); }
        }

        public string TStatus
        {
            get { return (string)this.GetValue(TStatusProperty); }
            set { this.SetValue(TStatusProperty, value); }
        }

        public string TextContent
        {
            get { return (string)this.GetValue(TextContentProperty); }
            set { this.SetValue(TextContentProperty, value); }
        }

        #endregion

        #region Callback

        private static void OnAvatarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FarseerNodeItem;
            if (obj != null)
            {
                obj.OnAvatarChanged((string)e.NewValue);
            }
        }

        private static void OnNickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FarseerNodeItem;
            if (obj != null)
            {
                obj.OnNickChanged((string)e.NewValue);
            }
        }

        private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FarseerNodeItem;
            if (obj != null)
            {
                obj.OnTimeChanged((int)e.NewValue);
            }
        }

        private static void OnAccuracyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FarseerNodeItem;
            if (obj != null)
            {
                obj.OnAccuracyChanged((int)e.NewValue);
            }
        }

        private static void OnTTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FarseerNodeItem;
            if (obj != null)
            {
                obj.OnTTypeChanged((int)e.NewValue);
            }
        }

        private static void OnTStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FarseerNodeItem;
            if (obj != null)
            {
                obj.OnTStatusChanged((int)e.NewValue);
            }
        }

        private static void OnTextContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as FarseerNodeItem;
            if (obj != null)
            {
                obj.OnTextContentChanged((string)e.NewValue);
            }
        }

        #endregion

        private void OnAvatarChanged(string v)
        {
            //TODO async load

            if (!string.IsNullOrEmpty(v))
            {
                var s = new BitmapImage();
                s.BeginInit();
                s.UriSource = new Uri("http://api.imxz.net" + v);
                s.EndInit();
                s.CacheOption = BitmapCacheOption.OnLoad;
                this.AvatarImage.Source = s;
            }
        }

        private void OnNickChanged(string v)
        {
            if (!string.IsNullOrEmpty(v))
            {
                this.NickLabel.Content = v;
            }
        }

        private void OnTimeChanged(int v)
        {

        }

        private void OnAccuracyChanged(int v)
        {
            this.AccuracyLabel.Content = $"胜率{v}%";
        }

        private readonly string[] TTypeDesc = { "未知", "具体看涨", "具体看跌", "纯看涨", "纯看跌" };
        private void OnTTypeChanged(int v)
        {
            if (v >= -1 && v < TTypeDesc.Length - 1)
            {
                this.TTypeLabel.Content = TTypeDesc[v + 1];
            }
        }

        private readonly string[] TStatusDesc = { "未知", "进行中", "成功", "失败" };
        private void OnTStatusChanged(int v)
        {
            if (v >= -1 && v <= TStatusDesc.Length - 1)
            {
                this.TStatusLabel.Content = TStatusDesc[v + 1];
            }
        }

        private void OnTextContentChanged(string v)
        {

        }

        public FarseerNodeItem()
        {
            InitializeComponent();
        }
    }
}
