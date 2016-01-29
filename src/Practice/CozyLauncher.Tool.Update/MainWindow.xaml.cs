using CozyLauncher.Core.Update;
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

namespace CozyLauncher.Tool.Update
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            var updatemgr       = new UpdateMgr();
            bool needToUpdate   = false;
            try
            {
                needToUpdate = updatemgr.CheckUpdate();
            }
            catch (Exception)
            {
                needToUpdate = false;
            }

            if (needToUpdate)
            {
                if(MessageBox.Show("检测到更新 是否升级", "Update", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Show();
                    this.ViewModel.DoUpdate(updatemgr);
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
