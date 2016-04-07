using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MMS.UI.Default
{
    internal class Message : System.Windows.Window
    {
        private Button mCloseBtn;
        private DockPanel mTitleBorder;
        private Image mLogo;
        private TextBlock mTitleTextBlock;
        private TextBlock mContextTextBlock;
        private Image mIconImage;
        private string mTitle;
        private string mText;
        private string mIcon;

        internal Message(string icon,string title, string text)
        {
            this.Style = (Style)Application.Current.Resources["MessageStyle"];
            this.mTitle = title;
            this.mText = text;
            this.mIcon = icon;
            this.Loaded += Message_Loaded;
        }

        void Message_Loaded(object sender, RoutedEventArgs e)
        {
            this.mCloseBtn.Click += mCloseBtn_Click;
            this.mTitleBorder.MouseMove += mTitleBorder_MouseMove;
            this.mLogo.Source = this.Icon;
            this.mTitleTextBlock.Text = this.mTitle;
            this.mContextTextBlock.Text = this.mText;
            this.mIconImage.Source = this.Icon;
        }

        private void mTitleBorder_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void mCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public override void OnApplyTemplate()
        {
            this.mCloseBtn = (Button)this.GetTemplateChild("closeBtn");
            this.mTitleBorder = (DockPanel)this.GetTemplateChild("titleBorder");
            this.mLogo = (Image)this.GetTemplateChild("logo");
            this.mTitleTextBlock = (TextBlock)this.GetTemplateChild("TitleTextBlock");
            this.mContextTextBlock = (TextBlock)this.GetTemplateChild("contextText");
            this.mIconImage = (Image)this.GetTemplateChild("icon");
            base.OnApplyTemplate();
        }
    }
}
