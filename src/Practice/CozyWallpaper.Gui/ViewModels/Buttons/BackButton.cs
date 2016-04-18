using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MMS.Installation
{
    public class BackButton : Button
    {
        public BackButton()
        {
            this.Text = "上一页";
            this.IsEnabled = true;
            this.ButtonVisiblity = Visibility.Hidden;
            this.Command = new BackCommand();
        }
    }

    public class BackCommand : ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //Navigation.GetInstance().Back();
        }
    }
}
