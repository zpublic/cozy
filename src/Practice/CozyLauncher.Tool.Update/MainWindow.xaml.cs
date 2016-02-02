using CozyLauncher.Tool.Update.Helper;
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
        private InfrastructureLoader Loader { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            Loader = InfrastructureLoader.Create("CozyLauncher.Core.Dll");
            Loader.LoadType(@"CozyLauncher.Core.Update.UpdateMgr");
            Loader.LoadObject();

            bool needToUpdate = false;
            try
            {
                needToUpdate = (bool)Loader.Invoke("CheckUpdate");
            }
            catch (Exception)
            {
                needToUpdate = false;
            }

            try
            {
                if (needToUpdate)
                {
                    if (MessageBox.Show("检测到更新 是否升级", "Update", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Show();
                        this.ViewModel.DoUpdate(Loader);
                        Loader = null;
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
            finally
            {
                Loader?.Dispose();
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
