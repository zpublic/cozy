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
using CozyLauncher.Core;
using System.IO;

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

                bool needShowGuide = !File.Exists(SettingObject.ConfigFilePath);

                SettingObject.Instance.Load();
                LoadSetting();

                if (needShowGuide)
                {
                    StartUpManager.Instance.IsAutoStartUp = true;

                    SaveSetting();

                    this.ViewModel.ShowPanel("guide");
                    HideApp();
                }

                this.ViewModel.PropertyChanged += OnViewModelPropertyChanged;
                this.QueryTextBox.Focus();
                this.ViewModel.Update();
            }
        }

        private void LoadSetting()
        {
            bool isCtrl             = false;
            bool isShift            = false;
            bool isAlt              = false;
            bool isWin              = false;
            bool ReplaceWindowR     = false;
            Key key                 = Key.Space;

            SettingObject.Instance.Get("Hotkey", "IsCtrl", out isCtrl, true);
            SettingObject.Instance.Get("Hotkey", "IsShift", out isShift, false);
            SettingObject.Instance.Get("Hotkey", "IsAlt", out isAlt, true);
            SettingObject.Instance.Get("Hotkey", "IsWin", out isWin, false);
            SettingObject.Instance.Get("Hotkey", "Key", out key, Key.Space);
            SettingObject.Instance.Get("Hotkey", "ReplaceWindowR", out ReplaceWindowR, true);

            var model = new HotkeyModel(isCtrl, isShift, isAlt, isWin, key);

            GlobalHotkey.Instance.RegistHotkey("HotKey.ShowApp", model);
            GlobalHotkey.Instance.ReplaceWindowR = ReplaceWindowR;

            string version = null;
            SettingObject.Instance.Get("Version", "version", out version, "0.6");
            VersionManager.Instance.Version = version;
        }

        private void SaveSetting()
        {
            var hkm = GlobalHotkey.Instance.GetRegistedHotkey("HotKey.ShowApp");

            SettingObject.Instance.Set("Hotkey", "IsCtrl", hkm.Ctrl);
            SettingObject.Instance.Set("Hotkey", "IsShift", hkm.Shift);
            SettingObject.Instance.Set("Hotkey", "IsAlt", hkm.Alt);
            SettingObject.Instance.Set("Hotkey", "IsWin", hkm.Win);
            SettingObject.Instance.Set("Hotkey", "Key", hkm.CharKey);
            SettingObject.Instance.Set("Hotkey", "ReplaceWindowR", GlobalHotkey.Instance.ReplaceWindowR);

            SettingObject.Instance.Save();
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
            //if (textbox != null)
            {
                var text = textbox.Text;//.Trim();
                if (!String.IsNullOrEmpty(text))
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
            else if (e.PropertyName == "SystemCommand.ClearEditBox")
            {
                ClearEditBox();
            }
            else if(e.PropertyName == "SystemCommand.SaveSetting")
            {
                SaveSetting();
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
            this.ViewModel.ShowPanel("about");
        }

        private void Guide_Click(object sender, EventArgs e)
        {
            this.ViewModel.ShowPanel("guide");
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
