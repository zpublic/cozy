using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MMS.UI.Default
{
    public class Window : System.Windows.Window
    {
        private Button mCloseBtn;
        private Button mMinBtn;
        private Button mMaxBtn;
        private Button mRestoreBtn;
        private DockPanel mTitleBorder;
        private Image mLogo;
        private TextBlock mTitleTextBlock;

        public Window()
        {
            this.Style = (Style)Application.Current.Resources["DefaultWindowStyle"];
            FullScreenManager.RepairWpfWindowFullScreenBehavior(this);
            this.Loaded += DefaultWindow_Loaded;
        }

        public override void OnApplyTemplate()
        {
            this.mCloseBtn = (Button)this.GetTemplateChild("closeBtn");
            this.mMinBtn = (Button)this.GetTemplateChild("minBtn");
            this.mMaxBtn = (Button)this.GetTemplateChild("maxBtn");
            this.mRestoreBtn = (Button)this.GetTemplateChild("restoreBtn");
            this.mTitleBorder = (DockPanel)this.GetTemplateChild("titleBorder");
            this.mLogo = (Image)this.GetTemplateChild("logo");
            this.mTitleTextBlock = (TextBlock)this.GetTemplateChild("TitleTextBlock");
            base.OnApplyTemplate();
        }

        private void DefaultWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.mCloseBtn.Click += MCloseBtn_Click;
            this.mMinBtn.Click += mMinBtn_Click;
            this.mMaxBtn.Click += mMaxBtn_Click;
            this.mRestoreBtn.Click += mRestoreBtn_Click;
            this.mTitleBorder.MouseMove += mTitleBorder_MouseMove;
            this.mLogo.Source = this.Icon;
            this.mTitleTextBlock.Text = this.Title;
        }

        void mTitleBorder_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        void mRestoreBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
        }

        void mMaxBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        void mMinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void MCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
