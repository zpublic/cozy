using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CozySql.Exe.Commands;
using System.Windows;

namespace CozySql.Exe.ViewModels
{
    public class ConnectEditorViewModel : BaseViewModel
    {
        #region Property
        private string connectName;
        public string ConnectName
        {
            get
            {
                return connectName;
            }
            set
            {
                Set(ref connectName, value, "ConnectName");
            }
        }

        private string connectAddress;
        public string ConnectAddress
        {
            get
            {
                return connectAddress;
            }
            set
            {
                Set(ref connectAddress, value, "ConnectAddress");
            }
        }

        private string connectPort;
        public string ConnectPort
        {
            get
            {
                return connectPort;
            }
            set
            {
                Set(ref connectPort, value, "ConnectPort");
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

        #region Commad
        private ICommand resetToDefault;
        public ICommand ResetToDefault
        {
            get
            {
                return resetToDefault = resetToDefault ?? new DelegateCommand(x => DefaultData());
            }
        }

        private ICommand showTestData;
        public ICommand ShowTestData
        {
            get
            {
                return showTestData = showTestData ?? new DelegateCommand(x =>
                MessageBox.Show(String.Format("{0}\n{1}\n{2}\n{3}\n{4}", ConnectName, ConnectAddress, ConnectPort, UserName, PassWord)));
            }
        }
        #endregion

        public ConnectEditorViewModel()
        {
            DefaultData();
        }

        private void DefaultData()
        {
            ConnectName = "Connect1";
            ConnectAddress = "127.0.0.1";
            ConnectPort = "3306";
            UserName = "username";
            PassWord = "password";
        }
    }
}
