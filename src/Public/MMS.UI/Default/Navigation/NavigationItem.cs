using MMS.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.UI.Default
{
    public class NavigationItem : BaseINotifyPropertyChanged
    {
        private string mText = String.Empty;
        public string Text { get { return this.mText; } set { this.mText = value; OnPropertyChanged("Text"); } }

        private NavigationType mStatus = NavigationType.Wait;
        public NavigationType Status { get { return this.mStatus; } set { this.mStatus = value; OnPropertyChanged("Status"); } }

        public Uri Url { get; set; }
    }

    public enum NavigationType
    {
        Wait,
        Process,
        Complete
    }
}
