using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CozyMabi.WpfExe.Command;

namespace CozyMabi.WpfExe.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            this.PropertyChanged += PropertyChangedEvent;
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            
        }
    }
}
