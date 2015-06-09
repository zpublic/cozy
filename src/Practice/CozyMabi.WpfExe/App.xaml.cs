using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CozyMabi.WpfExe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            LoginWindow window = new LoginWindow();
            bool? dialogResult = window.ShowDialog();
            if (dialogResult ?? false)
            {
                base.OnStartup(e);
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
            else
            {
                this.Shutdown();
            }
        }
    }
}
