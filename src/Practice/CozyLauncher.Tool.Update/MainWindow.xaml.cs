using System;
using System.Windows;
using CozyLauncher.Core.Update;

namespace CozyLauncher.Tool.Update
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private UpdateMgr UpdateManager { get; set; } = new UpdateMgr();

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            bool needToUpdate = false;
            try
            {
                needToUpdate = UpdateManager.CheckUpdate();
            }
            catch (Exception)
            {
                needToUpdate = false;
            }

            if (needToUpdate)
            {
                if (MessageBox.Show("检测到更新 是否升级", "Update", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Show();
                    this.ViewModel.DoUpdate(UpdateManager);
                }
                else
                {
                    App.Current.Shutdown();
                }
            }
            else
            {
                App.Current.Shutdown();
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "UpdateCommand.Exit")
            {
                App.Current.Shutdown();
            }
        }
    }
}
