using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.UI.Common;
using System.Windows.Input;

namespace MMS.UI.Default
{
    public class ContextMenuItem : BaseINotifyPropertyChanged
    {
        private string mText = String.Empty;
        public string Text { get { return this.mText; } set { this.mText = value; OnPropertyChanged("Text"); } }

        private ICommand mCommand = null;
        public ICommand Command { get { return this.mCommand; } set { this.mCommand = value; OnPropertyChanged("Command"); } }
    }
}
