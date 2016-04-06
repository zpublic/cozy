using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MMS.UI.Default
{
    public class Connection : System.Windows.Window
    {
        private Button mCloseBtn;
        private DockPanel mTitleBorder;
        private Button mConnectBtn;
        private Button mCancelBtn;
        private ComboBox mDBType;
        private TextBox mAddress;
        private TextBox mPort;
        private TextBox mUsername;
        private PasswordBox mPassword;

        public delegate void OKClick(string type, string address, string port, string username, string password);
        public event OKClick OKButton_Click;

        public Connection()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Style = (Style)Application.Current.Resources["ConnectionStyle"];
            this.Loaded += Connection_Loaded;
        }

        void Connection_Loaded(object sender, RoutedEventArgs e)
        {
            this.mCloseBtn.Click += mCloseBtn_Click;
            this.mTitleBorder.MouseMove += mTitleBorder_MouseMove;
            this.mConnectBtn.Click += mConnectBtn_Click;
            this.mCancelBtn.Click += mCancelBtn_Click;
        }

        void mCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void mConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            string dbType = (((this.mDBType.SelectedValue as ComboBoxItem).Content as StackPanel).Children[1] as TextBlock).Text;
            this.OKButton_Click(dbType, this.mAddress.Text, this.mPort.Text, this.mUsername.Text, this.mPassword.Password);
            this.Close();
        }

        public override void OnApplyTemplate()
        {
            this.mCloseBtn = (Button)this.GetTemplateChild("closeBtn");
            this.mTitleBorder = (DockPanel)this.GetTemplateChild("titleBorder");
            this.mConnectBtn = (Button)this.GetTemplateChild("connectBtn");
            this.mCancelBtn = (Button)this.GetTemplateChild("cancelBtn");
            this.mDBType = (ComboBox)this.GetTemplateChild("dbType");
            this.mAddress = (TextBox)this.GetTemplateChild("address");
            this.mPort = (TextBox)this.GetTemplateChild("port");
            this.mUsername = (TextBox)this.GetTemplateChild("username");
            this.mPassword = (PasswordBox)this.GetTemplateChild("password");
            base.OnApplyTemplate();
        }

        void mTitleBorder_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void mCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
