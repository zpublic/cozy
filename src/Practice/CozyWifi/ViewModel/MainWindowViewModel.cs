using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using CozyWifi.Command;
using System.Windows;

namespace CozyWifi.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Property
        private bool isWifiOpened;
        public bool IsWifiOpened
        {
            get
            {
                return isWifiOpened;
            }
            set
            {
                Set(ref isWifiOpened, value, "WifiOpened");
            }
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                Set(ref userName, value, "UserName");
            }
        }

        private string passWord;
        public string PassWord
        {
            get
            {
                return passWord;
            }
            set
            {
                Set(ref passWord, value, "PassWord");
            }
        }
        #endregion

        #region Command
        private ICommand switchStateCommand;
        public ICommand SwitchStateCommand
        {
            get
            {
                return switchStateCommand = switchStateCommand ?? new DelegateCommand(x => IsWifiOpened = !IsWifiOpened);
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            this.PropertyChanged += PropertyChangedEvent;
            this.TestData();
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WifiOpened")
            {

            }
        }

        private void TestData()
        {

        }
    }
}
