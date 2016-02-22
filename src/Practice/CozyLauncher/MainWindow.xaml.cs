using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using CozyLauncher.Infrastructure.Hotkey;
using CozyLauncher.Infrastructure.ProcessMutex;
using CozyLauncher.Infrastructure.IPC;
using CozyLauncher.Infrastructure.Version;
using CozyLauncher.Infrastructure.StartUp;
using CozyLauncher.Infrastructure.Config;

namespace CozyLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon = null;

        public MainWindow()
        {
            if (ProcessMutexMgr.Instance.CheckExist("CozyLauncher.Main"))
            {
                MessageBox.Show("多个程序实例正在运行");
                CloseApp();
            }
            else
            {
                InitializeComponent();

                InitCloseServer();

                InitialTray();

                GlobalHotkey.Instance.Init();
                GlobalHotkey.Instance.ReplaceWindowRAction = new Action(ShowApp);

                ConfigManager.Instance.Load();

                if (!VersionManager.Instance.IsExist)
                {
                    StartUpManager.Instance.IsAutoStartUp   = true;

                    this.ViewModel.QueryCommand.Execute("guide");
                    this.ViewModel.DoCommand.Execute(null);
                    HideApp();
                }

                this.ViewModel.PropertyChanged += OnViewModelPropertyChanged;

                this.QueryTextBox.Focus();

                this.ViewModel.Update();
            }
        }

        private void OnWindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void OnQueryKeyUp(object sender, KeyEventArgs e)
        {
            var oldHandle = e.Handled;
            e.Handled = true;
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            switch (key)
            {
                case Key.Up:
                    this.ViewModel.UpCommand.Execute(null);
                    this.ResultList.ScrollIntoView(this.ResultList.SelectedItem);
                    break;
                case Key.Down:
                    this.ViewModel.DownCommand.Execute(null);
                    this.ResultList.ScrollIntoView(this.ResultList.SelectedItem);
                    break;
                case Key.Enter:
                    this.ViewModel.DoCommand.Execute(null);
                    break;
                case Key.Escape:
                    this.ViewModel.HideAndClear();
                    break;
                default:
                    e.Handled = oldHandle;
                    break;
            }
        }

        private void OnQueryTextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox != null)
            {
                var text = textbox.Text.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    this.ViewModel.QueryCommand.Execute(text);
                }
                else
                {
                    this.ViewModel.Clear();
                }
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SystemCommand.HideApp")
            {
                HideApp();
            }
            else if (e.PropertyName == "SystemCommand.CloseApp")
            {
                CloseApp();
            }
            else if (e.PropertyName == "SystemCommand.ShowApp")
            {
                ShowApp();
            }
            else if (e.PropertyName == "SystemCommand.ShowConfig")
            {
                ShowConfig();
            }
            else if(e.PropertyName == "SystemCommand.Help")
            {
                Help();
            }
            else if (e.PropertyName == "SystemCommand.About")
            {
                About();
            }
            else if (e.PropertyName == "SystemCommand.ClearEditBox")
            {
                ClearEditBox();
            }
        }

        private void HideWox()
        {
            Hide();
        }

        private void ShowWox()
        {
            Show();
        }

        private bool IsNeedToClose { get; set; } = true;
        public void CloseApp()
        {
            if(IsNeedToClose)
            {
                PipeIPCServer.TryCloseServer("CozyLauncher.CloseAppPipe");
            }

            Application.Current.Shutdown();
        }

        public void HideApp()
        {
            Dispatcher.Invoke(HideWox);
        }

        public void ShowApp()
        {
            Dispatcher.Invoke(ShowWox);
        }

        public void ShowConfig()
        {
            var config = new ConfigWindow();
            config.ShowDialog();
        }

        public void About()
        {
            var about = new AboutWindow();
            about.ShowDialog();
        }

        public void Help()
        {
            var help = new HelpWindow();
            help.ShowDialog();
        }

        public void ClearEditBox()
        {
            QueryTextBox.Clear();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalHotkey.Instance.Release();
        }

        private void InitialTray()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.BalloonTipText = "我在这里";
            notifyIcon.Text = "CozyLauncher";
            notifyIcon.Icon = CozyLauncher.Resource.AppTray;
            notifyIcon.Visible = true;

            notifyIcon.Click += NotifyIcon_Click;

            InitialTrayMenu();
        }

        private void InitialTrayMenu()
        {
            if (null == notifyIcon)
            {
                return;
            }


            System.Windows.Forms.MenuItem about = new System.Windows.Forms.MenuItem("关于");
            about.Click += About_Click;

            System.Windows.Forms.MenuItem guide = new System.Windows.Forms.MenuItem("向导");
            guide.Click += Guide_Click;

            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += Exit_Click;

            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { about, guide, exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MouseEventArgs mouse_e = (System.Windows.Forms.MouseEventArgs)e;

            if (mouse_e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ShowWox();
            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            About();
        }

        private void Guide_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            CloseApp();
        }

        private void InitCloseServer()
        {
            Task.Factory.StartNew(()=> 
            {
                using (var nps = new PipeIPCServer("CozyLauncher.CloseAppPipe"))
                {
                    nps.Callback = (s) =>
                    {
                        if (s == "SystemCommand.CloseApp")
                        {
                            IsNeedToClose = false;
                            Dispatcher.Invoke(() =>
                            {
                                CloseApp();
                            });
                        }
                    };

                    nps.Wait();
                }
            });
        }
    }
}
