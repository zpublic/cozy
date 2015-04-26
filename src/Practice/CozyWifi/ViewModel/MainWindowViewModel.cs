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

        public bool IsAdmin { get; private set; }

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
                            bool result = WifiOperator.WifiStateQuery();
                            if (result)
                            {
                                Message = "Wifi停止失败";
                            }
                            else
                            {
                                Message = "Wifi已停止";
                                IsWifiOpened = false;
                            }
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
                                bool result = WifiOperator.WifiStateQuery();
                                if(result)
                                {
                                    IsWifiOpened = true;
                                    Message = "Wifi已启动";
                                }
                                else
                                {
                                    Message = "Wifi启动失败";
                                }
                            }
                        }
                    });
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            this.PropertyChanged += PropertyChangedEvent;
            InitWifiState();
        }

        private void InitWifiState()
        {
            bool result = WifiOperator.WifiStateQuery();
            if(result)
            {
                IsWifiOpened = true;
                Message = "Wifi已启动";
            }
            else
            {
                IsWifiOpened = false;
                Message = "Wifi未启动";
            }

            System.Security.Principal.WindowsIdentity wid = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal printcipal = new System.Security.Principal.WindowsPrincipal(wid);
            IsAdmin = (printcipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator));
            if (!IsAdmin)
            {
                Message = "请用管理员权限运行";
            }
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsWifiOpened")
            {

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
        }

        private void StopWifi()
        {
            Message = "正在停止Wifi";
            WifiOperator.StopWifi();
        }
    }
}
