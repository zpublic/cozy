using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.UI.Common;
using System.Windows.Input;

namespace MMS.UI.Default
{
    public class MenuItem : BaseINotifyPropertyChanged
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public List<MenuItem> Children { get; set; }

        public ICommand Command { get; set; }
    }
}
