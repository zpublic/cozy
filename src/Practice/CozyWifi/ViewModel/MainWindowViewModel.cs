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
using CozyWifi.Operator;

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

        private string username = "";
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

        private string password = "";
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

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                Set(ref message, value, "Message");
            }
        }

        #endregion

        #region Command
        private ICommand switchStateCommand;
        public ICommand SwitchStateCommand
        {
            get
            {
                return switchStateCommand = switchStateCommand ?? new DelegateCommand(x =>
                    {
                        if(IsWifiOpened)
                        {
                            StopWifi();
                        }
                        else
                        {
                            string user = Username.Trim();
                            string pass = Password.Trim();
                            if (user.Length == 0)
                            {
                                Message = "请输入名称";
                                return;
                            }
                            else if (pass.Length < 8)
                            {
                                Message = "密码长度必须大于8";
                                return;
                            }
                            else
                            {
                                StartWifi();
                            }
                        }
                        IsWifiOpened = !IsWifiOpened;
                    });
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

        private IWifiOperator wifiOperator;
        public IWifiOperator WifiOperator
        {
            get
            {
                return wifiOperator = wifiOperator ?? new WifiOperatorCommand();
            }
        }

        private void StartWifi()
        {
            Message = "正在启动Wifi";
            WifiOperator.StopWifi();
            WifiOperator.SetWifiProperty(Username, Password);
            WifiOperator.StartWifi();
            Message = "Wifi已启动";
        }

        private void StopWifi()
        {
            Message = "正在停止Wifi";
            WifiOperator.StopWifi();
            Message = "Wifi已停止";
        }
    }
}
