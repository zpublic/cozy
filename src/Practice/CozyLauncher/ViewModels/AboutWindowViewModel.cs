using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.Commands;
using System.Windows.Input;

namespace CozyLauncher.ViewModels
{
    public class AboutWindowViewModel : BaseViewModel
    {
        private ICommand _OkCommand;
        public ICommand OkCommand
        {
            get
            {
                return _OkCommand = _OkCommand ?? new DelegateCommand(x => {
                    this.OnPropertyChanged("SystemCommand.Ok");
                });
            }
        }
    }
}
