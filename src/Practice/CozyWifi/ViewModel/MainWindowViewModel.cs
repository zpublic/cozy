using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CozyWifi.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            this.PropertyChanged += PropertyChangedEvent;
            this.TestData();
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectTreeInfo")
            {

            }
        }

        private void TestData()
        {

        }
    }
}
