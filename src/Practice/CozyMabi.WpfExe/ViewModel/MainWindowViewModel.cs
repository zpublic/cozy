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

        }
    }
}
