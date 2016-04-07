using Common.Logger;
using MMS.UI.Common;
using MMS.UI.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.Client
{
    public class MainWindowViewModel : BaseINotifyPropertyChanged
    {
        private static MainWindowViewModel mMainWindow;
        private static readonly Logger mLog = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly object syncRoot = new object();

        private MainWindowViewModel()
        {
        }

        public static MainWindowViewModel GetInstance()
        {
            if (mMainWindow == null)
            {
                lock (syncRoot)
                {
                    if (mMainWindow == null)
                    {
                        mMainWindow = new MainWindowViewModel();
                    }
                }
            }
            return mMainWindow;
        }

        public string mPage = Navigate.GetInstance().GetCurrentPage();
        public string Page { get { return this.mPage; } set { this.mPage = value; OnPropertyChanged("Page"); } }
    }

    public class Navigate
    {
        private static Navigate mNavigate;
        private static readonly Logger mLog = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly object syncRoot = new object();
        private int mCurrentId = 0;

        private Navigate()
        {
            this.Initialize();
        }

        public static Navigate GetInstance()
        {
            if (mNavigate == null)
            {
                lock (syncRoot)
                {
                    if (mNavigate == null)
                    {
                        mNavigate = new Navigate();
                    }
                }
            }
            return mNavigate;
        }

        private List<NavigationItem> GetItems()
        {
            List<NavigationItem> menus = new List<NavigationItem>();
            NavigationItem download = new NavigationItem()
            {
                Url = new Uri("/Views/Download.xaml", UriKind.Relative)
            };
            menus.Add(download);
            return menus;
        }

        private List<NavigationItem> mItems;

        public void Initialize()
        {
            this.mItems = this.GetItems();
        }

        public string GetCurrentPage()
        {
            return this.mItems[this.mCurrentId].Url.ToString();
        }
    }
}
