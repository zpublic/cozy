using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CozySql.Exe.Commands;
using System.Windows;
using Microsoft.Win32;

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

        private string dbPath;
        public string DbPath
        {
            get
            {
                return dbPath;
            }
            set
            {
                Set(ref dbPath, value, "DbPath");
            }
        }

        private int dbType;
        public int DbType
        {
            get
            {
                return dbType;
            }
            set
            {
                Set(ref dbType, value, "DbType");
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
                {
                    switch (DbType)
                    {
                        //MySql
                        case 0:
                            MySqlDbTodo();
                            return;
                        //Sqlite
                        case 1:
                            SqliteDbTodo();
                            return;
                    }
                });
            }
        }

        private ICommand openDbFileCommand;
        public ICommand OpenDbFileCommand
        {
            get
            {
                return openDbFileCommand = openDbFileCommand ?? new DelegateCommand(x =>
                {
                    var openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() ?? false)
                    {
                        DbPath = openFileDialog.FileName;
                    }
                });
            }
        }

        #endregion

        void SqliteDbTodo()
        {
            //Todo
        }

        void MySqlDbTodo()
        {
            MessageBox.Show(String.Format("{0}\n{1}\n{2}\n{3}\n{4}", ConnectName, ConnectAddress, ConnectPort, UserName, PassWord));
        }

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
