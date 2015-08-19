using CozyDitto.Exe.Command;
using CozyDitto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CozyDitto.Exe.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ICommand deactivateCommand;
        public ICommand DeactivateCommand
        {
            get
            {
                return deactivateCommand = deactivateCommand ?? new DelegateCommand((x) =>
                {
                    WindowVisibility = Visibility.Collapsed;
                });
            }
        }

        private ICommand copyCommand;
        public ICommand CopyCommand
        {
            get
            {
                return copyCommand = copyCommand ?? new DelegateCommand((x) =>
                {
                    if (SelectedClipboardText != null && SelectedClipboardText.Length > 0)
                    {
                        Util.SetClipboardText(SelectedClipboardText);

                        WindowVisibility = Visibility.Collapsed;
                    }
                });
            }
        }
    }
}
