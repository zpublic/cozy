using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using CozyMabi.WpfExe.Command;

namespace CozyMabi.WpfExe.ViewModel
{
    public class LoginWindowViewModel : BaseViewModel
    {
        #region Property

        private string _AccountEmail = String.Empty;
        public string AccountEmail
        {
            get
            {
                return _AccountEmail;
            }
            set
            {
                Set(ref _AccountEmail, value.Trim(), "AccountEmail");
            }
        }

        private string _AccountPassword = String.Empty;
        public string AccountPassword
        {
            get
            {
                return _AccountPassword;
            }
            set
            {
                Set(ref _AccountPassword, value.Trim(), "AccountPassword");
            }
        }

        private bool _RememberAccount = false;
        public bool RememberAccount
        {
            get
            {
                return _RememberAccount;
            }
            set
            {
                Set(ref _RememberAccount, value, "RememberAccount");
            }
        }

        private bool _Result = false;
        public bool Result
        {
            get
            {
                return _Result;
            }
            set
            {
                Set(ref _Result, value, "Result");
            }
        }

        #endregion

        #region Command

        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _LoginCommand = _LoginCommand ?? new DelegateCommand(x =>
                {
                    MessageBox.Show(String.Format("Email : {0} Password : {1} Remember : {2}", AccountEmail, AccountPassword, RememberAccount));

                    Result = true;
                });
            }
        }

        #endregion

        public LoginWindowViewModel()
        {

        }
    }
}
