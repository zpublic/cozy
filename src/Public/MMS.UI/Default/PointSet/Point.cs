using MMS.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.UI.Default
{
    public class Point : BaseINotifyPropertyChanged
    {
        private int mWidth = 10;
        public int Width { get { return this.mWidth; } set { this.mWidth = value; OnPropertyChanged("Width"); } }

        private int mHeight = 10;
        public int Height { get { return this.mHeight; } set { this.mHeight = value; OnPropertyChanged("Height"); } }

        private string mBackgroundColor = "SkyBlue";
        public string BackgroundColor { get { return this.mBackgroundColor; } set { this.mBackgroundColor = value; OnPropertyChanged("BackgroundColor"); } }
    }
}
