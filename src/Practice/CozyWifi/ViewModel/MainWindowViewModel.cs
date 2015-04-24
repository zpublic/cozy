using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using CozyWifi.Command;
using System.Windows;
using System.Runtime.InteropServices;

namespace CozyWifi.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Property
        private bool isWifiOpened = false;
        public bool IsWifiOpened
        {
            get
            {
                return isWifiOpened;
            }
            set
            {
                Set(ref isWifiOpened, value, "IsWifiOpened");
            }
        }

        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                Set(ref username, value, "Username");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                Set(ref password, value, "Password");
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
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsWifiOpened")
            {
                // 由于是先改变值再触发事件 所以当事件触发时 当前状态应该是已经更改过的状态
                if(IsWifiOpened)
                {
                    StartWifi();
                }
                else
                {
                    StopWifi();
                }
            }
        }

        private void StartWifi()
        {
            MessageBox.Show("Start Wifi");
        }

        private void StopWifi()
        {
            MessageBox.Show("Stop Wifi");
        }
    }
}
