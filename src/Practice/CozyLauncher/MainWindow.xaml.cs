﻿using CozyLauncher.Core.Plugin;
using CozyLauncher.PluginBase;
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
using System.ComponentModel;
using CozyLauncher.Infrastructure.Hotkey;
using System.Reflection;
using System.Resources;
using System.Drawing;
using System.IO.Pipes;
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
            InitializeComponent();

            InitCloseServer();

            InitialTray();

            try
            {
                GlobalHotkey.Instance.Load();
            }
            catch(Exception)
            {
                MessageBox.Show("热键冲突 注册失败");
            }

            this.ViewModel.PropertyChanged += OnViewModelPropertyChanged;

            this.QueryTextBox.Focus();
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
                    break;
                case Key.Down:
                    this.ViewModel.DownCommand.Execute(null);
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
            else if(e.PropertyName == "SystemCommand.ShowConfig")
            {
                ShowConfig();
            }
            else if(e.PropertyName == "SystemCommand.About")
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

        public void CloseApp()
        {
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

        public void ClearEditBox()
        {
            QueryTextBox.Clear();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalHotkey.Instance.UnregistAllHotkey();
        }

        private void InitialTray()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.BalloonTipText = "我在这里";
            notifyIcon.Text = "CozyLauncher";
            notifyIcon.Icon = CozyLauncher.Resource.AppTray;
            notifyIcon.Visible = true;

            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += Exit_Click;

            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
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
                using (var nps = new NamedPipeServerStream("CozyLauncher.CloseAppPipe"))
                {
                    nps.WaitForConnection();

                    try
                    {
                        using (var sr = new StreamReader(nps))
                        {
                            var res = sr.ReadToEnd();
                            if(res == "SystemCommand.CloseApp")
                            {
                                Dispatcher.Invoke(() => 
                                {
                                    CloseApp();
                                });
                            }
                        }
                    }
                    catch(IOException)
                    {

                    }
                }
            });
        }
    }
}
